using RLS.Gameplay.Combat.HUD;
using RLS.Generic.Ragdoll;
using System.Collections;
using UnityEngine;

namespace RLS.Gameplay.Ennemy
{
    public class MonsterCombatController : Combat.CombatController
    {
        [SerializeField]
        private SyncCloneFromModel m_ragdollModel = null;
        [SerializeField]
        private GameObject m_model = null;
        [SerializeField]
        private float m_improvementOffset = 0.1f;

        protected override void Start()
        {
            m_lifePoints = (int)(m_maxLifePoints * MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>().LevelManager.LevelsCount * m_improvementOffset);
        }

        protected override void HandleDeath()
        {
            base.HandleDeath();
            Destroy(gameObject.GetComponent<MonsterAI>());
            m_ragdollModel?.ApplyModelPositionsToClone();
            m_model?.SetActive(false);
            m_ragdollModel?.gameObject.SetActive(true);
            StartCoroutine(WaitingToDestroyMonster(5f));
            Destroy(GetComponent<GenericCombatHUDManager>().HealthBar.gameObject);
            Destroy(GetComponent<GenericCombatHUDManager>());
        }

        private IEnumerator WaitingToDestroyMonster(float a_waitingDuration)
        {
            yield return new WaitForSeconds(a_waitingDuration);
            Destroy(gameObject);
        }
    }
}