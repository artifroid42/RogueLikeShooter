using RLS.Gameplay.Combat.Weapon;
using UnityEngine;

namespace RLS.Gameplay.Ennemy.LongRange
{
    public class ProjectileAttackingMonsterState : AGenericAttackingPlayerMonsterState<LongRangeMonsterAI>
    {
        [Header("Refs")]
        [SerializeField]
        private FireBall m_projectilePrefab = null;

        protected override void TryToAttack()
        {
            base.TryToAttack();
            var projectile = Pooling.PoolingManager.Instance.GetPoolingSystem<FireBallPoolingSystem>().
                GetObject(m_projectilePrefab,
                Owner.ProjectileSource.transform.position,
                Quaternion.identity);
            if (Owner.ClosestPlayer != null)
                projectile.transform.forward = (Owner.ClosestPlayer.SeeablePositions[0].position - Owner.ProjectileSource.transform.position);
            else
                projectile.transform.rotation = Owner.ProjectileSource.rotation;
            var damageDealer = projectile.GetComponent<Combat.DamageDealer>();
            damageDealer.SetOwner(Owner.CombatController);
            damageDealer.CanDoDamage = true;
        }
    }
}