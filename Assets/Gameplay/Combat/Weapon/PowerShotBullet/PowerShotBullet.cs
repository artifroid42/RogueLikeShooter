using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class PowerShotBullet : Projectile
    {
        [SerializeField]
        private float m_explosionRadius = 3f;
        [SerializeField]
        private float m_explosionDamage = 20f;
        [SerializeField]
        private Explosion m_explosion = null;

        private CombatController m_owner = null;
        private bool m_exploding;

        public void SetChargeRatio(float a_chargeRatio)
        {
            a_chargeRatio = Mathf.Clamp01(a_chargeRatio);

            transform.localScale = Vector3.one * a_chargeRatio;
            m_explosion.SetDamageToDeal(Mathf.RoundToInt(m_explosionDamage * a_chargeRatio));
            m_exploding = false;
        }

        public void SetOwner(CombatController a_owner)
        {
            m_explosion.SetOwner(a_owner);
            m_owner = a_owner;
        }

        private void Explode()
        {
            m_exploding = true;
            m_explosion.Execute(m_explosionRadius);
            m_explosion.gameObject.SetActive(true);
            m_explosion.OnExplosionFinished += HandleExplosionFinished;
        }

        protected override void Update()
        {
            if(!m_exploding)
                base.Update();
        }

        private void HandleExplosionFinished()
        {
            m_explosion.OnExplosionFinished -= HandleExplosionFinished;

            gameObject.SetActive(false);
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