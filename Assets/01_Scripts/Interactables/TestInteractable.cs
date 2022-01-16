using UnityEngine;
using UnityEngine.EventSystems;

public class TestInteractable : Interactable
{
    [Header("Interaction")]
    [SerializeField] private float destroyTimer = 0;
    [SerializeField] private Object[] objsToDestroy;
    
    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        GameObject.FindObjectOfType<ScoreScript>().ChangeScore(+1);
        
        foreach (Object obj in objsToDestroy)
        {
            Destroy(obj, destroyTimer);
        }
        
        Destroy(this);
        
        base.OnInteraction(eventData);
    }
}
