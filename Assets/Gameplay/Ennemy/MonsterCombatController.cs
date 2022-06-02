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

        protected override void HandleDeath()
        {
            base.HandleDeath();
            Destroy(gameObject.GetComponent<MonsterAI>());
            m_ragdollModel?.ApplyModelPositionsToClone();
            m_model?.SetActive(false);
            m_ragdollModel?.gameObject.SetActive(true);
            StartCoroutine(WaitingToDestroyMonster(5f));
        }

        private IEnumerator WaitingToDestroyMonster(float a_waitingDuration)
        {
            yield return new WaitForSeconds(a_waitingDuration);
            Destroy(gameObject);
        }
    }
}