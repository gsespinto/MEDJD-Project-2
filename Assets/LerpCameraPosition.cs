using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCameraPosition : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float speed = 2.0f;

    private void Update()
    {
        if (this.transform.localPosition.magnitude > threshold)
        {
            this.transform.localPosition = -this.transform.localPosition.normalized * speed * Time.deltaTime;
            Debug.Log(this.transform.localPosition);
        }
    }
}
