using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera cam;

    [Header("Movement Values")]
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float flyHeight;
    [SerializeField] private float rotationSensivity = 0.1f;
    [SerializeField] private float rotationDeadAngle = 10f;
    private float turnAngle;

    [Header("Audio")]
    [SerializeField] private AudioSource windSource;
    [SerializeField, Range(0, 1)] private float minWindVolume = 0f;
    [SerializeField, Range(0, 1)] private float maxWindVolume = 1f;
    [SerializeField, Range(0, 90)] private float maxWindRotationValue = 75.0f;
    [SerializeField, Range(0, 1)] private float maxWindSide = 0.75f;

    void Start()
    {
        // Get main camera reference if not valid
        if(!cam)
            cam = Camera.main;

        rotationDeadAngle = Mathf.Abs(Quaternion.Euler(0, 0, rotationDeadAngle).z);
    }

    void Update()
    {
        WindSFX();
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
        if (Mathf.Abs(cam.transform.localRotation.z) > rotationDeadAngle)
        {
            turnAngle = (cam.transform.localRotation.z - Mathf.Sign(cam.transform.localRotation.z) * rotationDeadAngle) * rotationSensivity;
        }
        else
            turnAngle = 0; 

        // Calculate rotation
        this.transform.RotateAround(newPos, gravityUp, turnAngle * Time.deltaTime);
        this.transform.rotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;
    }

    void WindSFX()
    {
        if (!windSource)
            return;

        float volume = Mathf.Clamp(Mathf.Abs(turnAngle) / maxWindRotationValue, minWindVolume, maxWindVolume);
        windSource.volume = volume;

        float pan = Mathf.Clamp(volume * Mathf.Sign(turnAngle), -maxWindSide, maxWindSide);
        windSource.panStereo = pan;
    }
}
