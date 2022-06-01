using RLS.Gameplay.Player.Upgrades;
using System;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Component Refs")]
        [SerializeField]
        private PlayerAnimationsHandler m_animationHandler = null;
        public PlayerAnimationsHandler AnimationsHandler => m_animationHandler;

        [Header("Sight Pos")]
        [SerializeField]
        private Transform[] m_seeablePositions = null;
        public Transform[] SeeablePositions => m_seeablePositions;

        [Header("Class")]
        [SerializeField]
        private EClass m_class;
        public EClass Class => m_class;

        public void GetControllers()
        {
            // GetComponents for get diferents controllers and then upgrade their stats
        }

        public void RefreshStats()
        {
            throw new NotImplementedException();
        }
    }
}
