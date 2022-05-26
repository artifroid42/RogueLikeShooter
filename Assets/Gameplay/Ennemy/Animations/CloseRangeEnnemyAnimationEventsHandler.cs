using RLS.Gameplay.Ennemy.CloseRange;
using UnityEngine;

namespace RLS.Gameplay.Ennemy.Animations
{
    public class CloseRangeEnnemyAnimationEventsHandler : MonoBehaviour
    {
        [SerializeField]
        private CloseRangeMonsterAI m_owner = null;
        public void StopAttacking()
        {
            Debug.Log("Stop attacking notified");
            m_owner.WeaponDamageDealer.CanDoDamage = false;
        }
    }
}