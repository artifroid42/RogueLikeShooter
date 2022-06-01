using MOtter.StatesMachine;
using UnityEngine;

namespace RLS.MainMenu
{
    public class MainMenuGameMode : MainFlowMachine
    {
        [SerializeField]
        private Options.GenericOptionsState m_optionsState = null;
        [SerializeField]
        private ModeSelectionScreen.ModeSelectionScreenState m_modeSelectionScreenState = null;

        public void SwitchToOptions()
        {
            SwitchToState(m_optionsState);
        }

        public void SwitchToModeSelectionScreen()
        {
            SwitchToState(m_modeSelectionScreenState);
        }
    }
}