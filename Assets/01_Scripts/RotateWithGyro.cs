using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithGyro : MonoBehaviour
{
    [SerializeField] private Vector2 mouseSensivity = new Vector2(1.0f, 1.0f);
    private GyroManager gyroManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gyroManager = GameObject.FindObjectOfType<GyroManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }
    
    void RotateCamera()
    {
        #region PC test controls
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");
        
        Vector3 rotateValue = new Vector3(x * mouseSensivity.x, y * -1 * mouseSensivity.y, 0);
        
        this.transform.Rotate(this.transform.right, -rotateValue.x);
        this.transform.Rotate(Vector3.up, -rotateValue.y);
        #endregion

        // Null references protection
        if (!gyroManager || !gyroManager.HasGyro())
            return;

        // Make camera rotate with mobile device's rotation
        this.transform.localRotation = gyroManager.GetGyroRotation();
    }
}
