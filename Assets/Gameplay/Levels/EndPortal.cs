using System;
using UnityEngine;

namespace RLS.Gameplay.Levels
{
    public class EndPortal : MonoBehaviour
    {
        public Action OnPlayerEnteredPortal = null;
        public bool m_canGoToNextStage = false;

        public void AllowAccessToNextStage()
        {
            m_canGoToNextStage = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(m_canGoToNextStage && other.TryGetComponent<Player.PlayerMovementController>(out Player.PlayerMovementController l_playerController))
            {
                OnPlayerEnteredPortal?.Invoke();
            }
        }
    }
}