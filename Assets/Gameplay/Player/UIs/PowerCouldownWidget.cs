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

        public void SetCouldownValue(float a_value)
        {
            m_couldownImage.fillAmount = a_value;
        }

        public void SetPowerFeedbackActive(bool a_isActive)
        {
            m_powerReadyFX.SetActive(a_isActive);
            //if (a_isActive)
            //{
            //    m_animator.SetBool("POWER", true);
            //}
            //else
            //{
            //    m_animator.SetBool("POWER", false);
            //}
        }
    }
}
