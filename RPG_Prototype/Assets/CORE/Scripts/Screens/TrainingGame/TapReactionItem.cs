using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public enum TapReactionState {
    Early,
    Correct,
    Late
}

public class TapReactionItem : MonoBehaviour {

    public delegate void TapReactionItemDelegate(TapReactionItem item, TapReactionState state);
    public TapReactionItemDelegate OnTapped;

    public bool IsActive { get { return isActive; } }

    [SerializeField] private Button button = null;
    [SerializeField] private Image startImage = null;
    [SerializeField] private Image correctImage = null;
    [SerializeField] private Image timedOutImage = null;

    private float startTime;
    private float allowedReactionTime;
    private bool isActive;
    private bool isCorrect;
    private bool isTimedOut;
    private bool isTapped;

    public void Setup(float minStartTime, float maxStartTime, float allowedReactionTime) {
        isActive = true;
        this.startTime = Random.Range(minStartTime, maxStartTime);
        this.allowedReactionTime = allowedReactionTime;
        isTimedOut = false;
        isCorrect = false;
        isTapped = false;
        SetStartVisual();
        StartCoroutine(SetCorrect(startTime));
    }

    public void Disable() {
        isActive = false;
        startImage.enabled = false;
        correctImage.enabled = false;
        timedOutImage.enabled = false;
    }

    private IEnumerator SetCorrect(float startTime) {
        yield return new WaitForSeconds(startTime);
        SetCorrectVisual();
        isCorrect = true;
        StartCoroutine(SetTimedOut(allowedReactionTime));
    }

    private IEnumerator SetTimedOut(float allowedReactionTime) {
        yield return new WaitForSeconds(allowedReactionTime);
        SetTimedOutVisual();
        isCorrect = false;
        isTimedOut = true;
    }

    private void OnButtonClicked() {
        if (IsActive == false || isTapped) { return; }

        isTapped = true;

        if (isTimedOut) {
            OnTapped?.Invoke(this, TapReactionState.Late);
        }
        else if (isCorrect) {
            OnTapped?.Invoke(this, TapReactionState.Correct);
        }
        else {
            OnTapped?.Invoke(this, TapReactionState.Early);
        }

        StopAllCoroutines();
        ShowTappedAnimation();
    }

    private void SetStartVisual() {
        gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
        startImage.enabled = true;
        correctImage.enabled = false;
        timedOutImage.enabled = false;
    }

    private void SetCorrectVisual() {
        startImage.enabled = false;
        correctImage.enabled = true;
        timedOutImage.enabled = false;
    }

    private void SetTimedOutVisual() {
        startImage.enabled = false;
        correctImage.enabled = false;
        timedOutImage.enabled = true;
    }

    private void OnEnable() {
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable() {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    private void ShowTappedAnimation() {
        gameObject.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InElastic).OnComplete(Disable);
    }
}
