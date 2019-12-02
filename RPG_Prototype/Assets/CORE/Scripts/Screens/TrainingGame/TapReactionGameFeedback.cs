using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class TapReactionGameFeedback : TrainingGameResultFeedback {

    //[SerializeField] private TextMeshProUGUI textMeshRawCookie = null;
    //[SerializeField] private TextMeshProUGUI textMeshCorrectCookie = null;
    //[SerializeField] private TextMeshProUGUI textMeshBurntCookie = null;

    [SerializeField] private GameObject templateCookie = null;
    [SerializeField] private Sprite rawCookie = null;
    [SerializeField] private Sprite correctCookie = null;
    [SerializeField] private Sprite burntCookie = null;
    [SerializeField] private GameObject backButton = null;
    [SerializeField] private TextMeshProUGUI scoreField = null;
    [SerializeField] private RectTransform[] cookieStacks = new RectTransform[0];
    [SerializeField] private Vector2 offsetPerCookie = Vector2.zero;

    private TapReactionFeedbackData feedbackDataTapReaction;
    private Vector2[] offsets;
    private int totalScore;

    public override void Setup(TrainingGameResultFeedbackData feedbackData) {
        templateCookie.gameObject.SetActive(false);
        backButton.SetActive(false);
        feedbackDataTapReaction = feedbackData as TapReactionFeedbackData;
        //textMeshRawCookie.text = feedbackDataTapReaction.CookiesRaw.ToString();
        //textMeshCorrectCookie.text = feedbackDataTapReaction.CookiesCorrect.ToString();
        //textMeshBurntCookie.text = feedbackDataTapReaction.CookiesBurnt.ToString();
        offsets = new Vector2[cookieStacks.Length];
        totalScore = 0;
        for (int i = 0; i < offsets.Length; i++) {
            offsets[i] = Vector2.zero;
        }
    }

    public override void OnVisible() {
        StartCoroutine(CookieAnimation());
    }

    private IEnumerator CookieAnimation() {
        float animTimeCookie = 0.3f;
        float waitTimeBetweenCookies = 0.35f;
        float minWaitTimeBetweenCookies = 0.1f;
        int stackIterator = 0;
        bool stackIteratorDir = true;

        int iterator = feedbackDataTapReaction.CookiesRaw;
        while (iterator > 0) {
            iterator--;
            SpawnCookie(rawCookie, animTimeCookie, stackIterator, feedbackDataTapReaction.GainedXPPerFailedItem);
            UpdateStackIterator(ref stackIterator, ref stackIteratorDir);
            yield return new WaitForSeconds(waitTimeBetweenCookies);
            waitTimeBetweenCookies = Mathf.MoveTowards(waitTimeBetweenCookies, minWaitTimeBetweenCookies, 0.022f);
        }

        iterator = feedbackDataTapReaction.CookiesBurnt;
        while (iterator > 0) {
            iterator--;
            SpawnCookie(burntCookie, animTimeCookie, stackIterator, feedbackDataTapReaction.GainedXPPerFailedItem);
            UpdateStackIterator(ref stackIterator, ref stackIteratorDir);
            yield return new WaitForSeconds(waitTimeBetweenCookies);
            waitTimeBetweenCookies = Mathf.MoveTowards(waitTimeBetweenCookies, minWaitTimeBetweenCookies, 0.022f);
        }

        iterator = feedbackDataTapReaction.CookiesCorrect;
        while (iterator > 0) {
            iterator--;
            SpawnCookie(correctCookie, animTimeCookie, stackIterator, feedbackDataTapReaction.GainedXPPerCorrectItem);
            UpdateStackIterator(ref stackIterator, ref stackIteratorDir);
            yield return new WaitForSeconds(waitTimeBetweenCookies);
            waitTimeBetweenCookies = Mathf.MoveTowards(waitTimeBetweenCookies, minWaitTimeBetweenCookies, 0.022f);
        }

        yield return new WaitForSeconds(animTimeCookie);

        backButton.SetActive(true);
    }

    private static void UpdateStackIterator(ref int stackIterator, ref bool stackIteratorDir) {
        if (stackIteratorDir) {
            stackIterator++;
            if (stackIterator == 2) {
                if (UnityEngine.Random.Range(0, 1f) > 0.35f) {
                    stackIteratorDir = false;
                }
                else {
                    stackIterator = 0;
                }
            }
        }
        else {
            stackIterator--;
            if (stackIterator == 0) {
                stackIteratorDir = true;
            }
        }
    }

    private void SpawnCookie(Sprite sprite, float animTimeCookie, int stackIterator, int gainedXP) {
        templateCookie.gameObject.SetActive(true);

        GameObject newCookie = Instantiate(templateCookie, cookieStacks[stackIterator]);
        newCookie.GetComponent<Image>().sprite = sprite;
        Vector2 targetPos = offsets[stackIterator];
        targetPos += new Vector2(UnityEngine.Random.Range(-15f, 15f), 0);
        Vector2 startPos = targetPos + new Vector2(0, 1200);
        newCookie.GetComponent<RectTransform>().anchoredPosition = startPos;
        newCookie.GetComponent<RectTransform>().DOAnchorPos(targetPos, animTimeCookie).SetEase(Ease.InCirc).OnComplete(() => IncrementScore(gainedXP));
        offsets[stackIterator] += offsetPerCookie;

        templateCookie.gameObject.SetActive(false);
    }

    private void IncrementScore(int gainedXP) {
        totalScore += gainedXP;
        scoreField.text = totalScore.ToString();
    }
}
