using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class TapReactionItem : MonoBehaviour {

    public delegate void TapReactionItemDelegate(TapReactionItem item);
    public TapReactionItemDelegate OnTapped;
    public TapReactionItemDelegate OnIsCorrect;
    public TapReactionItemDelegate OnTimedOut;

    public bool IsCorrect { get { return isCorrect; } }
    public bool IsTimedOut { get { return isTimedOut; } }
    public bool IsTapped { get { return isTapped; } }

    [SerializeField] private Button button = null;
    [SerializeField] private Image startImage = null;
    [SerializeField] private Image correctImage = null;
    [SerializeField] private Image timedOutImage = null;

    private float timeAlive;
    private float startTime;
    private float allowedReactionTime;
    private bool isCorrect;
    private bool isTimedOut;
    private bool isTapped;

    public void Setup(float minStartTime, float maxStartTime, float allowedReactionTime) {
        timeAlive = 0;
        this.startTime = Random.Range(minStartTime, maxStartTime);
        this.allowedReactionTime = allowedReactionTime;
        isTimedOut = false;
        isCorrect = false;
        isTapped = false;
        SetStartVisual();
    }

    public void UpdateItem(float deltaTime) {
        if (isTimedOut || isTapped) { return; }

        timeAlive += deltaTime;
        if (timeAlive > startTime && !isCorrect && !isTimedOut) {
            isCorrect = true;
            OnIsCorrect?.Invoke(this);
            SetCorrectVisual();
        }

        if (timeAlive > startTime + allowedReactionTime) {
            isCorrect = false;
            isTimedOut = true;
            OnTimedOut?.Invoke(this);
            SetTimedOutVisual();
        }
    }

    private void SetStartVisual() {
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

    private void OnButtonClicked() {
        if (isTapped) { return; }

        isTapped = true;
        OnTapped?.Invoke(this);

        ShowTappedAnimation();
    }

    private void ShowTappedAnimation() {
        gameObject.GetComponent<RectTransform>().DOScale(0, 0.5f).SetEase(Ease.InElastic);
    }
}
