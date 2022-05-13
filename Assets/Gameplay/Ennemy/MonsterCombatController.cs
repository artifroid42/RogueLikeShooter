

namespace RLS.Gameplay.Ennemy
{
    public class MonsterCombatController : Combat.CombatController
    {
        protected override void HandleDeath()
        {
            base.HandleDeath();
            Destroy(gameObject);
        }
    }
}