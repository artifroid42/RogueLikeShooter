using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class PirateSword : DamageDealer
    {
        [SerializeField]
        private GameObject m_fxContainer = null;

        protected override void HandleDamageActivated()
        {
            base.HandleDamageActivated();
            m_fxContainer.SetActive(true);
        }

        protected override void HandleDamageDeactivated()
        {
            base.HandleDamageDeactivated();
            m_fxContainer.SetActive(false);
        }
    }
}