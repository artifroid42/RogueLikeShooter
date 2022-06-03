using RLS.Generic.Ragdoll;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerCombatController : Combat.CombatController, IPlayerInputsObserver
    {
        protected Player m_player = null;

        public virtual float PowerCooldownRatio => 0;

        [SerializeField]
        private SyncCloneFromModel m_ragdollModel = null;

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

        protected override void HandleDeath()
        {
            base.HandleDeath();
            GetComponent<PlayerMovementController>().Model.SetActive(false);
            Destroy(gameObject.GetComponent<Player>());
            Destroy(gameObject.GetComponent<PlayerMovementController>());
            Destroy(gameObject.GetComponent<PlayerInputsHandler>());
            m_ragdollModel?.ApplyModelPositionsToClone();
            m_ragdollModel?.gameObject.SetActive(true);
        }

        public virtual void HandleAttackStartedInput()
        { }

        public virtual void HandleAttackCanceledInput()
        { }

        public virtual void HandleSecondaryAttackStartedInput() { }
        public virtual void HandleSecondaryAttackCanceledInput() { }
    }
}