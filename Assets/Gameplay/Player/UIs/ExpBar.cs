using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RLS.Gameplay.Player.UI
{
    public class ExpBar : MonoBehaviour
    {
        [SerializeField]
        private Slider m_expSlider;

        [SerializeField]
        private float m_updateSpeed = 1f;

        private float m_currentValue = 1f;
        private float m_targetValue = 1f;

        internal void SetValue(float a_targetValue)
        {
            m_targetValue = a_targetValue;
        }

        private void LateUpdate()
        {
            if (m_currentValue < m_targetValue)
            {
                m_currentValue = Mathf.MoveTowards(m_currentValue, m_targetValue, m_updateSpeed * Time.deltaTime);
                m_expSlider.value = m_currentValue;
            }
            else if(m_currentValue > m_targetValue)
            {
                if (m_currentValue == 1f)
                {
                    m_currentValue = 0;
                }
                else
                {
                    m_currentValue = Mathf.MoveTowards(m_currentValue, 1f, m_updateSpeed * Time.deltaTime);
                }
                m_expSlider.value = m_currentValue;
            }
        }

        public void SetHealthSliderValue(float a_targetValue)
        {
            m_targetValue = a_targetValue;
        }
    }
}

