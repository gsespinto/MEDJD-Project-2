using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroManager : MonoBehaviour
{
    [Header("Logic")] 
    [SerializeField] private Quaternion gyroRotOffset = new Quaternion(0, 0, 1, 0);
    private Gyroscope gyro; // Reference to gyroscope component of mobile devide
    private Quaternion rotation; // Current roation of the device

    private void Awake()
    {
        EnableGyro();
    }

    /// <summary> Gets referece to gyroscope component of the device, if it's valid, enables the usage of its rotation </summary>
    public void EnableGyro()
    {
        if (gyro != null)
            return;

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }
    }

    private void Update()
    {
        // If the gyroscope is valid, set device rotation to gyroscopes value times the offset
        if (gyro != null)
        {
            rotation = gyro.attitude * gyroRotOffset;
        }
    }

    /// <summary> Returns the offseted rotation of the mobile device </summary>
    public Quaternion GetGyroRotation()
    {
        return rotation;
    }

    /// <returns> True if the device supports gyroscope </returns>
    public bool HasGyro()
    {
        return gyro != null;
    }
}
