using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    public class FireBall : Projectile
    {
        private const float DELAY_TO_DESPAWN = 3f;
        private float m_timeOfStart = 0f;

        protected override void OnEnable()
        {
            base.OnEnable();
            GetComponent<Collider>().enabled = true;
        }

        protected override void Update()
        {
            base.Update();
            if(HasCollided)
            {
                if (Time.time - m_timeOfStart > DELAY_TO_DESPAWN)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        protected override void HandleCollision(Collider other)
        {
            GetComponent<Collider>().enabled = false;
            m_timeOfStart = Time.time;

        }
    }
}