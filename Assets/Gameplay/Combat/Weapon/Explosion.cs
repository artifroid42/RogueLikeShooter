using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    [RequireComponent(typeof(SphereCollider))]
    public class Explosion : MonoBehaviour
    {
        private CombatController m_owner = null;
        private SphereCollider m_sphereCollider = null;


        private List<CombatController> m_collidedControllers = new List<CombatController>();
        private int m_damageToDeal = 5;

        public void SetUp(float a_maxRadius, int a_damageToDeal, CombatController a_owner)
        {
            m_sphereCollider = GetComponent<SphereCollider>();
            m_sphereCollider.radius = a_maxRadius;
            m_damageToDeal = a_damageToDeal;
            m_owner = a_owner;
        }

        public void DealDamage()
        {
            m_collidedControllers?.RemoveAll(x => x == null);
            m_collidedControllers?.ForEach(x => x.TakeDamage(m_damageToDeal, m_owner));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CombatController>(out CombatController l_combatController))
            {
                if(!m_collidedControllers.Contains(l_combatController))
                    m_collidedControllers.Add(l_combatController);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CombatController>(out CombatController l_combatController))
            {
                if (m_collidedControllers.Contains(l_combatController))
                    m_collidedControllers.Remove(l_combatController);
            }
        }

    }
}