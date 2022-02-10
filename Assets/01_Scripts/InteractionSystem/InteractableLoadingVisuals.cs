using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Interactable))] 
public class InteractableLoadingVisuals : MonoBehaviour
{
    [SerializeField] private Interactable interactable;

    [Space(10)]
    [SerializeField] private Image loadingImage;
    [SerializeField] private Canvas loadingCanvas;

    // Start is called before the first frame update
    void Start()
    {
        AssignCallbacks();
    }

    void AssignCallbacks()
    {
        if (!interactable)
        {
            interactable = this.GetComponent<Interactable>();
            if (!interactable)
            {
                Debug.LogWarning("Couldn't find valid reference of Interactable script.", this);
                return;
            }
        }

        interactable.OnLoadingInteraction += LoadingVisuals;
        interactable.OnGazedAt += GazedAtVisuals;
    }

    void GazedAtVisuals(bool gazedAt)
    {
        loadingCanvas.gameObject.SetActive(gazedAt);
    }

    void LoadingVisuals()
    {
        if (!interactable)
        {
            Debug.LogWarning("Missing interactable script reference.", this);
            return;
        }

        if (!loadingImage)
        {
            Debug.LogWarning("Missing loading image reference.", this);
            return;
        }

        if (!loadingCanvas)
        {
            Debug.LogWarning("Missing loading canvas reference.", this);
            return;
        }

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
