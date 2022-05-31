using System.Collections;
using System.Collections.Generic;
using Tween;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    public class ClassUpgradesModule : MonoBehaviour
    {
        [SerializeField]
        private List<ClassSelectionWidget> m_classSelectionWidgets;
        public List<ClassSelectionWidget> ClassSelectionWidgets => m_classSelectionWidgets;

        [SerializeField]
        private GameObject m_classSelection;
        [SerializeField]
        private GameObject m_classUpgrades;

        [SerializeField]
        private PositionTween m_classSelectionTween;
        [SerializeField]
        private PositionTween m_classUpgradesTween;

        public void Init()
        {
            m_classSelection.SetActive(true);
            m_classUpgrades.SetActive(false);
        }
    }
}

