using System;
using UnityEngine;

namespace RLS.Gameplay.Combat.Weapon
{
    [RequireComponent(typeof(SphereCollider))]
    public class BarrelExplosion : MonoBehaviour
    {
        private const float EXPLOSION_PROPAGATION_SPEED = 3f;

        private bool m_isExecuting = false;
        private float m_maxRadius = 1f;
        private float m_currentRadius = 0f;

        private SphereCollider m_sphereCollider = null;

        public Action OnExplosionFinished = null;

        public void Execute(float a_maxRadius)
        {
            m_isExecuting = true;
            m_maxRadius = a_maxRadius;
            m_currentRadius = 0f;
            m_sphereCollider = GetComponent<SphereCollider>();
        }

        private void FixedUpdate()
        {
            if(m_isExecuting)
            {
                m_currentRadius += EXPLOSION_PROPAGATION_SPEED * Time.fixedDeltaTime;
                m_sphereCollider.radius = m_currentRadius;
                if(m_currentRadius >= m_maxRadius)
                {
                    OnExplosionFinished?.Invoke();
                }
            }
        }
    }
}