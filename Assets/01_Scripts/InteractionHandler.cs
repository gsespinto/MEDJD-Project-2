using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject camera;

    [Header("Detection")] 
    [SerializeField] private float interactionSpherecastLength = 1000.0f;
    [SerializeField] private float interactionSpherecastRadius = 5.0f;

    private Interactable currentInteractable;

    // Update is called once per frame
    void Update()
    {
        CheckInteractable();
        InteractionInput();
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

    void InteractionInput()
    {
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
    
    /// <summary> Call interaction function of the interactable in line sight </summary>
    void Interact()
    {
        if (!currentInteractable)
            return;
        
        currentInteractable.Interaction();
    }
}
