using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.UI
{
    public abstract class APlayerUIManager : MonoBehaviour
    {
        protected PlayerPanel m_playerPanel;
        protected Debug.PlayerDebugPanel m_debugPanel;

        protected virtual void Init(PlayerPanel a_playerPanel, Debug.PlayerDebugPanel a_debugPanel)
        {
            m_playerPanel = a_playerPanel;
            m_debugPanel = a_debugPanel;
        }


    }

}
