using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace RLS.Gameplay.Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Data.GameConfigurationData m_gameConfiguration = null;

        public Action OnLoadingStarted = null;
        public Action OnLoadingEnded = null;

        private Data.StageData m_currentStageData = null;
        private AsyncOperationHandle<SceneInstance> m_currentSceneHandle = default;
        private AsyncOperationHandle<SceneInstance> m_currentCamSceneHandle = default;

        private bool m_isLoadingAStage = false;

        private int m_levelsCount = 0;
        public int LevelsCount => m_levelsCount; 

        public void LoadNextStage()
        {
            Debug.LogError("Asking to load next stage");
            if (m_isLoadingAStage)
            {
                Debug.LogError("Already loading a stage !");
                return;
            }
            OnLoadingStarted?.Invoke();
            m_isLoadingAStage = true;
            unload_current_stage();
            m_levelsCount++;
        }

        private void unload_current_stage()
        {
            if (m_currentStageData != null)
            {
                Debug.LogError("Unloading Current Stage");
                m_currentStageData.OnStageUnloaded += HandleCurrentStageUnloaded;
                m_currentStageData.UnloadStage(m_currentSceneHandle, m_currentCamSceneHandle);
            }
            else
            {
                load_next_stage();
            }
        }

        private void HandleCurrentStageUnloaded()
        {
            Debug.LogError("Current Stage Unloaded");
            m_currentStageData.OnStageUnloaded -= HandleCurrentStageUnloaded;
            load_next_stage();
        }

        private void load_next_stage()
        {
            Debug.LogError("Loading Next Stage");
            m_currentStageData = m_gameConfiguration.StageListData.GetRandomStageData();

            m_currentStageData.OnStageLoaded += HandleStageLoaded;
            m_currentStageData.LoadStage();
        }

        private void HandleStageLoaded(AsyncOperationHandle<SceneInstance> obj, AsyncOperationHandle<SceneInstance> obj2)
        {
            m_currentStageData.OnStageLoaded -= HandleStageLoaded;
            Debug.LogError($"Stage Loaded |scene loaded :{obj.Result.Scene.isLoaded} && cam scene loaded {obj2.Result.Scene.isLoaded}");
            m_currentSceneHandle = obj;
            m_currentCamSceneHandle = obj2;
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(m_currentSceneHandle.Result.Scene);
            m_isLoadingAStage = false;
            OnLoadingEnded?.Invoke();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                LoadNextStage();
            }
        }
#endif
    }
}