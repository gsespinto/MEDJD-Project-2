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
        protected float currentInteractionLoadTime; // Current load time
        private bool loadingInteraction = false; // Is the interactable loading
        
        [Header("Visual Components")]
        [SerializeField] private Image loadImage;
        [SerializeField] private Canvas loadCanvas;

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
        
        // Set if the interactable is gazed at
        public virtual void SetGazedAt(bool gazedAt)
        {
            // Engages interaction loading
            loadingInteraction = gazedAt;

            // Enables and disables interaction visuals
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

        /// <summary> Called when the player interacts with this object </summary>
        public virtual void OnInteraction(BaseEventData eventData)
        {
            return;
        }

        /// <summary> Returns true with the interact input was given </summary>
        protected bool IsInteractInput(BaseEventData eventData)
        {
            // Only trigger on left input button, which maps to
            // Daydream controller TouchPadButton and Trigger buttons.
            
            PointerEventData ped = eventData as PointerEventData;
            if (ped == null)
                return false;
            
            return ped.button == PointerEventData.InputButton.Left;
        }

        /// <summary> If the interaction is loading, decreases load time </summary>
        private void LoadInteraction()
        {
            // If the interaction isn't loading
            // do nothing
            if (!loadingInteraction)
                return;

            // Decrease interaction load time
            currentInteractionLoadTime -= Time.deltaTime;
            
            // Handle interaction loading visuals
            if(loadImage) loadImage.fillAmount = currentInteractionLoadTime / interactionLoadTime;
            if (loadCanvas) loadCanvas.transform.LookAt(Camera.main.transform);
        }
        
        /// <summary> Returns true if the interactable is done loading </summary>
        public bool CanInteract()
        {
            return currentInteractionLoadTime <= 0;
        }

        /// <summary> Sets input events to handle make this object interactable </summary>
        private void SetupInputEvents()
        {
            EventTrigger eventTrigger = this.GetComponent<EventTrigger>();

            // Set up on pointer enter event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => {SetGazedAt(true);});
            eventTrigger.triggers.Add(entry);
            
            // Set up on pointer exit event
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((data) => {SetGazedAt(false);});
            eventTrigger.triggers.Add(entry);
            
            // Set up pointer click event
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => {OnInteraction(data);});
            eventTrigger.triggers.Add(entry);
            
            // Debug.Log("Interactable set up.", this.gameObject);
        }
    }
}