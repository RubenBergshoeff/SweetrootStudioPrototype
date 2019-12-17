using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewCharacterController : UIDisplayController {

    public bool IsDone { get; private set; }
    [SerializeField] private Image visualContainer = null;
    [SerializeField] private TextMeshProUGUI textmeshVisual = null;
    [SerializeField] private Button backButton = null;

    [SerializeField] private VisualSkillTest[] storyFrames = new VisualSkillTest[0];
    private int itterator = 0;
    private int lineItterator = 0;
    private bool showStory = false;
    private bool skipWaitTime = false;
    private float lastVisualChangeTime = 0;

    protected override void OnShowing() {
        itterator = 0;
        lineItterator = 0;
        showStory = false;
        IsDone = false;
        UpdateVisual();
        backButton.gameObject.SetActive(false);
    }

    protected override void OnVisible() {
        showStory = true;
        //    StartCoroutine(BoterkroonStoryAnimation());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {
        showStory = false;
    }

    private void Update() {
        if (showStory == false || IsDone) { return; }

        if (Input.GetMouseButtonDown(0)) {
            skipWaitTime = true;
        }

        if (lastVisualChangeTime + 5 > Time.time && skipWaitTime == false) {
            return;
        }

        skipWaitTime = false;
        if (itterator < storyFrames.Length) {
            UpdateVisual();
        }
        else {
            SaveController.Instance.GameData.BoterKroon.IsNew = false;
            IsDone = true;
            backButton.gameObject.SetActive(true);
        }
    }

    private void UpdateVisual() {
        lastVisualChangeTime = Time.time;
        SetVisual(storyFrames[itterator]);
        itterator++;
    }

    private IEnumerator BoterkroonStoryAnimation() {
        yield return new WaitForSeconds(5);

        while (itterator < storyFrames.Length) {
            SetVisual(storyFrames[itterator]);
            itterator++;
            yield return new WaitForSeconds(5);
        }

        SaveController.Instance.GameData.BoterKroon.IsNew = false;
        IsDone = true;
        backButton.gameObject.SetActive(true);
    }

    private void SetVisual(VisualSkillTest visual) {
        visualContainer.sprite = visual.Image;
        textmeshVisual.text = visual.Text;
    }
}
