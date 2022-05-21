using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerMovementController : MonoBehaviour, IPlayerInputsObserver
    {
        [Header("Refs")]
        [SerializeField]
        private CharacterController m_characterController;
        [SerializeField]
        private Transform m_cameraTarget;
        [SerializeField]
        private Transform m_topHead = null;

        public Transform CameraTarget => m_cameraTarget;

        [Header("Gameplay Params")]
        [SerializeField]
        private float m_speed = 5f;
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
        private bool m_isHeadTouchingCeiling = false;

        private float m_verticalVelocity = 0f;

        [Header("Camera Settings")]
        [SerializeField]
        private Vector2 m_cameraSensitivity = new Vector2(60f, 60f);
        [SerializeField]
        private Vector2 m_cameraClamping = new Vector2(-60, 60);
        private float m_yCamValue = 0f;

        private bool m_canMove = false;

        private void Start()
        {
            GetComponent<PlayerInputsHandler>()?.RegisterNewObserver(this);
        }

        private void OnDestroy()
        {
            GetComponent<PlayerInputsHandler>()?.UnregisterObserver(this);
        }

        public void ActivateMovement()
        {
            m_verticalVelocity = 0f;
            m_canMove = true;
            Debug.LogError("Activate Movements");
        }

        public void DeactivateMovement()
        {
            m_canMove = false;
            Debug.LogError("Deactivate Movements");
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
            if (!m_canMove) return;

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
            if(m_isHeadTouchingCeiling)
            {
                if(m_verticalVelocity > 0f)
                {
                    m_verticalVelocity = 0f;
                }
                
            }
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
            transform.Rotate(Vector3.up, m_lookAroundValues.x * m_cameraSensitivity.x * Time.deltaTime);


            Debug.Log(m_yCamValue);
           
            m_yCamValue += -m_lookAroundValues.y * m_cameraSensitivity.y * Time.deltaTime;
            m_yCamValue = Mathf.Clamp(m_yCamValue, m_cameraClamping.x, m_cameraClamping.y);
            m_cameraTarget.rotation = Quaternion.LookRotation(Mathf.Sin(-m_yCamValue * Mathf.Deg2Rad) * transform.up + Mathf.Cos(-m_yCamValue * Mathf.Deg2Rad) * transform.forward);
        }

        private void Update()
        {
            check_if_grounded();
            if (m_canMove)
            {
                apply_rotation();
                apply_movement();
            }
        }

        private void FixedUpdate()
        {
            check_if_touched_ceiling();
        }

        #region Specific Generic Actions
        protected void Jump()
        {
            m_verticalVelocity = m_jumpSpeed;
        }
        #endregion

        #region Environment Checking

        private void check_if_touched_ceiling()
        {
            m_isHeadTouchingCeiling = false;
            if (Physics.Raycast(m_topHead.position + Vector3.down * 0.5f, Vector3.up, 0.6f))
            {
                m_isHeadTouchingCeiling = true;
                //Debug.DrawLine(m_topHead.position + Vector3.down * 0.5f, m_topHead.position + Vector3.down * 0.5f + Vector3.up * 0.6f, Color.red);
            }
            /*else
            {
                Debug.DrawLine(m_topHead.position + Vector3.down * 0.5f, m_topHead.position + Vector3.down * 0.5f + Vector3.up * 0.6f, Color.green);

            }*/
        }


        #endregion
    }
}