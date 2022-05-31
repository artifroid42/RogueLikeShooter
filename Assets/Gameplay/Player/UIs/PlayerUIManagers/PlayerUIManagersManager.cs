using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.UI
{
    public class PlayerUIManagersManager : MonoBehaviour
    {
        [SerializeField] private PlayerUpgradesUIManager m_upgradesUIManager;
        [SerializeField] private PlayerExpUIManager m_expUIManager;

        public PlayerUpgradesUIManager UpgradesUIManager => m_upgradesUIManager;
        public PlayerExpUIManager ExpUIManager => m_expUIManager;

        public void RegisterEvents()
        {
            m_upgradesUIManager.RegisterEvents();
        }

        public void UnregisterEvents()
        {
            m_upgradesUIManager.UnregisterEvents();
        }
    }
}
