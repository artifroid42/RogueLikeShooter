using MOtter.LevelData;
using UnityEngine;

namespace RLS
{
    public class Root : MonoBehaviour
    {
        [SerializeField]
        private LevelData m_defaultLevelData = null;
        private void Start()
        {
            m_defaultLevelData.LoadLevel();
        }
    }
}