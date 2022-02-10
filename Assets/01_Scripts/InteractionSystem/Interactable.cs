using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MyDelegates;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(EventTrigger))]
public class Interactable : MonoBehaviour
{
  #region Variables
    [Header("Setup")]
    [SerializeField] private bool setupEventTriggers = true; // Set interaction event triggers through code?

    [Header("Interaction Loading")]
    [SerializeField] protected float interactionLoadTime = 1.0f; // Time to load interaction function
    protected float currentInteractionLoadTime; // Current load time
    private bool isLoadingInteraction = false; // Is the interactable loading
    [SerializeField] private bool rechargeInteraction = false; // Is the load time supposed to reset after interaction?
  #endregion

    public NoParamsDelegate OnInteraction;
    public BoolParamDelegate OnGazedAt;
    public NoParamsDelegate OnLoadingInteraction;

    private void Awake()
    {
        if (setupEventTriggers)
            SetupInputEvents();
    }

    protected virtual void Start()
    {
        SetGazedAt(false);
        RechargeInteraction();
    }

    protected virtual void Update()
    {
        LoadInteraction();
    }

    ///<summary> Set if the interactable is gazed at </summary>
    protected virtual void SetGazedAt(bool gazedAt)
    {
        // Engages interaction loading
        isLoadingInteraction = gazedAt;
        OnGazedAt?.Invoke(gazedAt);
    }

    /// <summary> Called when the player interacts with this object </summary>
    public virtual void Interact()
    {
        if (!CanInteract())
            return;

        OnInteraction?.Invoke();
        RechargeInteraction();
    }

    /// <summary> If the interaction is loading, decreases load time </summary>
    private void LoadInteraction()
    {
        // If the interaction isn't loading
        // Or is loaded
        // do nothing
        if (!isLoadingInteraction || currentInteractionLoadTime < -0.1f)
            return;

        // Decrease interaction load time
        currentInteractionLoadTime -= Time.deltaTime;
        OnLoadingInteraction?.Invoke();
    }

    /// <summary> Returns true if the interactable is done loading </summary>
    public virtual bool CanInteract()
    {
        return currentInteractionLoadTime <= 0;
    }

    /// <summary> Sets input events to handle make this object interactable </summary>
    private void SetupInputEvents()
    {
        EventTrigger eventTrigger = this.GetComponent<EventTrigger>();

        // Set up on pointer enter event
        EventTrigger.Entry entry = new EventTrigger.Entry{
            eventID = EventTriggerType.PointerEnter
        };
        entry.callback.AddListener((data) => { SetGazedAt(true); });
        eventTrigger.triggers.Add(entry);

        // Set up on pointer exit event
        entry = new EventTrigger.Entry{
            eventID = EventTriggerType.PointerExit
        };
        entry.callback.AddListener((data) => { SetGazedAt(false); });
        eventTrigger.triggers.Add(entry);

        // Set up pointer click event
        entry = new EventTrigger.Entry{
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((data) => { Interact(); });
        eventTrigger.triggers.Add(entry);
    }

    public float GetLoadingRatio()
    {
        return currentInteractionLoadTime / interactionLoadTime;
    }

    protected void RechargeInteraction()
    {
        // Recharge load time if it's supposed to
        if (rechargeInteraction)
        {
            currentInteractionLoadTime = interactionLoadTime;
            isLoadingInteraction = false;
        }
    }
}