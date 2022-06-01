using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RLS.Gameplay.Player.Upgrades
{
    public class UpgradeLineWidget : MonoBehaviour
    {
        [SerializeField] private Image m_outlineImage;
        [SerializeField] private Image m_backgroundImage;
        [SerializeField] private Image m_upgradeImage;
        [SerializeField] private Image m_upgradeSliderImage;
            
        [SerializeField] private Slider m_upgradeSlider;

        [SerializeField] private EUpgrade m_eUpgrade;

        public EUpgrade Upgrades => m_eUpgrade; 

        internal void Init(Color a_upgradeColor, Sprite a_upgradeSprite)
        {
            m_outlineImage.color = a_upgradeColor;
            m_upgradeImage.sprite = a_upgradeSprite;
            m_upgradeImage.color = a_upgradeColor;
            m_upgradeSliderImage.color = a_upgradeColor;
            m_upgradeSlider.value = 0f;

            var bgColor = a_upgradeColor;
            bgColor.a = 0.4f;
            m_backgroundImage.color = bgColor;
        }

        public void SetUpgradeSliderValue(float a_value)
        {
            m_upgradeSlider.value = a_value;
        }
    }
}
