using UnityEngine;
using UnityEngine.EventSystems;

public class TreeInteractable : Interactable
{

    [Header("Interaction")]
    [SerializeField] private Object[] objsToDestroy;
    [SerializeField] private AudioClip[] shoutClips;

    public override void OnInteraction(BaseEventData eventData)
    {
        if (!CanInteract())
            return;

        GameObject.FindObjectOfType<ScoreScript>().ChangeScore(+1);
        if(shoutClips.Length >= 0)
            GameObject.FindObjectOfType<NarrationComponent>().PlaySFX(shoutClips[Random.Range(0, shoutClips.Length)]);

        foreach (Object obj in objsToDestroy)
        {
            Destroy(obj);
        }

        Destroy(this);

        base.OnInteraction(eventData);
    }
}
