using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
        Handheld.Vibrate();
    }
    private void OnTriggerExit(Collider other)
    {
        transform.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 0.0f);

    }
}
