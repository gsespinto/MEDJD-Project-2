using UnityEngine;
using UnityEngine.EventSystems;

public class BirdInteractable : Interactable
{
    [Header("Interaction")]
    [SerializeField] private Object[] objsToDestroy;
    private ChildScript childScript;

    protected override void Start()
    {
        base.Start();

        childScript = GameObject.FindObjectOfType<ChildScript>();
    }

    protected override void SetGazedAt(bool gazedAt)
    {
        if (!childScript || !childScript.HasFruit)
        {
            base.SetGazedAt(false);
            return;
        }

        base.SetGazedAt(gazedAt);
    }

    public override bool CanInteract()
    {
        if (!childScript)
            return false;

        return currentInteractionLoadTime <= 0 && childScript.HasFruit;
    }

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        GameObject.FindObjectOfType<ScoreScript>().ChangeScore(+1);
        childScript.HasFruit = false;
        Debug.Log(childScript.HasFruit);

        foreach (Object obj in objsToDestroy)
        {
            Destroy(obj);
        }

        Destroy(this);

        base.OnInteraction(eventData);
    }
}
