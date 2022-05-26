using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Ennemy.Spawning.Data
{
    [CreateAssetMenu(fileName = "Spawner Data", menuName = "RLS/Gameplay/Ennemy/Spawning/Spawner Data")]
    public class EnnemyToSpawnData : ScriptableObject
    {
        [System.Serializable]
        struct EnnemySpawningData
        {
            public MonsterAI EnnemyPrefab;
            public int SpawningWeight;
        }
        [SerializeField]
        private List<EnnemySpawningData> m_ennemiesPrefabs = null;

        public MonsterAI GetRandomEnnemy()
        {
            int totalWeight = 0;
            m_ennemiesPrefabs?.ForEach(x => totalWeight += x.SpawningWeight);

            int weightLimit = Random.Range(0, totalWeight);
            totalWeight = 0;
            for(int i = 0; i < m_ennemiesPrefabs.Count; ++i)
            {
                totalWeight += m_ennemiesPrefabs[i].SpawningWeight;
                if(totalWeight > weightLimit)
                {
                    return m_ennemiesPrefabs[i].EnnemyPrefab;
                }
            }
            return null;
        }
    }
}