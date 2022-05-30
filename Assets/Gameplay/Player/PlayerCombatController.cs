using RLS.Gameplay.Combat.Weapon;
using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerCombatController : Combat.CombatController, IPlayerInputsObserver
    {
        [SerializeField]
        private Combat.Weapon.Shuriken m_shurikenPrefab = null;
        [SerializeField]
        private Transform m_shurikenSource = null;
        private Player m_player = null;


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

        public void HandleAttackStartedInput() 
        {
            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hitInfo))
            {
                var newShuriken = Pooling.PoolingManager.Instance.GetPoolingSystem<ShurikenPoolingSystem>().
                    GetObject(m_shurikenPrefab, 
                    m_shurikenSource.position, 
                    Quaternion.LookRotation(hitInfo.point - m_shurikenSource.position));
                var damageDealer = newShuriken.GetComponent<Combat.DamageDealer>();
                damageDealer.SetOwner(this);
                damageDealer.CanDoDamage = true;
            }
            else
            {
                var newShuriken = Pooling.PoolingManager.Instance.GetPoolingSystem<ShurikenPoolingSystem>().
                    GetObject(m_shurikenPrefab,
                    m_shurikenSource.position,
                    GetComponent<PlayerMovementController>().CameraTarget.rotation);
                var damageDealer = newShuriken.GetComponent<Combat.DamageDealer>();
                damageDealer.SetOwner(this);
                damageDealer.CanDoDamage = true;
            }
            m_player.AnimationsHandler.ThrowShuriken();
        }
    }
}