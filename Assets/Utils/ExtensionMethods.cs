using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace RLS.Utils
{
    internal static class ExtensionMethods
    {
        #region Private Fields

        // It's cheaper to multiply by the inverse than to divide by a number
        private const float INVERSE_OF_3600 = 1.0f / 3600.0f;

        #endregion Private Fields

        #region Float

        /// <summary>
        /// Maps the given value - expected to vary between <paramref name="a_inMin"/> and <paramref name="a_inMax"/> - to a value between <paramref name="a_outMin"/> and <paramref name="a_outMax"/> />
        /// </summary>
        /// <param name="a_value">The value to map</param>
        /// <param name="a_inMin">The expected minimum value for <paramref name="a_value"/></param>
        /// <param name="a_inMax">The expected maximum value for <paramref name="a_value"/>></param>
        /// <param name="a_outMin">The minimum output bound</param>
        /// <param name="a_outMax">The maximum output bound</param>
        /// <returns>The value mapped onto the output range</returns>
        public static float Map(this float a_value, float a_inMin, float a_inMax, float a_outMin, float a_outMax)
        {
            return a_outMin + (a_outMax - a_outMin) * (a_value - a_inMin) / (a_inMax - a_inMin);
        }

        /// <summary>
        /// Convert a given velocity in m/s to km/h
        /// </summary>
        /// <param name="speedInMPS">A speed value in m/s</param>
        /// <returns>The given speed converted to km/h</returns>
        public static float ToKPH(this float speedInMPS)
        {
            return (speedInMPS * 3600) * 0.001f;
        }

        /// <summary>
        /// Convert a given velocity in km/h to m/s
        /// </summary>
        /// <param name="speedInKPH">A speed value in km/h</param>
        /// <returns>The given speed converted to m/s</returns>
        public static float ToMPS(this float speedInKPH)
        {
            return (speedInKPH * 1000) * INVERSE_OF_3600;
        }

        #endregion Float

        #region Vector2

        public static bool Contains(this Vector2 a_vector, float a_value)
        {
            return a_vector.x <= a_value && a_vector.y >= a_value;
        }

        #endregion Vector2

        #region GameObject

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            var component = go.GetComponent<T>();

            if (component != null)
            {
                return component;
            }

            return go.AddComponent<T>();
        }

        public static void SetLayerRecursively(this GameObject obj, int layer)
        {
            obj.layer = layer;

            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }

        #endregion GameObject

        #region Transform

        public static void SetParentAndResetLocals(this Transform target, Transform parent)
        {
            target.SetParent(parent);
            target.localPosition = Vector3.zero;
            target.localScale = Vector3.one;
            target.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// Full hierarchy path of a transform.
        /// </summary>
        public static string GetScenePath(this Transform transform)
        {
            string path = transform.name;
            while (transform.parent != null)
            {
                transform = transform.parent;
                path = transform.name + "/" + path;
            }
            return path;
        }

        /// <param name="transform"></param>
        public static void DestroyChildren(this Transform transform)
        {
            List<GameObject> children = GetChildren(transform);
            transform.transform.DetachChildren();

            foreach (GameObject child in children)
            {
                Object.Destroy(child);
            }
        }

        public static void DestroyChildrenImmediate(this Transform transform)
        {
            List<GameObject> children = GetChildren(transform);
            transform.transform.DetachChildren();

            foreach (GameObject child in children)
            {
                Object.DestroyImmediate(child);
            }
        }

        public static List<GameObject> GetChildren(this Transform transform)
        {
            List<GameObject> children = new List<GameObject>();

            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }

            return children;
        }

        #endregion Transform

        #region IEnumerable/List/Array

        public static IEnumerable<T> OrderRandom<T>(this IEnumerable<T> a_enumerable)
        {
            return a_enumerable.OrderBy(a => Random.value).ToList();
        }

        public static T PickRandom<T>(this List<T> a_enumerable)
        {
            return a_enumerable[Random.Range(0, a_enumerable.Count)];
        }

        public static T PopRandom<T>(this List<T> a_enumerable)
        {
            T l_selected = a_enumerable.PickRandom();
            a_enumerable.Remove(l_selected);
            return l_selected;
        }

        public static T PickRandom<T>(this T[] a_enumerable)
        {
            return a_enumerable[Random.Range(0, a_enumerable.Length)];
        }

        #endregion IEnumerable/List/Array

        #region MonoBehaviour

        public static void InvokeNextFrame(this MonoBehaviour a_caller, Action a_function)
        {
            try
            {
                a_caller.StartCoroutine(_InvokeNextFrame(a_function));
            }
            catch
            {
                Debug.Log("Trying to invoke " + a_function.ToString() + " but it doesnt seem to exist");
            }
        }

        private static IEnumerator _InvokeNextFrame(Action a_function)
        {
            yield return new WaitForEndOfFrame();
            a_function();
        }

        #endregion MonoBehaviour
    }
}