using MOtter.StatesMachine;
using UnityEngine;

namespace RLS.Gameplay.PauseMenu
{
    public class PauseMenuState : FlowState
    {
        [SerializeField]
        protected PauseFlowMachine m_pauseGamemode = null;
    }
}