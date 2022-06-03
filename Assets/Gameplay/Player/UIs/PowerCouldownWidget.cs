using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RLS.Gameplay.Player.UI
{
    public class PowerCouldownWidget : MonoBehaviour
    {
        [SerializeField]
        private Image m_powerIcon;
        [SerializeField]
        private Image m_couldownImage;
        [SerializeField]
        private GameObject m_powerReadyFX;
        [SerializeField]
        private Animator m_animator;
        private bool m_isPowerActive = false;

        public void SetCouldownValue(float a_value)
        {
            m_couldownImage.fillAmount = a_value;
            if(a_value >= 1f)
            {
                SetPowerFeedbackActive(true);
            }
            else if (m_powerReadyFX.activeSelf)
            {
                SetPowerFeedbackActive(false);
            }
        }

        public void SetPowerFeedbackActive(bool a_isActive)
        {
            if (m_isPowerActive != a_isActive)
            {
                m_powerReadyFX.SetActive(a_isActive);
                m_isPowerActive = a_isActive;
            }
        }
    }
}
