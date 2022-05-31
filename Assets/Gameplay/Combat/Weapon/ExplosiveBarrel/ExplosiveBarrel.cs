using RLS.Generic.VFXManager;
using System.Collections;
using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class ExplosiveBarrel : MonoBehaviour
    {
        [SerializeField]
        private float m_explosionRadius = 3f;
        [SerializeField]
        private BarrelExplosion m_explosion = null;
        [SerializeField]
        private GameObject m_barilModel = null;
        [SerializeField]
        private Rigidbody m_rigidbody = null;
        public Rigidbody Rigidbody => m_rigidbody;

        public void PrepareToThrow()
        {
            m_rigidbody.isKinematic = false;
            m_barilModel.SetActive(true);
        }

        public void SetOwner(CombatController a_owner)
        {
            m_explosion.SetOwner(a_owner);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Detonate(collision.GetContact(0).point);
        }

        private void Detonate(Vector3 a_collisionPoint)
        {
            m_rigidbody.isKinematic = true;
            m_explosion.transform.position = a_collisionPoint;
            m_barilModel.SetActive(false);
            VFXManager.Instance.PlayFXAt(0, a_collisionPoint, Quaternion.identity);
            StartCoroutine(DetonatingRoutine());
        }

        private IEnumerator DetonatingRoutine()
        {
            yield return null;
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