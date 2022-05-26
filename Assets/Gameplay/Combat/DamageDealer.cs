using UnityEngine;

namespace RLS.Gameplay.Combat
{
    public class DamageDealer : MonoBehaviour
    {
        private bool m_canDoDamage = false;
        public bool CanDoDamage 
        {
            get
            {
                return m_canDoDamage;
            }
            set
            {
                m_canDoDamage = value;
                if(m_canDoDamage)
                {
                    HandleDamageActivated();
                }
                else
                {
                    HandleDamageDeactivated();
                }
            }
        }

        [SerializeField]
        private int m_damageToDeal = 5;
        [SerializeField]
        private CombatController m_owner = null;

        public void SetOwner(CombatController a_owner)
        {
            m_owner = a_owner;
        }

        protected virtual void HandleDamageDeactivated()
        { }

        protected virtual void HandleDamageActivated()
        { }

        private void OnTriggerEnter(Collider other)
        {
            if(CanDoDamage && other.TryGetComponent<CombatController>(out CombatController l_combatController))
            {
                l_combatController.TakeDamage(m_damageToDeal, m_owner);
                if(l_combatController != null && (m_owner == null || m_owner.TeamIndex == l_combatController.TeamIndex))
                {
                    Destroy(gameObject);
                }
            }
        }
    }

#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(DamageDealer))]
    public class DamageDealerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Label($"Can do damage : {(target as DamageDealer).CanDoDamage}");

        }
    }
#endif
}