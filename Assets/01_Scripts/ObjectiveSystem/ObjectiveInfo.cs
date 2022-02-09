using UnityEngine;
using TMPro;

[System.Serializable]
public class ObjectiveInfo
{
    [SerializeField] private TextMeshProUGUI informationText;
    [SerializeField] private string additionalInformation;

    private bool isCompleted;

    [SerializeField] private FNarration reminder;
    [SerializeField] private Vector2 reminderTimerRange;
    private float currentReminderTimer;

    NarrationComponent narrationComponent;

    /// <summary> Called in Start function of ObjectiveComponent </summary>
    public void StartFunction()
    {
        // Get narration component
        narrationComponent = GameObject.FindObjectOfType<NarrationComponent>();
        RefreshTimer();
    }

    /// <summary> Is the objective completed </summary>
    public bool IsCompleted
    {
        get { return isCompleted; }
    }

    /// <summary> Complete and crossout objective </summary>
    public void CompleteObjective()
    {
        // If it's already completed
        // Do nothing
        if (isCompleted)
            return;

        isCompleted = true;
        // Strike out the objective's additional information
        SetAdditionalInformation(additionalInformation);
    }

    /// <summary> Set addition information </summary>
    public void SetAdditionalInformation(string info)
    {
        additionalInformation = info;

        // If the objective is completed
        // Add strike out identifier
        if (isCompleted)
            additionalInformation = "<s>" + additionalInformation + "</s>";

        // If no text mesh component is set
        // do nothing
        if (!informationText)
            return;

        // Set visuals
        informationText.text = additionalInformation;
    }

    /// <summary> Ticks timer to give objective reminder </summary>
    public void TickReminder()
    {
        // If there's no reminder clip
        // Do nothing
        if (!reminder.clip)
            return;

        // If the object is already completed
        // Do nothing
        if (isCompleted)
            return;

        // If the timer is still greater than zero
        // Tick it
        if (currentReminderTimer > 0)
        {
            currentReminderTimer -= Time.deltaTime;
            return;
        }

        // Null ref protection
        if (!narrationComponent)
        {
            Debug.LogWarning("Missing narrative component reference in ObjectiveInfo.");
            return;
        }

        // Wait for the narrative component to have finished playing
        if (!narrationComponent.HasFinishedPlaying())
            return;

        // Play reminder narration
        narrationComponent.PlayNarration(reminder);

        RefreshTimer();
    }

    /// <summary> Reset currentReminderTimer to original value </summary>
    public void RefreshTimer()
    {
        currentReminderTimer = Random.Range(reminderTimerRange.x, reminderTimerRange.y);
    }
}

