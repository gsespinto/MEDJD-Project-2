using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCameraPosition : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 2.0f;
    private float currentSpeed;
    [SerializeField] private float acceleration = 1f;

    private void Update()
    {
        if (this.transform.localPosition.magnitude > currentSpeed * Time.deltaTime)
        {
            if (currentSpeed < maxSpeed)
                currentSpeed += acceleration * Time.deltaTime;
            
            this.transform.localPosition += -this.transform.localPosition.normalized * currentSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed = 0;
        }
    }
}
