using UnityEngine;

namespace RLS.Generic.Ragdoll
{
    public class SyncCloneFromModel : MonoBehaviour
    {
        [SerializeField]
        private Transform m_model = null;
        [SerializeField]
        private Transform m_clone = null;

        public void ApplyModelPositionsToClone()
        {
            apply_transform_from_model_to_clone(m_model, m_clone);
        }

        private void apply_transform_from_model_to_clone(Transform a_model, Transform a_clone)
        {
            for(int i = 0; i < a_model.childCount; ++i)
            {
                apply_transform_from_model_to_clone(a_model.GetChild(i), a_clone.GetChild(i));
            }

            a_clone.localPosition = a_model.localPosition;
            a_clone.localRotation = a_model.localRotation;
            a_clone.localScale = a_model.localScale;
        }
    }
}