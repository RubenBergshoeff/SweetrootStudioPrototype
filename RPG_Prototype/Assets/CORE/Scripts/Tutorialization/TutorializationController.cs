using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum PointDirection {
    Up,
    Down
}

public class TutorializationController : MonoBehaviour {

    [SerializeField] private GameObject pointPrefab = null;
    [SerializeField] private Button testButton = null;
    [SerializeField] private Transform container = null;
    [SerializeField] private CanvasGroup canvasGroup = null;

    private GameObject pointObject = null;
    private Transform originalParent = null;

    private void Awake() {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    [ContextMenu("Test Pointer")]
    private void TestPointer() {
        PointAtButton(testButton);
    }

    public void PointAtButton(Button button, PointDirection pointDirection = PointDirection.Down) {
        canvasGroup.DOFade(1, 0.3f);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector2 pointPosition = new Vector2(0, (rectTransform.sizeDelta.y / 2 + 50) * (pointDirection == PointDirection.Down ? 1 : -1));
        originalParent = button.transform.parent;
        button.transform.SetParent(container);
        pointObject = Instantiate(pointPrefab, button.transform);
        pointObject.GetComponent<RectTransform>().anchoredPosition = pointPosition;
        button.onClick.AddListener(RemovePointer);
    }

    private void RemovePointer() {
        canvasGroup.DOFade(0, 0.3f);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        if (pointObject != null) {
            pointObject.transform.parent.GetComponent<Button>().onClick.RemoveListener(RemovePointer);
            pointObject.transform.parent.SetParent(originalParent);
            Destroy(pointObject);
        }
    }
}
