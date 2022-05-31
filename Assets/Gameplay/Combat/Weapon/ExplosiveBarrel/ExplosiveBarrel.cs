using System.Collections;
using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class ExplosiveBarrel : MonoBehaviour
    {
        [SerializeField]
        private float m_timeToDetonate = 1f;
        [SerializeField]
        private float m_explosionRadius = 3f;
        [SerializeField]
        private BarrelExplosion m_explosion = null;

        private void OnCollisionEnter(Collision collision)
        {
            Detonate();
        }

        private void Detonate()
        {
            StartCoroutine(DetonatingRoutine());
        }

        private IEnumerator DetonatingRoutine()
        {
            yield return new WaitForSeconds(m_timeToDetonate);

            m_explosion.gameObject.SetActive(true);
            m_explosion.Execute(m_explosionRadius);
            m_explosion.OnExplosionFinished += HandleExplosionFinished;
        }

        private void HandleExplosionFinished()
        {
            m_explosion.OnExplosionFinished -= HandleExplosionFinished;

            gameObject.SetActive(false);
        }
    }
}