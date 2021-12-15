using System;

namespace GoogleVR.HelloVR
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>Controls interactable teleporting objects in the Demo scene.</summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(EventTrigger))]
    public class Interactable : MonoBehaviour
    {
        [Header("Interaction Loading")]
        [SerializeField] protected float interactionLoadTime = 1.0f; // Time to load interaction function
        protected float currentInteractionLoadTime;
        private bool loadingInteraction = false;
        
        [Header("Visual Components")]
        [SerializeField] private Image loadImage;
        [SerializeField] private Canvas loadCanvas;


        public virtual void SetGazedAt(bool gazedAt)
        {
            loadingInteraction = gazedAt;
            if (loadCanvas) loadCanvas.gameObject.SetActive(gazedAt);
        }

        public virtual void Reset()
        {
            return;
        }

        /// <summary>Calls the Recenter event.</summary>
        public void Recenter()
        {
#if !UNITY_EDITOR
            GvrCardboardHelpers.Recenter();
#else
            if (GvrEditorEmulator.Instance != null)
            {
                GvrEditorEmulator.Instance.Recenter();
            }
#endif  // !UNITY_EDITOR
        }

        public virtual void OnInteraction(BaseEventData eventData)
        {
            return;
        }

        protected bool IsInteractInput(BaseEventData eventData)
        {
            // Only trigger on left input button, which maps to
            // Daydream controller TouchPadButton and Trigger buttons.
            
            PointerEventData ped = eventData as PointerEventData;
            if (ped == null)
                return false;
            
            return ped.button == PointerEventData.InputButton.Left;
        }

        private void Awake()
        {
            SetupInputEvents();
        }

        protected virtual void Start()
        {
            SetGazedAt(false);
            currentInteractionLoadTime = interactionLoadTime;
        }
        
        private void Update()
        {
            LoadInteraction();
        }
        
        private void LoadInteraction()
        {
            if (!loadingInteraction)
                return;

            currentInteractionLoadTime -= Time.deltaTime;
            
            if(loadImage) loadImage.fillAmount = currentInteractionLoadTime / interactionLoadTime;
            if (loadCanvas) loadCanvas.transform.LookAt(Camera.main.transform);
        }
        
        public bool CanInteract()
        {
            return currentInteractionLoadTime <= 0;
        }

        private void SetupInputEvents()
        {
            EventTrigger eventTrigger = this.GetComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => {SetGazedAt(true);});
            eventTrigger.triggers.Add(entry);
            
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => {SetGazedAt(false);});
            eventTrigger.triggers.Add(entry);
            
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => {OnInteraction(data);});
            eventTrigger.triggers.Add(entry);
            
            Debug.Log("Setup");
        }
    }
}