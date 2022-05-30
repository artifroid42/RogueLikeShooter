using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float m_speed = 12f;
        [SerializeField]
        private float m_lifeTime = 10f;
        private float m_timeOfStart = 0f;

        private void OnEnable()
        {
            m_timeOfStart = Time.time;
        }

        private void Update()
        {
            transform.position += transform.forward * m_speed * Time.deltaTime;
            if(Time.time - m_timeOfStart > m_lifeTime)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}