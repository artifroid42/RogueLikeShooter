using MOtter.StatesMachine;
using UnityEngine;

namespace RLS.MainMenu
{
    public class MainMenuGameMode : MainFlowMachine
    {
        [SerializeField]
        private TitleScreen.TitleScreenState m_titleScreenState = null;
        [SerializeField]
        private MainScreen.MainScreenState m_mainScreenState = null;
    }
}