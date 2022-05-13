using UnityEngine;

namespace RLS.Gameplay.Levels.Data
{
    [CreateAssetMenu(fileName = "GameConfigurationData", menuName = "RLS/Gameplay/Levels/Game Configuration Data")]
    public class GameConfigurationData : ScriptableObject
    {
        [SerializeField]
        private StageListData m_stageListData = null;
        public StageListData StageListData => m_stageListData;
    }
}