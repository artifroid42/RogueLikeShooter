using System;
using System.Collections;
using System.Collections.Generic;
using Tween;
using UnityEngine;
using UnityEngine.UI;

namespace RLS.Gameplay.Player.Upgrades
{
    public enum EUpgrade
    {
        Health = 0,
        Damage = 1, 
        AttackSpeed = 2,
        MoveSpeed = 3,
        Power = 4
    }

    public class ClassUpgradesModule : MonoBehaviour
    {
        [Header("Ressources")]
        [SerializeField]
        private UpgradeReprensentationsData m_reprensentationsData;

        [Header("Refs")]       
        [SerializeField]
        private List<ClassSelectionWidget> m_classSelectionWidgets;
        public List<ClassSelectionWidget> ClassSelectionWidgets => m_classSelectionWidgets;

        [SerializeField]
        private List<UpgradeLineWidget> m_upgradeLineWidgets;
        public List<UpgradeLineWidget> UpgradeLineWidgets => m_upgradeLineWidgets;

        [SerializeField]
        private Image m_classRepresentationImage;

        [SerializeField]
        private GameObject m_classSelection;
        [SerializeField]
        private GameObject m_classUpgrades;

        [SerializeField]
        private PositionTween m_classSelectionTween;
        [SerializeField]
        private PositionTween m_classUpgradesTween;

        internal void Init()
        {
            foreach (var upgradeLineWidget in m_upgradeLineWidgets)
            {
                var upgradeRepresentation = m_reprensentationsData.UpgradeRepresentations.Find(x => x.Upgrade == upgradeLineWidget.Upgrades);
                upgradeLineWidget.Init(upgradeRepresentation.UpgradeColor, upgradeRepresentation.UpgradeSprite);
            }
        }

        public void ShowClassSelection()
        {
            m_classSelection.SetActive(true);
            m_classUpgrades.SetActive(false);
        }

        public void ShowClassUpgrades(Sprite m_classSprite)
        {
            m_classSelection.SetActive(false);
            m_classUpgrades.SetActive(true);
            m_classRepresentationImage.sprite = m_classSprite;
        }

        public void Hide()
        {
            m_classSelection.SetActive(false);
            m_classUpgrades.SetActive(false);
        }


    }
}

