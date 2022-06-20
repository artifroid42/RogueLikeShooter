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
        public SoundData EnemyTouched;
        public SoundData Death;
        public SoundData Jump;
        public SoundData LevelUp;
        public SoundData Menu;
        public SoundData PowerNinja;
        public SoundData PowerPirate;
        public SoundData PowerSciFILoad;
        public SoundData PowerSciFIShoot;
        public SoundData ShootPirate;
        public SoundData ShootSciFi;
        public SoundData UpgradePerformed;
        public SoundData Walk;
        public SoundData PlayerHitted;
    }

}
