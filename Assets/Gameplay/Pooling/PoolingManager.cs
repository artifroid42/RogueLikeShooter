using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Pooling
{
    public class PoolingManager : MonoBehaviour
    {
        #region Singleton
        private static PoolingManager s_instance = null;
        private void Awake()
        {
            if(s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(s_instance);
                s_instance.gameObject.name = "PoolingManager";
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static PoolingManager Instance
        {
            get
            {
                if(s_instance == null)
                {
                    GameObject GO = new GameObject("PoolingManager");
                    s_instance = GO.AddComponent<PoolingManager>();
                    DontDestroyOnLoad(GO);
                }
                return s_instance;
            }
        }

        #endregion

        private List<APoolingSystem> m_poolingSystems = new List<APoolingSystem>();

        protected void RegisterNewPoolingSystem(APoolingSystem a_poolingSystem)
        {
            m_poolingSystems.Add(a_poolingSystem);
            a_poolingSystem.transform.SetParent(transform);
        }

        protected void UnregisterPoolingSystem(APoolingSystem a_poolingSystem)
        {
            m_poolingSystems.Remove(a_poolingSystem);
        }

        public T GetPoolingSystem<T>() where T : APoolingSystem
        {
            m_poolingSystems.RemoveAll(x => x == null);
            for (int i = 0; i < m_poolingSystems.Count; ++i)
            {
                if(m_poolingSystems[i] is T)
                {
                    return m_poolingSystems[i] as T;
                }
            }

            return InitializeNewPoolingSystem<T>();
        }

        public T GetPoolingSystem<T>(int a_ID) where T : APoolingSystem
        {
            m_poolingSystems.RemoveAll(x => x == null);
            for (int i = 0; i < m_poolingSystems.Count; ++i)
            {
                if (m_poolingSystems[i] is T && m_poolingSystems[i].ID == a_ID)
                {
                    return m_poolingSystems[i] as T;
                }
            }

            return InitializeNewPoolingSystem<T>(a_ID);
        }

        private T InitializeNewPoolingSystem<T>() where T : APoolingSystem
        {
            GameObject system_GO = new GameObject($"{typeof(T)}");
            T system = system_GO.AddComponent<T>();
            RegisterNewPoolingSystem(system);
            return system;
        }

        private T InitializeNewPoolingSystem<T>(int a_ID) where T : APoolingSystem
        {
            GameObject system_GO = new GameObject($"{typeof(T)}");
            T system = system_GO.AddComponent<T>();
            system.ID = a_ID;
            RegisterNewPoolingSystem(system);
            return system;
        }

    }
}
