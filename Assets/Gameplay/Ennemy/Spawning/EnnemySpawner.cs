using UnityEngine;

namespace RLS.Gameplay.Ennemy.Spawning
{
    public class EnnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Data.EnnemyToSpawnData m_ennemyToSpawnData = null;
        private MonsterAI m_ennemy = null;
        public MonsterAI Ennemy => m_ennemy;


        public void InstantiateRandomEnnemy()
        {
            var ennemyPrefab = m_ennemyToSpawnData.GetRandomEnnemy();

            m_ennemy = Instantiate(ennemyPrefab, transform.position, Quaternion.identity, transform);
        }
    }
    
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(EnnemySpawner), true)]
    public class EnnemySpawnEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Instantiate Random Ennemy"))
            {
                (target as EnnemySpawner).InstantiateRandomEnnemy();
            }
        }
    }
#endif
}
