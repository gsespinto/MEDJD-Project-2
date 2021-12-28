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
            if(!narrationComponent || narrations.Length <= 0)
                return;

            if (!CanInteract())
                return;

            foreach (FNarration n in narrations)
            {
                narrationComponent.PlayNarration(n);
            }

            base.OnInteraction(eventData);

            Destroy(this);
        }
    }
}