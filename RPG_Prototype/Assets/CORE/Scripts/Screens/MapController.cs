using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {
    [SerializeField] private Button buttonMood = null;
    [SerializeField] private Button buttonTraining = null;
    [SerializeField] private Button buttonControlTest = null;
    [SerializeField] private Button buttonSkillTest = null;

    public void OnVisible() {
        buttonMood.gameObject.SetActive(SaveController.Instance.GameData.IsMoodEnabled);
        buttonTraining.gameObject.SetActive(SaveController.Instance.GameData.IsTrainingEnabled);
        buttonControlTest.gameObject.SetActive(SaveController.Instance.GameData.IsControlTestEnabled);
        buttonSkillTest.gameObject.SetActive(SaveController.Instance.GameData.IsSkillTestEnabled);
    }

    public void OnHidden() {
        buttonMood.gameObject.SetActive(false);
        buttonTraining.gameObject.SetActive(false);
        buttonControlTest.gameObject.SetActive(false);
        buttonSkillTest.gameObject.SetActive(false);
    }
}
