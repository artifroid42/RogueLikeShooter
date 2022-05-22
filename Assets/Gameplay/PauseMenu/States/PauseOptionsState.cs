using UnityEngine;

namespace RLS.Gameplay.PauseMenu
{
    public class PauseOptionsState : Options.GenericOptionsState
    {
        [SerializeField]
        private PauseFlowMachine m_pauseFlowMachine = null;
        protected override void HandleBackButtonPressed()
        {
            m_pauseFlowMachine.SwitchToPreviousState();
        }
    }
}