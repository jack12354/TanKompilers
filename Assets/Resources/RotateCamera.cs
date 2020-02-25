using UnityEngine;
using System.Collections;
using System;

public class RotateCamera : MonoBehaviour
{
    public Vector3 RotateAround = Vector3.zero;
    Vector3 rootPos;
    float speed = 1f;
    // Use this for initialization
    void Start()
    {
        rootPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Debug.Log(transform.position);
        gameObject.transform.position = new Vector3(18 * Mathf.Sin(Time.time * speed), 7 * Mathf.Cos((Time.time + 90) * 1.0f * speed) + 10, 18 * Mathf.Cos(Time.time * speed));
        transform.LookAt(Vector3.zero);
    }
}
