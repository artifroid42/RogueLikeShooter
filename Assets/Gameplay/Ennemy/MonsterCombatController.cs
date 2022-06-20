using RLS.Gameplay.Combat;
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

        private MonsterAI m_monsterAI = null;

        protected override void Start()
        {
            int lifePoints = (int)(m_maxLifePoints + m_maxLifePoints * MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>().LevelManager.LevelsCount * m_improvementOffset);
            SetMaxLifePoints(lifePoints, Combat.ECurrentLifeBehaviourWhenChangingMaxLife.SetToMaxLife);
            m_monsterAI = GetComponent<MonsterAI>();
        }

        public override void TakeDamage(int a_damageToDeal, CombatController a_source)
        {
            base.TakeDamage(a_damageToDeal, a_source);
            if(m_monsterAI != null)
            {
                m_monsterAI.HPLostFeedback.gameObject.SetActive(true);
                m_monsterAI.HPLostFeedback.SetLostHPAmount(a_damageToDeal);
            }
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
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<UnityEngine.AI.NavMeshAgent>());
        }

        private IEnumerator WaitingToDestroyMonster(float a_waitingDuration)
        {
            yield return new WaitForSeconds(a_waitingDuration);
            Destroy(gameObject);
        }
    }
}