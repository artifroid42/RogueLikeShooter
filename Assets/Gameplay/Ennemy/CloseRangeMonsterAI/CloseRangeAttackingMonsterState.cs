
namespace RLS.Gameplay.Ennemy.CloseRange
{
    public class CloseRangeAttackingMonsterState : AGenericAttackingPlayerMonsterState<CloseRangeMonsterAI>
    {
        protected override bool TryToAttack()
        {
            if (Owner.WeaponDamageDealer.CanDoDamage)
                return false;
            Owner.AnimationsHandler.SwordAttack();
            Owner.WeaponDamageDealer.CanDoDamage = true;
            return true;
        }
    }
}