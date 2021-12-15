namespace GoogleVR.HelloVR
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class TransitionInteractable : Interactable
    {
        [Header("References")]
        [SerializeField] private GameObject oldPlayerController;
        [SerializeField] private GameObject newPlayerController;
        [SerializeField] private Transform newCameraContainer;

        protected override void Start()
        {
            base.Start();
            
            oldPlayerController.SetActive(true);
            newPlayerController.SetActive(false);
        }
        
        public override void OnInteraction(BaseEventData eventData)
        {
            if (!CanInteract())
                return;

            base.OnInteraction(eventData);
            if (!IsInteractInput(eventData))
                return;

            newPlayerController.SetActive(true);
            Camera.main.transform.SetParent(newCameraContainer, true);
            
            Destroy(oldPlayerController);
            Destroy(this.gameObject);
        }
    }
}
