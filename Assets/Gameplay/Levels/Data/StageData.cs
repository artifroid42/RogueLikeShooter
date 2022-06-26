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
        private enum EPlayerType
        {
            Ninja,
            Pirate,
            Scifi
        }
        [SerializeField]
        private AssetReference m_stageScene = null;
        [SerializeField]
        private AssetReference m_cameraSettingsScene = null;
        [SerializeField]
        private EPlayerType m_playerType = EPlayerType.Ninja;

        public Action<AsyncOperationHandle<SceneInstance>, AsyncOperationHandle<SceneInstance>> OnStageLoaded = null;
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
            var obj2 = Addressables.LoadSceneAsync(m_cameraSettingsScene, LoadSceneMode.Additive);
            while (!obj2.IsDone)
            {
                yield return null;
            }
            yield return null;
            string playerKey = "";
            switch(m_playerType)
            {
                case EPlayerType.Ninja:
                    playerKey = "NinjaPlayer";
                    break;
                case EPlayerType.Pirate:
                    playerKey = "PiratePlayer";
                    break;
                case EPlayerType.Scifi:
                    playerKey = "ScifiPlayer";
                    break;
            }
            var objPlayer = Addressables.LoadAssetAsync<GameObject>(playerKey);
            while(!objPlayer.IsDone)
            {
                yield return null;
            }
            Instantiate(objPlayer.Result, FindObjectOfType<Stage>().transform);
            yield return null;
            if (obj.Status == AsyncOperationStatus.Succeeded && obj2.Status == AsyncOperationStatus.Succeeded)
            {
                OnStageLoaded?.Invoke(obj, obj2);
            }
        }


        public void UnloadStage(AsyncOperationHandle<SceneInstance> a_handle, AsyncOperationHandle<SceneInstance> a_handle2)
        {
            MOtter.MOtt.GM.StartCoroutine(UnloadStageRoutine(a_handle, a_handle2));
        }

        private IEnumerator UnloadStageRoutine(AsyncOperationHandle<SceneInstance> a_handle, AsyncOperationHandle<SceneInstance> a_handle2)
        {
            var obj = Addressables.UnloadSceneAsync(a_handle);
            while(!obj.IsDone)
            {
                yield return null;
            }
            obj = Addressables.UnloadSceneAsync(a_handle2);
            while (!obj.IsDone)
            {
                yield return null;
            }
            yield return null;
            OnStageUnloaded?.Invoke();
        }
    }
}