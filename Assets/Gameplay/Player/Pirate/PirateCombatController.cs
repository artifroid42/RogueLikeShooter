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
            if (Time.time - m_timeOfLastBarrelAttack < m_barrelAttackCooldown) return;
            m_timeOfLastBarrelAttack = Time.time;

            m_animationHandler.ThrowExplosiveBarrel();
            var newBarrel = Pooling.PoolingManager.Instance.GetPoolingSystem<ExplosiveBarrelPoolingSystem>().
                GetObject(m_explosiveBarrelPrefab, 
                m_projectileSource.position, 
                GetComponent<PlayerMovementController>().CameraTarget.rotation);
            newBarrel.SetOwner(this);
            newBarrel.PrepareToThrow();
            newBarrel.Rigidbody.AddForce(newBarrel.transform.forward * m_barrelThrowForce, ForceMode.VelocityChange);
        }
    }
}