using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private Camera cam;
    [SerializeField] private float flyHeight;
    [SerializeField] private float rotationSensivity = 0.1f;
    private float turnAngle;

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

        // Calculate new position around world center
        Vector3 newPos = this.transform.position + this.transform.forward * movementSpeed * Time.deltaTime;
        Vector3 gravityUp = newPos.normalized;
        newPos = Vector3.zero + gravityUp * flyHeight;
        this.transform.position = newPos;

        // Turn controller according with z rotation of camera
        turnAngle = cam.transform.localRotation.z * rotationSensivity;

        // Calculate rotation
        this.transform.RotateAround(newPos, gravityUp, turnAngle * Time.deltaTime);
        this.transform.rotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
    }
}
