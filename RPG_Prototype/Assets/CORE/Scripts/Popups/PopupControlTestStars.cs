using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class PopupControlTestStars : MonoBehaviour {

    [SerializeField] private Image starOne = null;
    [SerializeField] private Image starTwo = null;
    [SerializeField] private Image starThree = null;

    private int oldStarAmount = 0;
    private int newStarAmount = 0;

    public void Setup(int oldStarAmount, int newStarAmount) {
        SetupStartStars(oldStarAmount);
        this.oldStarAmount = oldStarAmount;
        this.newStarAmount = newStarAmount;
    }

    public void OnVisible() {
        StartCoroutine(AnimateNewStars());
    }

    private IEnumerator AnimateNewStars() {
        yield return new WaitForSeconds(0.25f);
        while (oldStarAmount < newStarAmount) {
            oldStarAmount++;
            Image targetStar = null;
            if (oldStarAmount == 1) {
                targetStar = starOne;
            } else if (oldStarAmount == 2) {
                targetStar = starTwo;
            } else {
                targetStar = starThree;
            }
            AnimateStar(targetStar, 0.45f);
            yield return new WaitForSeconds(.5f);
        }
    }

    private void AnimateStar(Image targetStar, float duration) {
        targetStar.gameObject.SetActive(true);
        targetStar.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        targetStar.GetComponent<RectTransform>().DOScale(Vector3.one, duration).SetEase(Ease.OutElastic);
    }

    private void SetupStartStars(int starAmount) {
        starOne.gameObject.SetActive(false);
        starTwo.gameObject.SetActive(false);
        starThree.gameObject.SetActive(false);
        if (starAmount > 0) {
            starOne.gameObject.SetActive(true);
        }
        if (starAmount > 1) {
            starTwo.gameObject.SetActive(true);
        }
    }
}
