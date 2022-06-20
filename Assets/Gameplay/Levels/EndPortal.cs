using System;
using UnityEngine;

namespace RLS.Gameplay.Levels
{
    public class EndPortal : MonoBehaviour
    {
        public Action OnPlayerEnteredPortal = null;
        public Action OnPlayerTriedToEnterPortalWhenDisabled = null;
        public bool m_canGoToNextStage = false;

        public void AllowAccessToNextStage()
        {
            m_canGoToNextStage = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player.PlayerMovementController l_playerController))
            {
                if (m_canGoToNextStage)
                {
                    OnPlayerEnteredPortal?.Invoke();
                }
                else
                {
                    OnPlayerTriedToEnterPortalWhenDisabled?.Invoke();
                }
            }
            
        }
    }
}