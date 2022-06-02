using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RLS.Gameplay.Player.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider m_healthSlider;
        [SerializeField]
        private Slider m_bgSlider;

        [SerializeField]
        private float m_updateSpeed = 1f;
        [SerializeField]
        private float m_bgUpdateSpeed = 1f;

        private float m_currentValue = 1f;
        private float m_targetValue = 1f;

        private float m_bgCurrentValue = 1f;

        private void LateUpdate()
        {
            if(m_currentValue != m_targetValue)
            {
                m_currentValue = Mathf.MoveTowards(m_currentValue, m_targetValue, m_updateSpeed * Time.deltaTime);
                m_healthSlider.value = m_currentValue;
            }
            else if(m_bgCurrentValue != m_currentValue)
            {
                m_bgCurrentValue = Mathf.MoveTowards(m_bgCurrentValue, m_currentValue, m_bgUpdateSpeed * Time.deltaTime);
                m_bgSlider.value = m_bgCurrentValue;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                SetHealthSliderValue(m_currentValue - 0.1f);
            }
        }

        public void SetHealthSliderValue(float a_targetValue)
        {
            m_targetValue = a_targetValue;
        }
    }
}
