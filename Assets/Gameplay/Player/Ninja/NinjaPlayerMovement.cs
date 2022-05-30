using UnityEngine;

namespace RLS.Gameplay.Player.Ninja
{
    public class NinjaPlayerMovement : PlayerMovementController
    {
        [Header("Ninja Specifics")]
        [SerializeField]
        private int m_numberOfJumps = 2;
        private int m_numberOfJumpsSinceGroundLeft = 0;
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