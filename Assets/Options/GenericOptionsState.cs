using MOtter.StatesMachine;

namespace RLS.Options
{
    public class GenericOptionsState : FlowState
    {
        private OptionsPanel m_panel = null;
        internal override void RegisterReferences()
        {
            base.RegisterReferences();
            m_panel = GetPanel<OptionsPanel>();
        }

        internal override void SetUpDependencies()
        {
            base.SetUpDependencies();
            m_panel.CamSensitivitySlider.value = MOtter.MOtt.SAVE.CameraSensitivity;
            m_panel.SfxVolumeSlider.value = MOtter.MOtt.SOUND.GetVolume(MOtter.SoundManagement.ESoundCategoryName.SFX);
            m_panel.MusicVolumeSlider.value = MOtter.MOtt.SOUND.GetVolume(MOtter.SoundManagement.ESoundCategoryName.Music);
        }

        internal override void RegisterEvents()
        {
            base.RegisterEvents();
            m_panel.CamSensitivitySlider.onValueChanged.AddListener(HandleCamSensitivityValueChanged);
            m_panel.SfxVolumeSlider.onValueChanged.AddListener(HandleSFXValueChanged);
            m_panel.MusicVolumeSlider.onValueChanged.AddListener(HandleMusicValueChanged);
            m_panel.LocalizationWidget.OnPreviousArrowPressed += HandlePreviousArrowPressed;
            m_panel.LocalizationWidget.OnNextArrowPressed += HandleNextArrowPressed;
            m_panel.BackButton.onClick.AddListener(HandleBackButtonPressed);
        }



        internal override void UnregisterEvents()
        {
            base.UnregisterEvents();
            m_panel.CamSensitivitySlider.onValueChanged.RemoveListener(HandleCamSensitivityValueChanged);
            m_panel.SfxVolumeSlider.onValueChanged.RemoveListener(HandleSFXValueChanged);
            m_panel.MusicVolumeSlider.onValueChanged.RemoveListener(HandleMusicValueChanged);
            m_panel.LocalizationWidget.OnPreviousArrowPressed -= HandlePreviousArrowPressed;
            m_panel.LocalizationWidget.OnNextArrowPressed -= HandleNextArrowPressed;
            m_panel.BackButton.onClick.RemoveListener(HandleBackButtonPressed);
        }
        private void HandleCamSensitivityValueChanged(float arg0)
        {
            MOtter.MOtt.SAVE.CameraSensitivity = m_panel.CamSensitivitySlider.value;
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
        }

        private void HandleNextArrowPressed()
        {
            MOtter.MOtt.LANG.SwitchToNextLanguage();
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
        }

        private void HandlePreviousArrowPressed()
        {
            MOtter.MOtt.LANG.SwitchToPreviousLanguage();
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
        }

        private void HandleMusicValueChanged(float a_musicVolume)
        {
            MOtter.MOtt.SOUND.SetVolume(a_musicVolume, MOtter.SoundManagement.ESoundCategoryName.Music);
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
        }

        private void HandleSFXValueChanged(float a_sfxVolume)
        {
            MOtter.MOtt.SOUND.SetVolume(a_sfxVolume, MOtter.SoundManagement.ESoundCategoryName.SFX);
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
        }

        protected virtual void HandleBackButtonPressed()
        {
            MOtter.MOtt.GM.GetCurrentMainStateMachine<MainFlowMachine>().SwitchToPreviousState();
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Menu);
        }

        public override void ExitState()
        {
            MOtter.MOtt.SAVE.SaveSaveDataManager();
            base.ExitState();
        }
    }
}