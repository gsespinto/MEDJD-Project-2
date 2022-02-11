using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))] 
public class InteractableLoadingVisuals : MonoBehaviour
{
    /// <summary> Interactable to add the effect to </summary>
    [SerializeField] private Interactable interactable;

    [Space(10)]
    [SerializeField] private Image loadingImage;
    [SerializeField] private Canvas loadingCanvas;

    void Awake()
    {
        AssignCallbacks();
    }

    /// <summary> Assings functions to the correspondent interactable delegates </summary>
    void AssignCallbacks()
    {
        // Get interactable ref if not set yet
        if (!interactable)
        {
            interactable = this.GetComponent<Interactable>();

            // Null ref protection
            if (!interactable)
            {
                Debug.LogWarning("Couldn't find valid reference of Interactable script.", this);
                return;
            }
        }

        // Assign functions to the delegates
        interactable.OnLoadingInteraction += LoadingVisuals;
        interactable.OnGazedAt += GazedAtVisuals;
    }

    /// <summary> Show the loading canvas whenever the interactable is gazed at, if not hide it </summary>
    void GazedAtVisuals(bool gazedAt)
    {
        loadingCanvas.gameObject.SetActive(gazedAt);
    }


    /// <summary> Fill image to show the interactables loading ratio </summary>
    void LoadingVisuals()
    {
        // Null ref protection
        if (!interactable)
        {
            Debug.LogWarning("Missing interactable script reference.", this);
            return;
        }

        // Null ref protection
        if (!loadingImage)
        {
            Debug.LogWarning("Missing loading image reference.", this);
            return;
        }

        // Null ref protection
        if (!loadingCanvas)
        {
            Debug.LogWarning("Missing loading canvas reference.", this);
            return;
        }

        // Null ref protection
        if (interactable.GetLoadingRatio() <= 0)
        {
            GazedAtVisuals(false);
            return;
        }

        // Handle interaction loading visuals
        loadingImage.fillAmount = interactable.GetLoadingRatio();
        loadingCanvas.transform.LookAt(Camera.main.transform);
    }
}
