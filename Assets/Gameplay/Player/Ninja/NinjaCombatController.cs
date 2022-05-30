using RLS.Gameplay.Combat.Weapon;
using UnityEngine;

namespace RLS.Gameplay.Player.Ninja
{
    public class NinjaCombatController : PlayerCombatController
    {
        [Header("Refs")]
        [SerializeField]
        private Combat.Weapon.Shuriken m_shurikenPrefab = null;
        [SerializeField]
        private Transform m_shurikenSource = null;

        [Header("Params")]
        [SerializeField]
        private float m_attackCooldown = 1f;
        private float m_timeOfLastAttack = 0f;

        public override void HandleAttackStartedInput()
        {
            if (Time.time - m_timeOfLastAttack < m_attackCooldown) return;
            m_timeOfLastAttack = Time.time;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hitInfo))
            {
                var newShuriken = Pooling.PoolingManager.Instance.GetPoolingSystem<ShurikenPoolingSystem>().
                    GetObject(m_shurikenPrefab,
                    m_shurikenSource.position,
                    Quaternion.LookRotation(hitInfo.point - m_shurikenSource.position));
                var damageDealer = newShuriken.GetComponent<Combat.DamageDealer>();
                damageDealer.SetOwner(this);
                damageDealer.CanDoDamage = true;
            }
            else
            {
                var newShuriken = Pooling.PoolingManager.Instance.GetPoolingSystem<ShurikenPoolingSystem>().
                    GetObject(m_shurikenPrefab,
                    m_shurikenSource.position,
                    GetComponent<PlayerMovementController>().CameraTarget.rotation);
                var damageDealer = newShuriken.GetComponent<Combat.DamageDealer>();
                damageDealer.SetOwner(this);
                damageDealer.CanDoDamage = true;
            }
            m_player.AnimationsHandler.ThrowShuriken();
        }
    }
}