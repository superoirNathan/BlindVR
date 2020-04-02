using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplementaryFilter : MonoBehaviour
{
    // rotation angle
    public Vector3 angle;
    public float pitch;
    public float roll;
    // filter values
    public float alpha = .0045f;
    public float alphaAlt = 1.0f;

    public float a_low = 0.3f;    //initialization of EMA alpha
    public float a_high = 0.5f;

    // accelerometer
    public Vector3 accAngle;
    public Vector3 accVal;
    public Vector3 accZero;
       // filter pass values
    public Vector3 lowAcc;        //initialization of EMA S
    public Vector3 highAcc;
    public Vector3 bandAcc;
    
    // gyroscope
    public bool gyroEnabled;
    public Gyroscope gyro;
    public Vector3 gyroRate;

        // filter pass values
    public Vector3 lowGyr;        //initialization of EMA S
    public Vector3 highGyr;
    public Vector3 bandGyr;

    // debug
    public bool gyrobandOn = false;
    public bool accbandOn = true;
    public float step = 1.0f;
    public bool alphaAltOn = false;
    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;

        angle = new Vector3();

        accAngle = new Vector3();
        accZero = Input.acceleration;
//        accZero = gyro.userAcceleration;
        lowAcc = new Vector3();
        highAcc = new Vector3();
        bandAcc = new Vector3();

        lowGyr = new Vector3();
        highGyr = new Vector3();
        bandGyr = new Vector3();

        pitch = 0;
        roll = 0;
    }
    void Update()
    {

        //accVal = Input.acceleration - accZero;
        accVal = gyro.userAcceleration - accZero;
        if (accbandOn)
        {
            lowAcc = passFilter(lowAcc, accVal, a_low);
            highAcc = passFilter(highAcc, accVal, a_high);
            bandAcc = highAcc - lowAcc;
        }
        else
        {
            bandAcc = accVal;
        }
        pitch = Mathf.Atan2(bandAcc.y, bandAcc.z) + Mathf.PI;
        roll = Mathf.Atan2(bandAcc.x, bandAcc.z) + Mathf.PI;

        accAngle = new Vector3(pitch, roll, bandAcc.z);
        
        gyroRate = new Vector3(gyro.rotationRate.x, gyro.rotationRate.y, gyro.rotationRate.z);
        if (gyrobandOn)
        {
            lowGyr = passFilter(lowGyr, gyroRate, a_low);
            highGyr = passFilter(highGyr, gyroRate, a_high);
            bandGyr = highGyr - lowGyr;
        }
        else
        {
            bandGyr = gyroRate *2.0f; //+ new Vector3(gyroRate.x, 0.0f, 0.0f);
        }
        if (alphaAltOn)
        {
            angle = (1 - alphaAlt) * (angle + bandGyr) + alphaAlt * accAngle;
        }
        else
        {
            angle = (1 - alpha) * (angle + bandGyr) + alpha * accAngle;
        }

        if (Input.touches.Length > 1)
        {
            alphaAltOn = !alphaAltOn;
            Debug.Log(alphaAltOn);
        }
        transform.rotation = Quaternion.Euler(-angle.x, -angle.y, -angle.z);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(-angle.x, -angle.y, angle.z), step);

//        Debug.Log(angle);
        Debug.DrawRay(transform.position + Vector3.up * 2, bandAcc, Color.blue);
        Debug.DrawRay(transform.position + Vector3.up * 2, new Vector3(-angle.x, -angle.y, angle.z), Color.red);
        
    }
    Vector3  passFilter(Vector3 newVal, Vector3 sensorValue, float a)
    {
        return (a* sensorValue) + ((1 - a) * newVal);//run the EMA
    }
}
