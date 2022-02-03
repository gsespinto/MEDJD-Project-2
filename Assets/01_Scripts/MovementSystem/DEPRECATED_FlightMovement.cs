using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEPRECATED_FlightMovement : MonoBehaviour
{
 #region Variables
    // ---------------- COMPONENTS ----------------- //

    [Header("Components")]
    [SerializeField] private Camera cam;

    // ---------------- MOVEMENT ----------------- //

    [Header("Movement Values")]
    [SerializeField] private float flyHeight;

    [SerializeField] private float movementSpeed = 1.0f;
    private float currentMovementSpeed;
    [SerializeField] private float acceleration = 1.0f;

    [SerializeField] private float rotationSensivity = 0.1f;
    [SerializeField] private float rotationDeadAngle = 10f;
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

        rotationDeadAngle = Mathf.Abs(Quaternion.Euler(0, 0, rotationDeadAngle).z);
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
        if (currentMovementSpeed >= movementSpeed)
            return;
        
        // Increase movement speed according to acceleration
        currentMovementSpeed += acceleration * Time.deltaTime;
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
        Vector3 newPos = this.transform.position + this.transform.forward * currentMovementSpeed * Time.deltaTime;
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
