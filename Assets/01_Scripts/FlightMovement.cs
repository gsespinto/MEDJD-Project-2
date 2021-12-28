using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private Camera cam;
    [SerializeField] private float flyHeight;

    void Start()
    {
        // Get main camera reference if not valid
        if(!cam)
            cam = Camera.main;
    }

    void FixedUpdate()
    {
        Move();
    }
    
    /// <summary> Move and roate character forward around world center </summary>
    void Move()
    {
        // Do nothing if there's no camera reference
        if (!cam)
            return;

        // Get camera flored forward vector
        Vector3 flooredCamForward = this.transform.InverseTransformVector(cam.transform.forward);
        flooredCamForward.y = 0;
        flooredCamForward = this.transform.TransformVector(flooredCamForward).normalized;

        // Calculate new position around world center
        Vector3 newPos = this.transform.position + flooredCamForward * movementSpeed * Time.deltaTime;
        Vector3 gravityUp = newPos.normalized;
        newPos = Vector3.zero + gravityUp * flyHeight;
        this.transform.position = newPos;

        // Calculate rotation
        this.transform.rotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
    }
}
