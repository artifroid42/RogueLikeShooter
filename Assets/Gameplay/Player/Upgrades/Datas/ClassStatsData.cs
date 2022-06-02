using UnityEngine;


namespace RLS.Gameplay.Player.Upgrades
{
    public enum EClass
    {
        Ninja,
        Pirate,
        SciFi
    }

    [CreateAssetMenu(fileName = "ClassStatsData", menuName = "RLS/Gameplay/Upgrades/Class Stats Data")]
    public class ClassStatsData : ScriptableObject
    {
        [SerializeField]
        private int m_maxUpgradesCount = 10;

        [Header("Ressources")]
        [SerializeField]
        private EClass m_class;
        [SerializeField]
        private Sprite m_classSprite;


        public EClass Class => m_class;
        public Sprite ClassSprite => m_classSprite;

        public int MaxUpgradesCount => m_maxUpgradesCount; 
    }
}
