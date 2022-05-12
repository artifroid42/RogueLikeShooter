using UnityEngine;

namespace RLS.Gameplay.PlayerController
{
    public class PlayerMovementController : MonoBehaviour, IPlayerInputsObserver
    {
        [Header("Refs")]
        [SerializeField]
        private CharacterController m_characterController;
        [SerializeField]
        private Transform m_cameraTarget;

        [Header("Params")]
        [SerializeField]
        private float m_speed = 5f;
        [SerializeField]
        private Vector2 m_cameraSensitivity = new Vector2(60f, 60f);

        private Vector3 m_movementDirection = default;
        private Vector2 m_lookAroundValues = default;

        private void Start()
        {
            GetComponent<PlayerInputsHandler>()?.RegisterNewObserver(this);
        }

        private void OnDestroy()
        {
            GetComponent<PlayerInputsHandler>()?.UnregisterObserver(this);
        }

        public void HandleMovementInput(Vector2 a_moveInputs) 
        {
            m_movementDirection = new Vector3(a_moveInputs.x, 0f, a_moveInputs.y);
            if(m_movementDirection.sqrMagnitude > 1f)
            {
                m_movementDirection.Normalize();
            }
        }

        private void apply_movement()
        {
            Vector3 movementToApply = default;

            movementToApply += m_movementDirection * m_speed;


            m_characterController.Move(movementToApply * Time.deltaTime);
        }

        private void apply_rotation()
        {
            transform.Rotate(Vector3.up * m_lookAroundValues.x * m_cameraSensitivity.x * Time.deltaTime);
            m_cameraTarget.Rotate(Vector3.right * m_lookAroundValues.y * m_cameraSensitivity.y * Time.deltaTime);
        }

        private void Update()
        {
            apply_rotation();
            apply_movement();
        }
    }
}