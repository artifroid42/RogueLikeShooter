

namespace RLS.Gameplay.Player
{
    public class PlayerCombatController : Combat.CombatController, IPlayerInputsObserver
    {
        protected Player m_player = null;


        protected override void Start()
        {
            base.Start();
            GetComponent<PlayerInputsHandler>()?.RegisterNewObserver(this);
            m_player = GetComponent<Player>();
        }

        private void OnDestroy()
        {
            GetComponent<PlayerInputsHandler>()?.UnregisterObserver(this);
        }

        public virtual void HandleAttackStartedInput()
        { }

        public virtual void HandleAttackCanceledInput()
        { }

        public virtual void HandleSecondaryAttackStartedInput() { }
        public virtual void HandleSecondaryAttackCanceledInput() { }
    }
}