using RLS.Generic.VFXManager;
using System.Collections;
using UnityEngine;

namespace RLS.Gameplay.Player.Ninja
{
    public class NinjaPlayerMovement : PlayerMovementController
    {
        [Header("Ninja Specifics")]
        [SerializeField]
        private int m_numberOfJumps = 2;
        private int m_numberOfJumpsSinceGroundLeft = 0;
        public float DashCooldown = 2f;

        private float m_dashMultiplier = 5f;
        private float m_timeOfLastDash = 0f;
        [SerializeField]
        private float m_dashDuration = 1f;
        [SerializeField]
        private Rigidbody
            m_poofNinjaPropPrefab = null;

        private Coroutine m_dashCoroutine = null;

        public void SetDashRange(float a_dashRange)
        {
            m_dashMultiplier = a_dashRange / m_dashDuration;
        }

        public override void HandleSecondaryAttackStartedInput()
        {
            base.HandleSecondaryAttackStartedInput();
            if (Time.time - m_timeOfLastDash < DashCooldown) return;
            m_timeOfLastDash = Time.time;

            if(m_dashCoroutine != null)
            { 
                StopCoroutine(m_dashCoroutine);
            }

            m_dashCoroutine = StartCoroutine(DashRoutine());
        }

        private IEnumerator DashRoutine()
        {
            m_gameplaySpeedMultiplier = m_dashMultiplier;
            m_movementDirection.Normalize();
            m_model.SetActive(false);
            BlockDirectionFor(m_dashDuration);
            LockVerticalVelocityFor(m_dashDuration);
            VFXManager.Instance.PlayFXAt(0, transform.position + Vector3.up, Quaternion.identity);
            Pooling.PoolingManager.Instance.GetPoolingSystem<PoofNinjaPropPoolingSystem>().
                GetObject(m_poofNinjaPropPrefab, transform.position + Vector3.up * 2f, Quaternion.identity);
            yield return new WaitForSeconds(m_dashDuration);
            m_gameplaySpeedMultiplier = 1f;
            m_model.SetActive(true);
            VFXManager.Instance.PlayFXAt(0, transform.position + Vector3.up, Quaternion.identity);
        }

        public override void HandleJumpInput()
        {
            if (!m_canMove) return;

            if (Time.time - m_lastJumpTime > JumpCooldown 
                && (m_isGrounded || m_numberOfJumpsSinceGroundLeft < m_numberOfJumps))
            {
                if (m_isGrounded)
                {
                    m_numberOfJumpsSinceGroundLeft = 0;
                }
                Jump();
                m_lastJumpTime = Time.time;
            }
        }

        protected override void Jump()
        {
            base.Jump();
            m_numberOfJumpsSinceGroundLeft++;
        }
    }
}