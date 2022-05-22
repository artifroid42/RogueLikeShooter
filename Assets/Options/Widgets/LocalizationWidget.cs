using System;
using UnityEngine;

namespace RLS.Options.Widget
{
    public class LocalizationWidget : MonoBehaviour
    {
        public Action OnNextArrowPressed = null;
        public Action OnPreviousArrowPressed = null;

        public void TriggerNextArrowHandling()
        {
            OnNextArrowPressed?.Invoke();
        }

        public void TriggerPreviousArrowHandling()
        {
            OnPreviousArrowPressed?.Invoke();
        }
    }
}