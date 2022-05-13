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
        void HandleJumpInput() { }
    }
}