using UnityEngine;

namespace RLS.Gameplay.Player
{
    public class PlayerCombatController : Combat.CombatController, IPlayerInputsObserver
    {
        [SerializeField]
        private Combat.Weapon.Shuriken m_shurikenPrefab = null;
        [SerializeField]
        private Transform m_shurikenSource = null;

        protected override void Start()
        {
            base.Start();
            GetComponent<PlayerInputsHandler>()?.RegisterNewObserver(this);
        }

        private void OnDestroy()
        {
            GetComponent<PlayerInputsHandler>()?.UnregisterObserver(this);
        }

        public void HandleAttackStartedInput() 
        {
            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hitInfo))
            {
                var newShuriken = Instantiate(m_shurikenPrefab, m_shurikenSource.position, Quaternion.LookRotation(hitInfo.point - m_shurikenSource.position));
                var damageDealer = newShuriken.GetComponent<Combat.DamageDealer>();
                damageDealer.SetOwner(this);
                damageDealer.CanDoDamage = true;
            }
            else
            {
                var newShuriken = Instantiate(m_shurikenPrefab, m_shurikenSource.position, GetComponent<PlayerMovementController>().CameraTarget.rotation);
                var damageDealer = newShuriken.GetComponent<Combat.DamageDealer>();
                damageDealer.SetOwner(this);
                damageDealer.CanDoDamage = true;
            }
        }
    }
}