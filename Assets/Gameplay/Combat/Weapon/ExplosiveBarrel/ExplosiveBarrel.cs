using RLS.Generic.VFXManager;
using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class ExplosiveBarrel : MonoBehaviour
    {
        [SerializeField]
        private float m_explosionRadius = 3f;
        [SerializeField]
        private Explosion m_explosion = null;
        [SerializeField]
        private GameObject m_barilModel = null;
        [SerializeField]
        private Rigidbody m_rigidbody = null;
        public Rigidbody Rigidbody => m_rigidbody;


        public void SetUp(int a_damageToDeal, CombatController a_owner)
        {
            m_rigidbody.isKinematic = false;
            m_barilModel.SetActive(true);
            m_explosion.SetUp(m_explosionRadius, a_damageToDeal, a_owner);
        }

        private void OnCollisionEnter(Collision collision)
        {
            PlaceBarrel(collision.GetContact(0));
        }

        private void PlaceBarrel(ContactPoint a_contactPoint)
        {
            m_rigidbody.isKinematic = true;
            m_explosion.transform.position = a_contactPoint.point;
            m_barilModel.transform.up = a_contactPoint.normal;
        }

        public void Detonate()
        {
            m_barilModel.SetActive(false);
            VFXManager.Instance.PlayFXAt(0, m_explosion.transform.position, Quaternion.identity);
            m_explosion.DealDamage();
        }


    }
}