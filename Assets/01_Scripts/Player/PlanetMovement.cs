﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour
{
 #region Variables
    // ---------------- COMPONENTS ----------------- //

    [Header("Components")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform environment;

    // ---------------- MOVEMENT ----------------- //

    [Header("Movement Values")]
    [SerializeField, Min(0)] private float environmentRotationSpeed = 1.0f;
    private float environmentCurrentSpeed;
    [SerializeField] private float acceleration = 1.0f;

    [SerializeField] private float turnSensivity = 0.1f;
    [SerializeField] private float turnDeadAngle = 10f;
    private float turnAngle;

    // ---------------- SFX ----------------- //

    [Header("Audio")]
    [SerializeField] private AudioSource windSource;

    [SerializeField, Range(0, 1)] private float minWindVolume = 0f;
    [SerializeField, Range(0, 1)] private float maxWindVolume = 1f;

    [SerializeField, Range(0, 90)] private float maxWindRotationValue = 75.0f;
    [SerializeField, Range(0, 1)] private float maxWindSide = 0.75f;
 #endregion

    void Start()
    {
        // Get main camera reference if not valid
        if(!cam)
            cam = Camera.main;

        // Set turn dead angle to quaternion value
        turnDeadAngle = Mathf.Abs(Quaternion.Euler(0, 0, turnDeadAngle).z);

        // Get movement values as fractions
        environmentRotationSpeed /= 100;
        acceleration /= 100;
    }

    void Update()
    {
        WindSFX();
        Accelerate();
    }

    /// <summary> Increases movement speed until it achieves its target value </summary>
    void Accelerate()
    {
        // If the current speed has achieved it target value
        // Do nothing
        if (environmentCurrentSpeed >= environmentRotationSpeed)
            return;
        
        // Increase movement speed according to acceleration
        environmentCurrentSpeed += acceleration * Time.deltaTime;
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
        
        // Calculate turn angle according with z rotation of camera
        if (Mathf.Abs(cam.transform.localRotation.z) > turnDeadAngle)
        {
            turnAngle = (cam.transform.localRotation.z - Mathf.Sign(cam.transform.localRotation.z) * turnDeadAngle) * turnSensivity;
        }
        else
            turnAngle = 0; 

        // Rotate controller with turn angle
        this.transform.RotateAround(transform.position, this.transform.up, turnAngle * Time.deltaTime);

        // Rotate world around controller's right vector with current speed
        environment.Rotate(-transform.right, environmentCurrentSpeed, Space.World);
    }

    void WindSFX()
    {
        // Null ref protection
        if (!windSource)
            return;

        // Set volume according to turn angle
        float volume = Mathf.Clamp(Mathf.Abs(turnAngle) / maxWindRotationValue, minWindVolume, maxWindVolume);
        windSource.volume = volume;

        // Set pan value according to volume and turn angle
        float pan = Mathf.Clamp(volume * Mathf.Sign(turnAngle), -maxWindSide, maxWindSide);
        windSource.panStereo = pan;
    }
}