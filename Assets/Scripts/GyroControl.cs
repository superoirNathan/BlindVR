using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    public bool gyroEnabled;
    public Gyroscope gyro;
//    public Quaternion rot;
    // Start is called before the first frame update
    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
//        rot = gyro.attitude;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Quaternion.Euler(gyro.rotationRate));
        transform.localRotation *= Quaternion.Euler(gyro.rotationRate.x, gyro.rotationRate.y, gyro.rotationRate.z) ;
        //Debug.DrawRay(transform.position + Vector3.up * 2, new Vector3(gyro.rotationRate.x, -gyro.rotationRate.y, -gyro.rotationRate.z), Color.green);

    }
}
