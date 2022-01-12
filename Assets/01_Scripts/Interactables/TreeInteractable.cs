using UnityEngine;
using UnityEngine.EventSystems;

public class TreeInteractable : Interactable
{

    [Header("Interaction")]
    [SerializeField] private Object[] objsToDestroy;
    [SerializeField] private AudioClip shoutClip;

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        GameObject.FindObjectOfType<ScoreScript>().ChangeScore(+1);
        if(shoutClip)
            GameObject.FindObjectOfType<NarrationComponent>().PlaySFX(shoutClip);

        foreach (Object obj in objsToDestroy)
        {
            Destroy(obj);
        }

        Destroy(this);

        base.OnInteraction(eventData);
    }
}
