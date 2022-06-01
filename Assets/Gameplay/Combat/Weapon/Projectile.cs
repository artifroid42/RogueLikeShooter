using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        protected float m_speed = 12f;
        [SerializeField]
        protected float m_lifeTime = 10f;
        private float m_timeOfStart = 0f;

        protected virtual void OnEnable()
        {
            m_timeOfStart = Time.time;
        }

        protected virtual void Update()
        {
            transform.position += transform.forward * m_speed * Time.deltaTime;
            if(Time.time - m_timeOfStart > m_lifeTime)
            {
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }

    }
}