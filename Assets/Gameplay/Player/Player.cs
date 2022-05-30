using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Sight Pos")]
        [SerializeField]
        private Transform[] m_seeablePositions = null;
        public Transform[] SeeablePositions => m_seeablePositions;

            
    }
}
