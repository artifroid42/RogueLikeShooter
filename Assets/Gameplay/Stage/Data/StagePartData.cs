using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Stage.Data
{
    [CreateAssetMenu(fileName = "StagePartData", menuName = "RLS/Gameplay/Stage/Stage Part Data")]
    public class StagePartData : ScriptableObject
    {
        [SerializeField]
        private StagePart m_partPrefab = null;
        [SerializeField]
        private List<GameObject> m_spawnableEnnemies = null;

        public StagePart PartPrefab => m_partPrefab;
        public List<GameObject> SpawnableEnnemies => m_spawnableEnnemies;
    }
}