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

        internal void SetValue(float a_expRatio)
        {
            m_expSlider.value = a_expRatio;
        }
    }
}

