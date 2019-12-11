using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewCharacterController : UIDisplayController {

    [SerializeField] private Image visualContainer = null;
    [SerializeField] private TextMeshProUGUI textmeshVisual = null;
    [SerializeField] private Button backButton = null;

    [SerializeField] private VisualSkillTest[] storyFrames = new VisualSkillTest[0];
    private int itterator = 0;

    protected override void OnShowing() {
        itterator = 0;
        SetVisual(storyFrames[itterator]);
        itterator++;
        backButton.gameObject.SetActive(false);
    }

    protected override void OnVisible() {
        StartCoroutine(BoterkroonStoryAnimation());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }

    private IEnumerator BoterkroonStoryAnimation() {
        yield return new WaitForSeconds(5);

        while (itterator < storyFrames.Length) {
            SetVisual(storyFrames[itterator]);
            itterator++;
            yield return new WaitForSeconds(5);
        }

        SaveController.Instance.GameData.BoterKroon.IsNew = false;
        backButton.gameObject.SetActive(true);
    }

    private void SetVisual(VisualSkillTest visual) {
        visualContainer.sprite = visual.Image;
        textmeshVisual.text = visual.Text;
    }
}
