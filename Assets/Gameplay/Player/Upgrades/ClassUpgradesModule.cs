using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.Upgrades
{
    public class ClassUpgradesModule : MonoBehaviour
    {
        [SerializeField]
        private List<ClassSelectionWidget> m_classSelectionWidgets;

        public List<ClassSelectionWidget> ClassSelectionWidgets => m_classSelectionWidgets; 
    }
}

