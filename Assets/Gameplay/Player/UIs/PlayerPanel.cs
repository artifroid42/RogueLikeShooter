using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Player.UI
{
    public class PlayerPanel : Panel
    {
        [SerializeField]
        private ExpBar m_expBar;

        public ExpBar ExpBar => m_expBar;
    }
}
