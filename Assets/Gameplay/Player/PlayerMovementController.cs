using System.Collections;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerMovementController : MonoBehaviour, IPlayerInputsObserver
    {
        public virtual float PowerCooldownRatio => 0;

        [Header("Refs")]
        [SerializeField]
        private CharacterController m_characterController;
        [SerializeField]
        protected GameObject m_model = null;
        public GameObject Model => m_model;
        [SerializeField]
        private Transform m_cameraTarget;
        [SerializeField]
        private Transform m_topHead = null;

        public Transform CameraTarget => m_cameraTarget;

        [Header("Gameplay Params")]
        public float BaseSpeed = 5f;
        protected float m_gameplaySpeedMultiplier = 1f;
        public float Speed => BaseSpeed * m_gameplaySpeedMultiplier;

        [SerializeField]
        private float m_jumpCooldown = 0.1f;
        protected float JumpCooldown => m_jumpCooldown;
        protected float m_lastJumpTime = 0f;
        [SerializeField]
        protected float m_delayToJumpAfterLeavingGround = 0.2f;
        protected float m_lastTimeGrounded = 0f;

        protected Vector3 m_movementDirection = default;
        protected Vector2 m_lookAroundValues = default;

        [Header("Custom Physics Params")]
        [SerializeField]
        private float m_gravity = 9.81f;
        [SerializeField]
        private float m_jumpSpeed = 6f;

        protected bool m_isGrounded = false;
        protected bool m_isHeadTouchingCeiling = false;

        protected float m_verticalVelocity = 0f;

        [Header("Camera Settings")]
        [SerializeField]
        private Vector2 m_cameraSensitivity = new Vector2(60f, 60f);
        [SerializeField]
        private Vector2 m_cameraClamping = new Vector2(-60, 60);
        protected float m_yCamValue = 0f;

        protected bool m_canMove = false;

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
            m_characterController.enabled = true;
            Debug.LogError("Activate Movements");
        }

        public void DeactivateMovement()
        {
            m_canMove = false;
            m_characterController.enabled = false;
            Debug.LogError("Deactivate Movements");
        }

        public void HandleMovementInput(Vector2 a_moveInputs) 
        {
            if(m_directionIsBlocked)
            {
                return;
            }
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

        public virtual void HandleJumpInput() 
        {
            if (!m_canMove) return;

            if(Time.time - m_lastJumpTime > m_jumpCooldown && (m_isGrounded || Time.time - m_lastTimeGrounded< m_delayToJumpAfterLeavingGround))
            {
                Jump();
                m_lastJumpTime = Time.time;
            }
        }

        public virtual void HandleSecondaryAttackStartedInput() { }

        private void check_if_grounded()
        {
            m_isGrounded = false;
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hitInfo, 0.55f))
            {
                if(hitInfo.collider != gameObject)
                    m_isGrounded = true;
            }
            else if (Physics.Raycast(transform.position + Vector3.up * 0.5f + transform.forward * 0.2f, Vector3.down, out hitInfo, 0.7f))
            {
                if (hitInfo.collider != gameObject)
                    m_isGrounded = true;
            }

            
            if(m_isGrounded)
            {
                m_lastTimeGrounded = Time.time;
            }
        }

        private void apply_movement()
        {

            Vector3 movementToApply = default;

            movementToApply += m_movementDirection * Speed;

            if (!m_verticalVelocityIsLocked)
            {
                if (m_isHeadTouchingCeiling)
                {
                    if (m_verticalVelocity > 0f)
                    {
                        m_verticalVelocity = 0f;
                    }

                }
                if (m_isGrounded && m_verticalVelocity < 0f)
                {
                    m_verticalVelocity = -1f;
                }
                else
                {
                    m_verticalVelocity -= m_gravity * Time.deltaTime;
                }
                
            }

            movementToApply += m_verticalVelocity * Vector3.up;


            m_characterController.Move(movementToApply * Time.deltaTime);
        }

        private void apply_rotation()
        {
            transform.Rotate(Vector3.up, m_lookAroundValues.x * m_cameraSensitivity.x * MOtter.MOtt.SAVE.CameraSensitivity * Time.deltaTime);
           
            m_yCamValue += -m_lookAroundValues.y * MOtter.MOtt.SAVE.CameraSensitivity * m_cameraSensitivity.y * Time.deltaTime;
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
        protected virtual void Jump()
        {
            if (m_verticalVelocityIsLocked) return;
            m_verticalVelocity = m_jumpSpeed;
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Jump);
        }

        private bool m_directionIsBlocked = false;
        private Coroutine m_blockDirectionCoroutine = null;
        protected void BlockDirectionFor(float a_duration)
        {
            if(m_blockDirectionCoroutine != null)
            {
                StopCoroutine(m_blockDirectionCoroutine);
            }
            m_blockDirectionCoroutine = StartCoroutine(BlockDirectionRoutine(a_duration));
        }
        private IEnumerator BlockDirectionRoutine(float a_duration)
        {
            m_directionIsBlocked = true;
            yield return new WaitForSeconds(a_duration);
            m_directionIsBlocked = false;
        }

        private bool m_verticalVelocityIsLocked = false;
        private Coroutine m_lockVerticalVelocityCoroutine = null;
        protected void LockVerticalVelocityFor(float a_duration)
        {
            if (m_lockVerticalVelocityCoroutine != null)
            {
                StopCoroutine(m_lockVerticalVelocityCoroutine);
            }
            m_lockVerticalVelocityCoroutine = StartCoroutine(LockVerticalVelocityRoutine(a_duration));
        }
        private IEnumerator LockVerticalVelocityRoutine(float a_duration)
        {
            m_verticalVelocityIsLocked = true;
            yield return new WaitForSeconds(a_duration);
            m_verticalVelocityIsLocked = false;
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