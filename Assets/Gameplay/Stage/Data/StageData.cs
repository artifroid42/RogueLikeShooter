using System.Collections.Generic;
using UnityEngine;
using RLS.Utils;

namespace RLS.Gameplay.Stage.Data
{
    [CreateAssetMenu(fileName = "StageData", menuName = "RLS/Gameplay/Stage/Stage Data")]
    public class StageData : ScriptableObject
    {
        [SerializeField]
        private List<StagePartData> m_stageParts = null;
        [SerializeField]
        private Vector2Int m_numberOfParts = new Vector2Int(2, 3);


        public List<StagePartData> GetStageParts()
        {
            List<StagePartData> randomizedStageParts = new List<StagePartData>(m_stageParts.OrderRandom<StagePartData>());

            int numberOfParts = Random.Range(m_numberOfParts.x, m_numberOfParts.y);

            List<StagePartData> stagePartsToUse = new List<StagePartData>();
            for(int i = 0; i < numberOfParts; ++i)
            {
                stagePartsToUse.Add(randomizedStageParts[i]);
            }

            return stagePartsToUse;
        }
    }
}