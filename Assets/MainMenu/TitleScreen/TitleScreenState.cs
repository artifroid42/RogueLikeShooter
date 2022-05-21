using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.MainMenu.TitleScreen
{
    public class TitleScreenState : MainMenuState
    {
        private PlayerInputsActions m_actions = null;
        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_actions = new PlayerInputsActions();
        }
        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_actions.Enable();
        }
        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_actions.MainMenu.TitleScreenPress.performed += TitleScreenPress_performed;
        }

        private void TitleScreenPress_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            m_gamemode.SwitchToNextState();
        }

        internal override void UnregisterEvents()
        {
            m_actions.MainMenu.TitleScreenPress.performed -= TitleScreenPress_performed;
            base.UnregisterEvents();
        }
    }
}