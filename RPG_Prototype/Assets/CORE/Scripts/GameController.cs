using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Screens {
    Init,
    Hub,
    TestTypeChoice,
    SkillTestChoice,
    SkillCatTestChoice,
    TrainingChoice
}

public class GameController : MonoBehaviour {

    public Screens ActiveScreen {
        get {
            return activeScreen;
        }
    }

    [SerializeField] private CameraController cameraController = null;

    [Header("Game Controllers")]
    [SerializeField] private HubController hubController = null;
    [SerializeField] private TestChoiceController testTypeChoiceController = null;

    private BaseSubController activeController = null;
    private Screens activeScreen = Screens.Init;

    private void Awake() {
        foreach (BaseSubController controller in GetComponentsInChildren<BaseSubController>()) {
            controller.gameObject.SetActive(false);
        }
    }

    private void Start() {
        GoToScreen(Screens.Hub);
    }

    public void GoToScreen(Screens screen) {
        if (screen == activeScreen) { return; }

        switch (screen) {
            case Screens.Hub:
                cameraController.SwitchToHub();
                SwitchToController(hubController);
                break;
            case Screens.TestTypeChoice:
                cameraController.SwitchToChoice();
                SwitchToController(testTypeChoiceController);
                break;
            case Screens.TrainingChoice:
                cameraController.SwitchToTraining();
                break;
            default:
                throw new System.NotImplementedException("No handling implemented for screen " + screen);
        }
    }

    private void SwitchToController(BaseSubController controller) {
        if (activeController != null) {
            activeController.gameObject.SetActive(false);
        }
        activeController = controller;
        activeController.gameObject.SetActive(true);
    }
}
