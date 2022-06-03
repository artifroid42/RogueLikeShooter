using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class PowerShotBullet : Projectile
    {
        [SerializeField]
        private float m_explosionRadius = 3f;
        [SerializeField]
        private Explosion m_explosion = null;

        [SerializeField]
        private GameObject m_bulletFX = null;

        private CombatController m_owner = null;
        private bool m_exploding;

        public void SetChargeRatio(float a_chargeRatio, int a_maxExplosionDamage)
        {
            a_chargeRatio = Mathf.Clamp01(a_chargeRatio);
            m_bulletFX.SetActive(true);
            transform.localScale = Vector3.one * a_chargeRatio;
            m_bulletFX.transform.localScale = Vector3.one * a_chargeRatio;
            m_explosion.SetUp(m_explosionRadius * a_chargeRatio,
                Mathf.RoundToInt(a_maxExplosionDamage * a_chargeRatio),
                m_owner);
            m_exploding = false;
        }

        public void SetOwner(CombatController a_owner)
        {
            m_owner = a_owner;
        }

        private void Explode()
        {
            m_bulletFX.SetActive(false);
            m_exploding = true;
            m_explosion.DealDamage();
            gameObject.SetActive(false);
        }

        protected override void Update()
        {
            if(!m_exploding)
                base.Update();
        }


        protected override void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<CombatController>(out CombatController l_cc))
            {
                if(l_cc == m_owner)
                {
                    return;
                }
            }

            if(other.gameObject == m_explosion.gameObject)
            {
                return;
            }

            Explode();
        }
    }
}