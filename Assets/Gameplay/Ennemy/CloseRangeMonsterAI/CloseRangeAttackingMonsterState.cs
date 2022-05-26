
namespace RLS.Gameplay.Ennemy.CloseRange
{
    public class CloseRangeAttackingMonsterState : AGenericAttackingPlayerMonsterState<CloseRangeMonsterAI>
    {
        protected override void TryToAttack()
        {
            base.TryToAttack();
            Owner.AnimationsHandler.Attack();
            Owner.WeaponDamageDealer.CanDoDamage = true;
        }
    }
}