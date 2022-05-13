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
        [SerializeField]
        private float m_jumpCooldown = 1f;
        private float m_lastJumpTime = 0f;

        private Vector3 m_movementDirection = default;
        private Vector2 m_lookAroundValues = default;

        [Header("Custom Physics Params")]
        [SerializeField]
        private float m_gravity = 9.81f;
        [SerializeField]
        private float m_jumpSpeed = 6f;
        private bool m_isGrounded = false;

        private float m_verticalVelocity = 0f;

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
            m_movementDirection = transform.forward * a_moveInputs.y + transform.right * a_moveInputs.x;
            if(m_movementDirection.sqrMagnitude > 1f)
            {
                m_movementDirection.Normalize();
            }
        }

        public void HandleLookAroundInput(Vector2 a_lookAroundInputs) 
        {
            m_lookAroundValues = a_lookAroundInputs;
        }

        public void HandleJumpInput() 
        { 
            if(Time.time - m_lastJumpTime > m_jumpCooldown && m_isGrounded)
            {
                Jump();
                m_lastJumpTime = Time.time;
            }
        }

        private void check_if_grounded()
        {
            m_isGrounded = m_characterController.isGrounded;
        }

        private void apply_movement()
        {
            if(m_isGrounded && m_verticalVelocity < 0f)
            {
                m_verticalVelocity = -1f;
            }
            else
            {
                m_verticalVelocity -= m_gravity * Time.deltaTime;
            }


            Vector3 movementToApply = default;

            movementToApply += m_movementDirection * m_speed;

            movementToApply += m_verticalVelocity * Vector3.up;
            
            m_characterController.Move(movementToApply * Time.deltaTime);
        }

        private void apply_rotation()
        {
            transform.Rotate(Vector3.up * m_lookAroundValues.x * m_cameraSensitivity.x * Time.deltaTime);
            m_cameraTarget.Rotate(Vector3.right * -m_lookAroundValues.y * m_cameraSensitivity.y * Time.deltaTime);
        }

        private void Update()
        {
            check_if_grounded();
            apply_rotation();
            apply_movement();
            
        }

        #region Specific Generic Actions
        protected void Jump()
        {
            m_verticalVelocity = m_jumpSpeed;
        }
        #endregion
    }
}