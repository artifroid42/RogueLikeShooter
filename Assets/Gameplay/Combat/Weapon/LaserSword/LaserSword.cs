using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class LaserSword : Combat.DamageDealer
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