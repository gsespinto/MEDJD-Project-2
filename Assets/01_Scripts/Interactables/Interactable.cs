    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    /// <summary>Controls interactable teleporting objects in the Demo scene.</summary>
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(EventTrigger))]
    public class Interactable : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] AudioSource audioSource;
        [SerializeField] private AudioClip[] onEnterSFXs;
        [SerializeField] private AudioClip[] onInteractionSFXs;

        [Header("Setup")]
        [SerializeField] private bool setupEventTriggers = true;

        [Header("Interaction Loading")]
        [SerializeField] protected float interactionLoadTime = 1.0f; // Time to load interaction function
        protected float currentInteractionLoadTime; // Current load time
        private bool loadingInteraction = false; // Is the interactable loading
        
        [Header("Visual Components")]
        [SerializeField] private Image loadImage;
        [SerializeField] private Canvas loadCanvas;

        private void Awake()
        {
            if (setupEventTriggers)
                SetupInputEvents();
        }

        protected virtual void Start()
        {
            SetGazedAt(false);
            currentInteractionLoadTime = interactionLoadTime;
            audioSource = this.GetComponent<AudioSource>();
        }
        
        private void Update()
        {
            LoadInteraction();
        }
        
        // Set if the interactable is gazed at
        protected virtual void SetGazedAt(bool gazedAt)
        {
            // Engages interaction loading
            loadingInteraction = gazedAt;

            // Enables and disables interaction visuals
            if (loadCanvas) loadCanvas.gameObject.SetActive(gazedAt);
        }

        /// <summary> Called when the player interacts with this object </summary>
        public virtual void OnInteraction(BaseEventData eventData)
        {
            PlaySFX(onInteractionSFXs);
            return;
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
        public virtual bool CanInteract()
        {
            return currentInteractionLoadTime <= 0;
        }

        /// <summary> Plays given audio clip once </summary>
        private void PlaySFX(AudioClip clip)
        {
            // Null ref protection
            if(!audioSource || !clip)
                return;

            // Play SFX
            audioSource.PlayOneShot(clip);
        }

        /// <summary> Plays given audio clip once </summary>
        private void PlaySFX(AudioClip[] clips)
        {
            // Null ref protection
            if(clips.Length <= 0)
                return;

            // Play SFX
            PlaySFX(clips[Random.Range(0, clips.Length - 1)]);
        }

        /// <summary> Sets input events to handle make this object interactable </summary>
        private void SetupInputEvents()
        {
            EventTrigger eventTrigger = this.GetComponent<EventTrigger>();


            // Set up on pointer enter event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((data) => {SetGazedAt(true);});
            entry.callback.AddListener((data) => {PlaySFX(onEnterSFXs);});
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
        }
    }