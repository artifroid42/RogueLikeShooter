using MOtter.SoundManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RLS
{
    public class SFXManager : MonoBehaviour
    {
        #region Singleton
        private static SFXManager s_instance = null;
        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                s_instance.gameObject.name = "SFXManager";
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static SFXManager Instance
        {
            get
            {
                if (s_instance == null)
                {
                    GameObject GO = new GameObject("SFXManager");
                    s_instance = GO.AddComponent<SFXManager>();
                }
                return s_instance;
            }
        }

        #endregion

        public SoundData ShootShuriken;
    }

}
