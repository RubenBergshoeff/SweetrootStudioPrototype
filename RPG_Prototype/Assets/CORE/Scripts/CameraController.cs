using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraHub = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraTestChoice = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraTraining = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraOpenBook = null;

    private Cinemachine.CinemachineVirtualCamera previousActiveCamera = null;

    private void Awake() {
        DisableAllVirtualCameras();
    }

    [ContextMenu("Switch To Hub")]
    public void SwitchToHub() {
        SwitchToVirtualCamera(virtualCameraHub);
    }

    [ContextMenu("Switch To Choice")]
    public void SwitchToChoice() {
        SwitchToVirtualCamera(virtualCameraTestChoice);
    }

    [ContextMenu("Switch To Training")]
    public void SwitchToTraining() {
        SwitchToVirtualCamera(virtualCameraTraining);
    }

    [ContextMenu("Switch To Book")]
    public void SwitchToBook() {
        SwitchToVirtualCamera(virtualCameraOpenBook);
    }

    private void SwitchToVirtualCamera(Cinemachine.CinemachineVirtualCamera camera) {
        if (previousActiveCamera != null) {
            previousActiveCamera.gameObject.SetActive(false);
        }
        camera.gameObject.SetActive(true);
        previousActiveCamera = camera;
    }

    private void DisableAllVirtualCameras() {
        virtualCameraHub.gameObject.SetActive(false);
        virtualCameraTestChoice.gameObject.SetActive(false);
        virtualCameraTraining.gameObject.SetActive(false);
        virtualCameraOpenBook.gameObject.SetActive(false);
    }
}
