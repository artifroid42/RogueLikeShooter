using System;
using UnityEngine;

namespace RLS.Gameplay.Levels
{
    public class EndPortal : MonoBehaviour
    {
        public Action OnPlayerEnteredPortal = null;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PlayerController.PlayerMovementController>(out PlayerController.PlayerMovementController l_playerController))
            {
                OnPlayerEnteredPortal?.Invoke();
            }
        }
    }
}