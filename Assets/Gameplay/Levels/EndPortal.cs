using System;
using UnityEngine;

namespace RLS.Gameplay.Levels
{
    public class EndPortal : MonoBehaviour
    {
        public Action OnPlayerEnteredPortal = null;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PlayerController>(out PlayerController l_playerController))
            {
                OnPlayerEnteredPortal?.Invoke();
            }
        }
    }
}