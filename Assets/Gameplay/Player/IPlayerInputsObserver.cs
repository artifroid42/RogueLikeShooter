using UnityEngine;

namespace RLS.Gameplay.Player
{
    public interface IPlayerInputsObserver
    {
        void HandleMovementInput(Vector2 a_moveInputs) { }
        void HandleLookAroundInput(Vector2 a_lookAroundInputs) { }
        void HandlePauseInput() { }
        void HandleAttackStartedInput() { }
        void HandleAttackCanceledInput() { }
        void HandleSecondaryAttackStartedInput() { }
        void HandleSecondaryAttackCanceledInput() { }
        void HandleJumpInput() { }
        
        void HandleUpgradeOneInput() { }
        void HandleUpgradeTwoInput() { }
        void HandleUpgradeThreeInput() { }
        void HandleUpgradeFourInput() { }
        void HandleUpgradeFiveInput() { }
        void HandleUpgradeSixInput() { }
    }
}