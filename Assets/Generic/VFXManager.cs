using RLS.Gameplay.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace RLS.Generic.VFXManager
{
    public class VFXManager : MonoBehaviour
    {
        #region Singleton
        private static VFXManager s_instance = null;
        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(s_instance);
                s_instance.gameObject.name = "VFXManager";
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static VFXManager Instance
        {
            get
            {
                if (s_instance == null)
                {
                    GameObject GO = new GameObject("VFXManager");
                    s_instance = GO.AddComponent<VFXManager>();
                    DontDestroyOnLoad(GO);
                }
                return s_instance;
            }
        }

        #endregion

        [SerializeField]
        private struct VFXData
        {
            public ParticleSystem VFXPrefab;
            public int ID;
        }

        [SerializeField]
        private List<VFXData> m_vfxDataList = null;

        public ParticleSystem PlayFXAt(int a_fxId, Vector3 a_position, Quaternion a_rotation, Transform a_parent = null)
        {
            ParticleSystem fx = PoolingManager.Instance.GetPoolingSystem<VFXPoolingSystem>(a_fxId).
                GetObject(m_vfxDataList.Find(x => x.ID == a_fxId).VFXPrefab,
                a_position, 
                a_rotation, 
                a_parent);
            return fx;
        }
    }
}