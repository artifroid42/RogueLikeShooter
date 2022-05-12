using RLS.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace RLS.Gameplay.Levels
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private List<Data.StageData> m_stageDatas = null;

        public Action OnLoadingStarted = null;
        public Action OnLoadingEnded = null;

        private Data.StageData m_currentStageData = null;
        private AsyncOperationHandle<SceneInstance> m_currentSceneHandle = default;

        private bool m_isLoadingAStage = false;

        public void LoadNextStage()
        {
            Debug.Log("Asking to load next stage");
            if (m_isLoadingAStage)
            {
                Debug.LogError("Already loading a stage !");
                return;
            }
            OnLoadingStarted?.Invoke();
            m_isLoadingAStage = true;
            unload_current_stage();

        }

        private void unload_current_stage()
        {
            if (m_currentStageData != null)
            {
                Debug.Log("Unloading Current Stage");
                m_currentStageData.OnStageUnloaded += OnCurrentStageUnloaded;
                m_currentStageData.UnloadStage(m_currentSceneHandle);
            }
            else
            {
                load_next_stage();
            }
        }

        private void OnCurrentStageUnloaded()
        {
            Debug.Log("Current Stage Unloaded");
            m_currentStageData.OnStageUnloaded -= OnCurrentStageUnloaded;
            load_next_stage();
        }

        private void load_next_stage()
        {
            Debug.Log("Loading Next Stage");
            m_currentStageData = m_stageDatas.PickRandom();

            m_currentStageData.OnStageLoaded += OnStageLoaded;
            m_currentStageData.LoadStage();
        }

        private void OnStageLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            m_currentStageData.OnStageLoaded -= OnStageLoaded;
            Debug.Log("Stage Loaded");
            m_currentSceneHandle = obj;
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(m_currentSceneHandle.Result.Scene);
            m_isLoadingAStage = false;
            OnLoadingEnded?.Invoke();
        }
    }
}