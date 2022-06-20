using MOtter.Localization;
using TMPro;
using Tween;
using UnityEngine;

namespace RLS.Gameplay.Player.PopUp
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField]
        private TextLocalizer m_textLocalizer = null;
        [SerializeField]
        private TextMeshProUGUI m_text = null;
        [SerializeField]
        private PositionTween m_tween = null;
        private float m_popUpDuration = 5f;
        private float m_timeOfStart;

        private void OnEnable()
        {
            m_tween.StartAllAttachedTweens();
            m_timeOfStart = Time.time;
        }

        public void SetUp(string a_textKey, Color a_color, float a_popUpDuration)
        {
            m_textLocalizer.SetKey(a_textKey);
            m_text.color = a_color;
            m_popUpDuration = a_popUpDuration;
            var rectTransform = GetComponent<RectTransform>();
            Vector2 temp = rectTransform.offsetMin;
            temp.x = 0f;
            rectTransform.offsetMin = temp;
            temp = rectTransform.offsetMax;
            temp.x = 0f;
            rectTransform.offsetMax = temp;
        }

        private void Update()
        {
            if(Time.time - m_timeOfStart > m_popUpDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }
}