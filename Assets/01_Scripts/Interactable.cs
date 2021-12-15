namespace GoogleVR.HelloVR
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>Controls interactable teleporting objects in the Demo scene.</summary>
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {

        public virtual void SetGazedAt(bool gazedAt)
        {
            return;
        }

        public virtual void Reset()
        {
            return;
        }

        /// <summary>Calls the Recenter event.</summary>
        public void Recenter()
        {
#if !UNITY_EDITOR
            GvrCardboardHelpers.Recenter();
#else
            if (GvrEditorEmulator.Instance != null)
            {
                GvrEditorEmulator.Instance.Recenter();
            }
#endif  // !UNITY_EDITOR
        }

        public virtual void Interaction(BaseEventData eventData)
        {
            return;
        }

        protected bool IsInteractInput(BaseEventData eventData)
        {
            // Only trigger on left input button, which maps to
            // Daydream controller TouchPadButton and Trigger buttons.
            
            PointerEventData ped = eventData as PointerEventData;
            if (ped == null)
                return false;
            
            return ped.button == PointerEventData.InputButton.Left;
        }

        private void Start()
        {
            SetGazedAt(false);
        }
    }
}