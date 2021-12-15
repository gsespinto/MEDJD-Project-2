using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] private Camera camera;
    private GyroManager gyroManager;
    
    [Header("Movement")] 
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private Vector2 cameraRotSensivity = new Vector2(1.0f, 1.0f);

    [Header("Interaction")] 
    [SerializeField] private float interactionSpherecastLength = 1000.0f;
    [SerializeField] private float interactionSpherecastRadius = 5.0f;

    private Interactable currentInteractable;


    private void Start()
    {
        gyroManager = GameObject.FindObjectOfType<GyroManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        RotateCamera();
        CheckInteractable();

#region PC test controls
        if (Input.GetButtonDown("Interact"))
            Interact();
#endregion        
        
        // If the player touches the device's screen, call interaction function
        if (Input.touches.Length > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                Interact();
        }
    }

    /// <summary> Move character forward </summary>
    void Move()
    {
        Vector3 position = this.transform.position;
        position.z += movementSpeed * Time.deltaTime;
        this.transform.position = position;
    }

    /// <summary> Handle camera roation input </summary>
    void RotateCamera()
    {
        
#region PC test controls
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");
        
        Vector3 rotateValue = new Vector3(x * cameraRotSensivity.x, y * -1 * cameraRotSensivity.y, 0);
        
        camera.transform.Rotate(camera.transform.right, -rotateValue.x);
        camera.transform.Rotate(Vector3.up, -rotateValue.y);
#endregion

        // Null references protection
        if (!gyroManager || !gyroManager.HasGyro())
            return;

        Debug.Log(gyroManager.GetGyroRotation());
        
        // Make camera rotate with mobile device's rotation
        camera.transform.localRotation = gyroManager.GetGyroRotation();
    }

    /// <summary> Checks if there's an interactable in line sight, and if ticks its interaction load time </summary>
    void CheckInteractable()
    {
        RaycastHit hit;
        Ray ray = new Ray(camera.transform.position + camera.transform.forward* interactionSpherecastRadius, camera.transform.forward);
        Debug.DrawRay(camera.transform.position + camera.transform.forward * interactionSpherecastRadius, camera.transform.forward * interactionSpherecastLength, Color.red);
        
        if (Physics.SphereCast(ray, interactionSpherecastRadius, out hit, interactionSpherecastLength))
        {
            if (currentInteractable && (currentInteractable != hit.collider.GetComponent<Interactable>()))
            {
                currentInteractable.StopLoadingInteraction();
            }
            
            currentInteractable = hit.collider.GetComponent<Interactable>();
        }

        if (!currentInteractable)
            return;

        currentInteractable.LoadInteraction();
    }

    /// <summary> Call interaction function of the interactable in line sight </summary>
    void Interact()
    {
        if (!currentInteractable)
            return;
        
        currentInteractable.Interaction();
    }
}
