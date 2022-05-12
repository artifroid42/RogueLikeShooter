using System;
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

        public Action<AsyncOperationHandle<SceneInstance>> OnStageLoaded = null;
        public Action OnStageUnloaded = null;

        public void LoadStage()
        {
            Addressables.LoadSceneAsync(m_stageScene, LoadSceneMode.Additive).Completed += HandleStageLoaded;
        }

        private void HandleStageLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            if(obj.Status == AsyncOperationStatus.Succeeded)
            {
                OnStageLoaded?.Invoke(obj);
            }
        }

        public void UnloadStage(AsyncOperationHandle<SceneInstance> a_handle)
        {
            Addressables.UnloadSceneAsync(a_handle).Completed += HandleStageUnloaded;
        }

        private void HandleStageUnloaded(AsyncOperationHandle<SceneInstance> obj)
        {
            OnStageUnloaded?.Invoke();
        }
    }
}