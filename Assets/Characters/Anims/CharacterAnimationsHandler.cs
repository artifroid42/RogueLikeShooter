using UnityEngine;

namespace RLS.Character.Animations
{
    public class CharacterAnimationsHandler : MonoBehaviour
    {
        private int RIGHT_VALUE = Animator.StringToHash("RightValue");
        private int FORWARD_VALUE = Animator.StringToHash("ForwardValue");

        [SerializeField]
        protected Animator m_animator = null;
        [SerializeField]
        private float m_movementSmoothness = 6f;
        [SerializeField]
        private float m_maxSpeed = 5f;

        private Vector3 m_deltaPos = default;
        private Vector3 m_lastPos = default;

        protected virtual void Update()
        {
            update_pos_values();

            apply_movement_anims();
        }

        private void apply_movement_anims()
        {
            if (Time.timeScale == 0) return;

            m_animator.SetFloat(RIGHT_VALUE, (3 * m_deltaPos.x) / (m_maxSpeed * Time.deltaTime));
            m_animator.SetFloat(FORWARD_VALUE, (3 * m_deltaPos.z) / (m_maxSpeed * Time.deltaTime));
        }

        private void update_pos_values()
        {
            if (Time.timeScale == 0) return;

            m_deltaPos = transform.InverseTransformVector(transform.position - m_lastPos);

            Debug.Log($"{m_deltaPos}");
            m_lastPos = transform.position;
        }
    }
}