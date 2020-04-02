using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerometerInput : MonoBehaviour
{
    public Vector3 accAngle;
    public Vector3 accVal;
    public Vector3 accZero;

    float pitch = 0.0f;
    float roll = 0.0f;

    private void Start()
    {
        accAngle = new Vector3();
        accZero = Input.acceleration;

    }
    // Update is called once per frame
    void Update()
    {
        accVal = Input.acceleration - accZero;

        pitch = Mathf.Atan2(accVal.y, accVal.z) + Mathf.PI;
        roll = Mathf.Atan2(accVal.x, accVal.z) + Mathf.PI;

        accVal = new Vector3(pitch, roll, accVal.z);

        //transform.Translate( Input.acceleration.x, Input.acceleration.y, Input.acceleration.z);
        Debug.DrawRay(transform.position + Vector3.up*2, accVal, Color.red);
       // Debug.Log(Input.acceleration);
    }
}
