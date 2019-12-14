﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public enum PointDirection {
    Up,
    Down
}

public class TutorializationController : MonoBehaviour {

    [SerializeField] private GameObject pointPrefabDown = null;
    [SerializeField] private GameObject pointPrefabUp = null;
    [SerializeField] private GameObject textPrefab = null;
    [SerializeField] private Button testButton = null;
    [SerializeField] private Transform container = null;
    [SerializeField] private Image canvasBackground = null;
    [SerializeField] private CanvasGroup canvasGroup = null;
    [SerializeField] private TutorialFragment[] tutorialSequence = new TutorialFragment[0];

    private GameObject pointObject = null;
    private GameObject textObject = null;
    private Transform originalParent = null;
    private bool tutorialActive = false;

    private void Awake() {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    private void Update() {
        if (tutorialActive == false && tutorialSequence.Length > SaveController.Instance.GameData.BoterKroon.TutorialIndex) {
            TutorialFragment currentFragment = tutorialSequence[SaveController.Instance.GameData.BoterKroon.TutorialIndex];
            if (currentFragment.AreRequirementsMet) {
                StartTutorial(currentFragment);
            }
        }
    }

    private void StartTutorial(TutorialFragment currentFragment) {
        if (currentFragment is TutorialFragmentPointer) {
            StartPointerTutorial(currentFragment as TutorialFragmentPointer);
        }
        else if (currentFragment is TutorialFragmentText) {
            StartTextTutorial(currentFragment as TutorialFragmentText);
        }
        tutorialActive = true;
    }

    private void StartTextTutorial(TutorialFragmentText currentFragment) {
        ShowTextTutorial(currentFragment.Text, true, null, currentFragment.DelayShowTime);
    }

    private void ShowTextTutorial(string text, bool showBackground = true, GameObject highlightedTarget = null, float delayShowTime = 0) {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasBackground.enabled = showBackground;
        textObject = Instantiate(textPrefab, canvasGroup.transform);
        textObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
        textObject.GetComponentInChildren<Button>().onClick.AddListener(RemoveTextTutorial);
        if (delayShowTime <= 0) {
            canvasGroup.DOFade(1, 0.3f);
        }
        else {
            StartCoroutine(Delay(delayShowTime, () => canvasGroup.DOFade(1, 0.3f)));
        }
    }

    private void RemoveTextTutorial() {
        textObject.GetComponentInChildren<Button>().onClick.RemoveListener(RemoveTextTutorial);

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, 0.3f).OnComplete(() => {
            Destroy(textObject);
            OnTutorialFinished();
        });
    }

    private void StartPointerTutorial(TutorialFragmentPointer currentFragment) {
        if (currentFragment.LinkedButton != null) {
            PointAtButton(currentFragment.LinkedButton, currentFragment.PointDirection, currentFragment.DelayShowTime);
        }
        else {
            PointAtSceneObject(currentFragment.TargetTransform, currentFragment.LinkedClickableObject, currentFragment.PointDirection, currentFragment.DelayShowTime);
        }
    }

    private void PointAtSceneObject(RectTransform rectTransform, ObjectClickTracker linkedClickableObject, PointDirection pointDirection, float delayShowTime) {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasBackground.enabled = false;
        Vector2 pointPosition = new Vector2(0, (rectTransform.sizeDelta.y / 2 + 75) * (pointDirection == PointDirection.Down ? 1 : -1));
        pointObject = Instantiate((pointDirection == PointDirection.Down ? pointPrefabDown : pointPrefabUp), rectTransform.transform);
        pointObject.GetComponent<RectTransform>().anchoredPosition = pointPosition;
        pointObject.transform.SetParent(canvasGroup.transform);
        pointObject.GetComponent<PointerHelper>().StartAnimation(pointDirection);
        linkedClickableObject.OnObjectClicked += RemovePointer;
        if (delayShowTime <= 0) {
            canvasGroup.DOFade(1, 0.3f);
        }
        else {
            StartCoroutine(Delay(delayShowTime, () => canvasGroup.DOFade(1, 0.3f)));
        }
    }

    public void PointAtButton(Button button, PointDirection pointDirection = PointDirection.Down, float delayShowTime = 0) {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasBackground.enabled = true;
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector2 pointPosition = new Vector2(0, (rectTransform.sizeDelta.y / 2 + 75) * (pointDirection == PointDirection.Down ? 1 : -1));
        originalParent = button.transform.parent;
        button.transform.SetParent(container);
        pointObject = Instantiate((pointDirection == PointDirection.Down ? pointPrefabDown : pointPrefabUp), button.transform);
        pointObject.GetComponent<RectTransform>().anchoredPosition = pointPosition;
        pointObject.transform.SetParent(canvasGroup.transform);
        pointObject.GetComponent<PointerHelper>().StartAnimation(pointDirection);
        button.onClick.AddListener(RemovePointer);
        if (delayShowTime <= 0) {
            canvasGroup.DOFade(1, 0.3f);
        }
        else {
            StartCoroutine(Delay(delayShowTime, () => canvasGroup.DOFade(1, 0.3f)));
        }
    }

    private void RemovePointer() {
        if (pointObject != null) {
            TutorialFragmentPointer currentFragment = tutorialSequence[SaveController.Instance.GameData.BoterKroon.TutorialIndex] as TutorialFragmentPointer;

            if (currentFragment.LinkedButton != null) {
                currentFragment.LinkedButton.onClick.RemoveListener(RemovePointer);
                currentFragment.LinkedButton.transform.SetParent(originalParent);
            }
            else {
                currentFragment.LinkedClickableObject.OnObjectClicked -= RemovePointer;
            }
            Destroy(pointObject);
        }

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, 0.3f).OnComplete(() => { OnTutorialFinished(); });
    }

    private void OnTutorialFinished() {
        StopAllCoroutines();
        SaveController.Instance.GameData.BoterKroon.TutorialIndex++;
        tutorialActive = false;
    }

    private IEnumerator Delay(float time, Action action) {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}