using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Levels.Data
{
    [CreateAssetMenu(fileName = "StageListData", menuName = "RLS/Gameplay/Levels/Stage List Data")]
    public class StageListData : ScriptableObject
    {
        [System.Serializable]
        private struct WeightedStageData
        {
            public StageData StageData;
            public int SpawnWeight;
        }
        [SerializeField]
        private List<WeightedStageData> m_stageData = null;

        public StageData GetRandomStageData()
        {
            int totalWeight = 0;
            for(int i = 0; i < m_stageData.Count; ++i)
            {
                totalWeight += m_stageData[i].SpawnWeight;
            }

            int weightToReach = Random.Range(0, totalWeight) + 1;

            totalWeight = 0;
            for (int i = 0; i < m_stageData.Count; ++i)
            {
                totalWeight += m_stageData[i].SpawnWeight;
                if(totalWeight >= weightToReach)
                {
                    return m_stageData[i].StageData;
                }
            }

            return null;
        }
    }
}