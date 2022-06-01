using RLS.Gameplay.Combat.Weapon;
using UnityEngine;

namespace RLS.Gameplay.Player.Pirate
{
    public class PirateCombatController : PlayerCombatController
    {
        [Header("Prefab Refs")]
        [SerializeField]
        private ExplosiveBarrel m_explosiveBarrelPrefab = null;

        [Header("Refs")]
        [SerializeField]
        private PirateSword m_pirateSword = null;
        [SerializeField]
        private PlayerAnimationsHandler m_animationHandler = null;
        [SerializeField]
        private Transform m_projectileSource = null;

        private ExplosiveBarrel m_explosiveBarrelInstantiated = null;
        public PirateSword PirateSword => m_pirateSword;

        [Header("Params")]
        [SerializeField]
        private float m_swordAttackCooldown = 1.5f;
        private float m_timeOfLastSwordAttack = 0f;
        [SerializeField]
        private float m_barrelAttackCooldown = 5f;
        private float m_timeOfLastBarrelAttack = 0f;
        [SerializeField]
        private float m_barrelThrowForce = 10f;
        public override void HandleAttackStartedInput()
        {
            base.HandleAttackStartedInput();
            if (Time.time - m_timeOfLastSwordAttack < m_swordAttackCooldown) return;
            m_timeOfLastSwordAttack = Time.time;


            m_animationHandler.SwordAttack();
            m_pirateSword.CanDoDamage = true;
        }

        public override void HandleSecondaryAttackStartedInput()
        {
            base.HandleSecondaryAttackStartedInput();
            if(m_explosiveBarrelInstantiated)
            {
                m_explosiveBarrelInstantiated.Detonate();
                m_explosiveBarrelInstantiated = null;
            }
            else
            {
                if (Time.time - m_timeOfLastBarrelAttack < m_barrelAttackCooldown) return;
                m_timeOfLastBarrelAttack = Time.time;

                m_animationHandler.ThrowExplosiveBarrel();
                m_explosiveBarrelInstantiated = Pooling.PoolingManager.Instance.GetPoolingSystem<ExplosiveBarrelPoolingSystem>().
                    GetObject(m_explosiveBarrelPrefab,
                    m_projectileSource.position,
                    GetComponent<PlayerMovementController>().CameraTarget.rotation);
                m_explosiveBarrelInstantiated.SetOwner(this);
                m_explosiveBarrelInstantiated.PrepareToThrow();
                m_explosiveBarrelInstantiated.Rigidbody.AddForce(m_explosiveBarrelInstantiated.transform.forward * m_barrelThrowForce, ForceMode.VelocityChange);
            }

            
        }
    }
}