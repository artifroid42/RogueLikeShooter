using RLS.Gameplay.Pooling;
using UnityEngine;

namespace RLS.Gameplay.Player.PopUp
{
    public class PopUpManager : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField]
        private PopUp m_popUpPrefab = null;
        [SerializeField]
        private Transform m_popUpContainer = null;

        [Header("Pop Up Params")]
        [SerializeField]
        private string m_notAllEnnemyDeadKey = "";
        [SerializeField]
        private Color m_notAllEnnemyDeadColor = Color.red;
        [SerializeField]
        private float m_notAllEnnemyDeadDuration = 3f;

        [SerializeField]
        private string m_AllEnnemyDeadKey = "";
        [SerializeField]
        private Color m_AllEnnemyDeadColor = Color.white;
        [SerializeField]
        private float m_allEnnemyDeadDuration = 5f;

        public void PopUpAllEnnemyDead()
        {
            PopUp newPopUp = PoolingManager.Instance.GetPoolingSystem<PopUpPoolingSystem>().
                GetObject(m_popUpPrefab, m_popUpContainer.transform.position, Quaternion.identity, m_popUpContainer);
            newPopUp.SetUp(m_AllEnnemyDeadKey, m_AllEnnemyDeadColor, m_allEnnemyDeadDuration);
        }

        public void PopUpNotAllEnnemyDead()
        {
            PopUp newPopUp = PoolingManager.Instance.GetPoolingSystem<PopUpPoolingSystem>().
                GetObject(m_popUpPrefab, m_popUpContainer.transform.position, Quaternion.identity, m_popUpContainer);
            newPopUp.SetUp(m_notAllEnnemyDeadKey, m_notAllEnnemyDeadColor, m_notAllEnnemyDeadDuration);
        }
    }
}
