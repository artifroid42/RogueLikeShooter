using RLS.Gameplay.Combat;
using RLS.Gameplay.Ennemy;
using RLS.Generic.Ragdoll;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerCombatController : Combat.CombatController, IPlayerInputsObserver
    {
        protected Player m_player = null;

        public virtual float PowerCooldownRatio => 0;

        [SerializeField]
        private SyncCloneFromModel m_ragdollModel = null;

        private MonsterAI m_lastMonsterIAWatched = null;

        protected override void Start()
        {
            base.Start();
            GetComponent<PlayerInputsHandler>()?.RegisterNewObserver(this);
            m_player = GetComponent<Player>();
        }

        private void OnDestroy()
        {
            GetComponent<PlayerInputsHandler>()?.UnregisterObserver(this);
        }

        protected override void HandleDeath()
        {
            base.HandleDeath();
            GetComponent<PlayerMovementController>().Model.SetActive(false);
            Destroy(gameObject.GetComponent<Player>());
            Destroy(gameObject.GetComponent<PlayerMovementController>());
            Destroy(gameObject.GetComponent<PlayerInputsHandler>());
            m_ragdollModel?.ApplyModelPositionsToClone();
            m_ragdollModel?.gameObject.SetActive(true);
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.Death);
            MOtter.MOtt.GM.GetCurrentMainStateMachine<DungeonFlow.DungeonGameMode>().Lose();
        }

        public override void TakeDamage(int a_damageToDeal, CombatController a_source)
        {
            base.TakeDamage(a_damageToDeal, a_source);
            MOtter.MOtt.SOUND.Play2DSound(SFXManager.Instance.PlayerHitted);
        }

        private void LateUpdate()
        {
            if (Camera.main != null && Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hitInfo))
            {
                if(hitInfo.collider.TryGetComponent(out MonsterAI monster))
                {
                    if(m_lastMonsterIAWatched == null)
                    {
                        m_lastMonsterIAWatched = monster;
                        m_lastMonsterIAWatched.SetWatched(true);
                    }
                    else if(m_lastMonsterIAWatched != monster)
                    {
                        m_lastMonsterIAWatched.SetWatched(false);
                        m_lastMonsterIAWatched = monster;
                        m_lastMonsterIAWatched.SetWatched(true);
                    }
                }
                else
                {
                    if(m_lastMonsterIAWatched != null)
                    {
                        m_lastMonsterIAWatched.SetWatched(false);
                        m_lastMonsterIAWatched = null;
                    }
                }
            }
        }

        public virtual void HandleAttackStartedInput()
        { }

        public virtual void HandleAttackCanceledInput()
        { }

        public virtual void HandleSecondaryAttackStartedInput() { }
        public virtual void HandleSecondaryAttackCanceledInput() { }
    }
}