using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.UI
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_hitFeedback;
        [SerializeField]
        private Animator m_animator;

        public void PlayHitFeedback()
        {
            m_animator.SetTrigger("HIT");
        }
    }
}

