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

        private bool m_hasCollided = false;
        protected bool HasCollided => m_hasCollided;

        protected virtual void OnEnable()
        {
            m_timeOfStart = Time.time;
            m_hasCollided = false;
        }

        protected virtual void Update()
        {
            if(!m_hasCollided)
            {
                transform.position += transform.forward * m_speed * Time.deltaTime;
                if (Time.time - m_timeOfStart > m_lifeTime)
                {
                    gameObject.SetActive(false);
                }
            } 
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (m_hasCollided) return;
            m_hasCollided = true;
            HandleCollision(other);
        }

        protected virtual void HandleCollision(Collider other)
        {
            gameObject.SetActive(false);
        }

    }
}