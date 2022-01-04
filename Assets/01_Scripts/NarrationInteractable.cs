using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GoogleVR.HelloVR
{
    public class NarrationInteractable : Interactable
    {
        [SerializeField] private FNarration[] narrations;
        NarrationComponent narrationComponent;

        protected override void Start()
        {
            base.Start();
            narrationComponent = GameObject.FindObjectOfType<NarrationComponent>();
        }

        public override void OnInteraction(BaseEventData eventData)
        {
            // null ref protection
            if(!narrationComponent || narrations.Length <= 0)
                return;

            // Only interact if the interaction is loaded
            if (!CanInteract())
                return;

            // Add the narrations to the queue
            foreach (FNarration n in narrations)
            {
                narrationComponent.PlayNarration(n);
            }

            base.OnInteraction(eventData);
            
            // Destroy this script
            Destroy(this);
        }
    }
}