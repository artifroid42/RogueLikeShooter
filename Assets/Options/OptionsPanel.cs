using UnityEngine;
using UnityEngine.UI;

namespace RLS.Options
{
    public class OptionsPanel : Panel
    {
        [SerializeField]
        private Slider m_camSensitivitySlider = null;
        [SerializeField]
        private Slider m_sfxVolumeSlider = null;
        [SerializeField]
        private Slider m_musicVolumeSlider = null;
        [SerializeField]
        private Widget.LocalizationWidget m_localizationWidget = null;
        [SerializeField]
        private Button m_backButton = null;
        public Slider CamSensitivitySlider => m_camSensitivitySlider;
        public Slider SfxVolumeSlider => m_sfxVolumeSlider;
        public Slider MusicVolumeSlider => m_musicVolumeSlider;
        public Widget.LocalizationWidget LocalizationWidget => m_localizationWidget;
        public Button BackButton => m_backButton;
    }
}