using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraHub = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraTestChoice = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraTraining = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraCharacterHome = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraBookControl = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraBookSkill = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraTestControl = null;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera virtualCameraTestSkill = null;

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

    [ContextMenu("Switch To Character Home")]
    public void SwitchToCharacterHome() {
        SwitchToVirtualCamera(virtualCameraCharacterHome);
    }

    [ContextMenu("Switch To Book Control")]
    public void SwitchToBookControl() {
        SwitchToVirtualCamera(virtualCameraBookControl);
    }

    [ContextMenu("Switch To Book Skill")]
    public void SwitchToBookSkill() {
        SwitchToVirtualCamera(virtualCameraBookSkill);
    }

    [ContextMenu("Switch To Test Control")]
    public void SwitchToTestControl() {
        SwitchToVirtualCamera(virtualCameraTestControl);
    }

    [ContextMenu("Switch To Test Skill")]
    public void SwitchToTestSkill() {
        SwitchToVirtualCamera(virtualCameraTestSkill);
    }

    private void SwitchToVirtualCamera(Cinemachine.CinemachineVirtualCamera virtualCamera) {
        if (previousActiveCamera != null) {
            previousActiveCamera.gameObject.SetActive(false);
        }
        virtualCamera.gameObject.SetActive(true);
        previousActiveCamera = virtualCamera;
    }

    private void DisableAllVirtualCameras() {
        virtualCameraHub.gameObject.SetActive(false);
        virtualCameraTestChoice.gameObject.SetActive(false);
        virtualCameraTraining.gameObject.SetActive(false);
        virtualCameraBookControl.gameObject.SetActive(false);
        virtualCameraBookSkill.gameObject.SetActive(false);
        virtualCameraTestControl.gameObject.SetActive(false);
        virtualCameraTestSkill.gameObject.SetActive(false);
        virtualCameraCharacterHome.gameObject.SetActive(false);
    }
}
