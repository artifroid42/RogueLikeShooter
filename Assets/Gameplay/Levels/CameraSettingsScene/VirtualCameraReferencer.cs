using System.Collections;
using UnityEngine;

namespace RLS.Gameplay.Levels.CameraSettingsScene
{
    [RequireComponent(typeof(Cinemachine.CinemachineVirtualCamera))]
    public class VirtualCameraReferencer : MonoBehaviour
    {
        private Cinemachine.CinemachineVirtualCamera m_virtualCam = null;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(WaitingForPlayerToBeInstantiated());
        }

        private IEnumerator WaitingForPlayerToBeInstantiated()
        {
            while(FindObjectOfType<Player.PlayerMovementController>() == null)
            {
                yield return null;
            }
            m_virtualCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
            var camTarget = FindObjectOfType<Player.PlayerMovementController>().CameraTarget;
            m_virtualCam.Follow = camTarget;
            m_virtualCam.LookAt = camTarget;
        }
    }
}