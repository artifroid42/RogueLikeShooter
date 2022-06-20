using RLS.Gameplay.Combat.Weapon;
using UnityEngine;

namespace RLS.Gameplay.Player.Pirate
{
    public class PirateCombatController : PlayerCombatController
    {
        public override float PowerCooldownRatio => (Time.time - m_timeOfLastBarrelAttack) / BarrelAttackCooldown;

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
        public float SwordAttackCooldown = 1.5f;
        private float m_timeOfLastSwordAttack = 0f;

        public float BarrelAttackCooldown = 5f;
        public int BarrelExplosionDamage = 5;
        private float m_timeOfLastBarrelAttack = 0f;
        [SerializeField]
        private float m_barrelThrowForce = 10f;
        public override void HandleAttackStartedInput()
        {
            base.HandleAttackStartedInput();
            if (Time.time - m_timeOfLastSwordAttack < SwordAttackCooldown) return;
            m_timeOfLastSwordAttack = Time.time;

            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.ShootPirate);
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
                if (Time.time - m_timeOfLastBarrelAttack < BarrelAttackCooldown) return;
                m_timeOfLastBarrelAttack = Time.time;

                m_animationHandler.ThrowExplosiveBarrel();
                m_explosiveBarrelInstantiated = Pooling.PoolingManager.Instance.GetPoolingSystem<ExplosiveBarrelPoolingSystem>().
                    GetObject(m_explosiveBarrelPrefab,
                    m_projectileSource.position,
                    GetComponent<PlayerMovementController>().CameraTarget.rotation);
                m_explosiveBarrelInstantiated.SetUp(BarrelExplosionDamage, this);
                m_explosiveBarrelInstantiated.Rigidbody.AddForce(m_explosiveBarrelInstantiated.transform.forward * m_barrelThrowForce, ForceMode.VelocityChange);
                MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.ShootShuriken);
            }

            
        }
    }
}