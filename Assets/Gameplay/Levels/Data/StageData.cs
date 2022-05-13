using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace RLS.Gameplay.Levels.Data
{
    [CreateAssetMenu(fileName = "StageData", menuName = "RLS/Gameplay/Levels/Stage Data")]
    public class StageData : ScriptableObject
    {
        [SerializeField]
        private AssetReference m_stageScene = null;
        [SerializeField]
        private AssetReference m_cameraSettingsScene = null;

        public Action<AsyncOperationHandle<SceneInstance>> OnStageLoaded = null;
        public Action OnStageUnloaded = null;

        public void LoadStage()
        {
            MOtter.MOtt.GM.StartCoroutine(LoadingStageRoutine());
        }

        private IEnumerator LoadingStageRoutine()
        {
            var obj = Addressables.LoadSceneAsync(m_stageScene, LoadSceneMode.Additive);
            while(!obj.IsDone)
            {
                yield return null;
            }
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                OnStageLoaded?.Invoke(obj);
            }
        }


        public void UnloadStage(AsyncOperationHandle<SceneInstance> a_handle)
        {
            MOtter.MOtt.GM.StartCoroutine(UnloadStageRoutine(a_handle));
        }

        private IEnumerator UnloadStageRoutine(AsyncOperationHandle<SceneInstance> a_handle)
        {
            var obj = Addressables.UnloadSceneAsync(a_handle);
            while(!obj.IsDone)
            {
                yield return null;
            }
            OnStageUnloaded?.Invoke();
        }
    }
}