using System.Collections.Generic;
using UnityEngine;

namespace RLS.Gameplay.Pooling
{
    public abstract class APoolingSystem : MonoBehaviour
    {
        public int ID = -1;
    }
    public abstract class APoolingSystem<T> : APoolingSystem where T : Component
    {
        private List<T> m_instantiatedObjects = new List<T>();

        /// <summary>
        /// Be sure to always use the same prefab, because it may return already spawned objects.
        /// </summary>
        /// <param name="a_prefabToSpawn"></param>
        /// <param name="a_position"></param>
        /// <param name="a_rotation"></param>
        /// <param name="a_parent"></param>
        /// <returns></returns>
        public T GetObject(T a_prefabToSpawn, Vector3 a_position, Quaternion a_rotation, Transform a_parent = null)
        {
            m_instantiatedObjects.RemoveAll(x => x == null);
            T objectToGet = m_instantiatedObjects.Find(x => !x.gameObject.activeSelf);
            if (objectToGet == null)
            {
                objectToGet = Object.Instantiate(a_prefabToSpawn);
                m_instantiatedObjects.Add(objectToGet);
            }
            objectToGet.transform.SetParent(a_parent);
            objectToGet.transform.position = a_position;
            objectToGet.transform.rotation = a_rotation;
            objectToGet.gameObject.SetActive(true);
            return objectToGet;
        }
    }
}