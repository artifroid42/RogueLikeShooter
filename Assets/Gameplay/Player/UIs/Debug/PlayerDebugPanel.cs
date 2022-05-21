using TMPro;
using UnityEngine;

namespace RLS.Gameplay.Player.UI.Debug
{
    public class PlayerDebugPanel : Panel
    {
        [SerializeField]
        private TextMeshProUGUI m_positionText = null;

        public void UpdatePositionDisplay(Vector3 a_playerPosition)
        {
            m_positionText.text = $"{a_playerPosition}";
        }
    }
}