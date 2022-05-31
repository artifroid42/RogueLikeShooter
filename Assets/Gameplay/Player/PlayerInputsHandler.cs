using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RLS.Gameplay.Player
{
    public class PlayerInputsHandler : MonoBehaviour
    {
        private PlayerInputsActions m_actions = null;
        private List<IPlayerInputsObserver> m_observers = new List<IPlayerInputsObserver>();

        public void ActivateInputs()
        {
            if (m_actions == null)
                m_actions = new PlayerInputsActions();
            m_actions.Enable();
            m_actions.Gameplay.Jump.performed += Jump_performed;
            m_actions.Gameplay.Attack.started += Attack_started;
            m_actions.Gameplay.Attack.canceled += Attack_canceled;
            m_actions.Gameplay.Pause.performed += Pause_performed;

            m_actions.Gameplay.OneUpgrade.performed += OneUpgrade_performed;
            m_actions.Gameplay.TwoUpgrade.performed += TwoUpgrade_performed;
            m_actions.Gameplay.ThreeUpgrade.performed += ThreeUpgrade_performed;
            m_actions.Gameplay.FourUpgrade.performed += FourUpgrade_performed;
            m_actions.Gameplay.FiveUpgrade.performed += FiveUpgrade_performed;
            m_actions.Gameplay.SixUpgrade.performed += SixUpgrade_performed;
        }

        public void RegisterNewObserver(IPlayerInputsObserver a_newObserver)
        {
            m_observers.Add(a_newObserver);
        }
        public void UnregisterObserver(IPlayerInputsObserver a_observer)
        {
            m_observers.Remove(a_observer);
        }
        private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandlePauseInput());
        }
        private void Attack_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleAttackCanceledInput());
        }
        private void Attack_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleAttackStartedInput());
        }
        private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleJumpInput());
        }
        private void OneUpgrade_performed(InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleUpgradeOneInput());
        }
        private void TwoUpgrade_performed(InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleUpgradeTwoInput());
        }
        private void ThreeUpgrade_performed(InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleUpgradeThreeInput());
        }
        private void FourUpgrade_performed(InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleUpgradeFourInput());
        }
        private void FiveUpgrade_performed(InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleUpgradeFiveInput());
        }
        private void SixUpgrade_performed(InputAction.CallbackContext obj)
        {
            m_observers?.ForEach(x => x.HandleUpgradeSixInput());
        }

        private void read_continuous_inputs()
        {
            if (m_actions == null) return;

            m_observers?.ForEach(x => x.HandleMovementInput(m_actions.Gameplay.Move.ReadValue<Vector2>()));
            m_observers?.ForEach(x => x.HandleLookAroundInput(m_actions.Gameplay.LookAround.ReadValue<Vector2>()));
        }

        private void Update()
        {
            read_continuous_inputs();
        }

        public void DeactivateInputs()
        {
            if (m_actions == null) return;
            m_actions.Gameplay.Jump.performed -= Jump_performed;
            m_actions.Gameplay.Attack.started -= Attack_started;
            m_actions.Gameplay.Attack.canceled -= Attack_canceled;
            m_actions.Gameplay.Pause.performed -= Pause_performed;
            m_actions.Disable();
        }
    }
}