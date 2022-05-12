using UnityEngine;

namespace RLS.Gameplay.Levels
{
    public class Stage : MonoBehaviour
    {
        [SerializeField]
        private Transform m_spawningPosition = null;
        public Transform SpawningPosition => m_spawningPosition;

        [SerializeField]
        private EndPortal m_endPortal = null;
        public EndPortal EndPortal => m_endPortal;
    }
}