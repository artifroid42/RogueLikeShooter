using RLS.Gameplay.Combat.Weapon;
using UnityEngine;

namespace RLS.Gameplay.Player.SciFi
{
    public class SciFiCombatController : PlayerCombatController
    {
        [SerializeField]
        private PlayerAnimationsHandler m_animationHandler = null;
        [SerializeField]
        private Tween.PositionTween m_weaponPositionTween = null;

        [SerializeField]
        private LaserBullet m_laserBulletPrefab = null;
        [SerializeField]
        private PowerShotBullet m_powerShotBullerPrefab = null;
        [SerializeField]
        private GameObject m_powerShotChargeFX = null; 
        [SerializeField]
        private Transform m_bulletSource = null;
        public int LaserBulletDamage = 5;
        public float AttackSpeed = 1.2f;
        private float m_timeOfLastShot = 0f;
        public int PowerShotDamage = 30;
        public float PowerShotLoadingDuration = 4f;
        private float m_timeOfStartToLoadPowerShot = 0f;
        
        
        private bool m_isLoadingPowerShot = false;
        private bool m_isShooting = false;
        public override void HandleAttackStartedInput()
        {
            base.HandleAttackStartedInput();
            if (m_isLoadingPowerShot) return;
            m_isShooting = true;
        }

        public override void HandleAttackCanceledInput()
        {
            base.HandleAttackCanceledInput();
            m_isShooting = false;
        }

        public override void HandleSecondaryAttackStartedInput()
        {
            base.HandleSecondaryAttackStartedInput();
            if (m_isShooting) return;
            m_isLoadingPowerShot = true;
            m_timeOfStartToLoadPowerShot = Time.time;
            m_powerShotChargeFX.SetActive(true);
        }

        public override void HandleSecondaryAttackCanceledInput()
        {
            base.HandleSecondaryAttackCanceledInput();
            if (!m_isLoadingPowerShot) return;
            m_isLoadingPowerShot = false;
            ShootPowerShot((Time.time - m_timeOfStartToLoadPowerShot) / PowerShotLoadingDuration);
            m_powerShotChargeFX.SetActive(false);
        }

        private void Update()
        {
            if(m_isShooting)
            {
                if(Time.time- m_timeOfLastShot > 1f / AttackSpeed)
                {
                    ShootLaserBullet();
                    m_timeOfLastShot = Time.time;
                }
            }
            if(m_isLoadingPowerShot)
            {
                if(Time.time - m_timeOfStartToLoadPowerShot > PowerShotLoadingDuration)
                {
                    ShootPowerShot(1f);
                    m_isLoadingPowerShot = false;
                }
            }
        }

        private void ShootPowerShot(float a_chargeRatio)
        {
            if(a_chargeRatio < 0.2f)
            {
                return;
            }
            Debug.Log($"SHOT POWERRRR : {a_chargeRatio}");
            m_powerShotChargeFX.SetActive(false);
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hitInfo))
            {
                var bullet = Pooling.PoolingManager.Instance.GetPoolingSystem<PowerShotBulletPoolingSystem>().
                    GetObject(m_powerShotBullerPrefab,
                    m_bulletSource.position,
                    Quaternion.LookRotation(hitInfo.point - m_bulletSource.position));
                bullet.SetOwner(this);
                bullet.SetChargeRatio(a_chargeRatio, PowerShotDamage);
            }
            else
            {
                var bullet = Pooling.PoolingManager.Instance.GetPoolingSystem<PowerShotBulletPoolingSystem>().
                    GetObject(m_powerShotBullerPrefab,
                    m_bulletSource.position,
                    GetComponent<PlayerMovementController>().CameraTarget.rotation);
                bullet.SetOwner(this);
                bullet.SetChargeRatio(a_chargeRatio, PowerShotDamage);
            }
        }

        private void ShootLaserBullet()
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hitInfo))
            {
                var bullet = Pooling.PoolingManager.Instance.GetPoolingSystem<LaserBulletPoolingSystem>().
                    GetObject(m_laserBulletPrefab,
                    m_bulletSource.position,
                    Quaternion.LookRotation(hitInfo.point - m_bulletSource.position));

                var damageDealer = bullet.GetComponent<Combat.DamageDealer>();
                damageDealer.SetOwner(this);
                damageDealer.CanDoDamage = true;
                damageDealer.SetDamageToDeal(LaserBulletDamage);
            }
            else
            {
                var bullet = Pooling.PoolingManager.Instance.GetPoolingSystem<LaserBulletPoolingSystem>().
                    GetObject(m_laserBulletPrefab,
                    m_bulletSource.position,
                    GetComponent<PlayerMovementController>().CameraTarget.rotation);

                var damageDealer = bullet.GetComponent<Combat.DamageDealer>();
                damageDealer.SetOwner(this);
                damageDealer.CanDoDamage = true;
                damageDealer.SetDamageToDeal(LaserBulletDamage);
            }
            m_animationHandler.GunShoot();
            m_weaponPositionTween.StartTween();
        }
    }
}