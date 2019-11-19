using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.UI;
using System;


public abstract class BrokerBase : UIDisplayController {
    [SerializeField] private BrokerBaseResult resultController = null;
    [SerializeField] private string uiEventString = "";

    public void PickResult(ActiveResultData result) {
        resultController.SetResult(result);
        GameEventMessage.SendEvent(uiEventString);
    }
}

public abstract class BrokerBase<T, U> : BrokerBase where T : ActiveResultData {
    [SerializeField] private BrokerButton templateButton = null;
    [SerializeField] private Transform buttonContainer = null;

    protected abstract List<T> Collection {
        get;
    }

    protected override void OnShowing() {
        templateButton.gameObject.SetActive(true);
        for (int i = buttonContainer.childCount - 1; i >= 0; i--) {
            if (buttonContainer.GetChild(i).gameObject != templateButton.gameObject) {
                Destroy(buttonContainer.GetChild(i).gameObject);
                buttonContainer.GetChild(i).SetParent(null);
            }
        }

        foreach (var item in Collection) {
            AddItem(item);
        }

        templateButton.gameObject.SetActive(false);
        GetComponentInChildren<ScrollViewContentScaler>().UpdateView();
    }

    protected override void OnVisible() {

    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {

    }

    private void AddItem(ActiveResultData item) {
        BrokerButton newButton = Instantiate(templateButton.gameObject, buttonContainer).GetComponent<BrokerButton>();
        newButton.SetupButton(this, item);
    }
}

public abstract class BrokerBaseFlipOver<T> : BrokerBase where T : ActiveResultData {

    protected abstract List<T> Collection {
        get;
    }

    [SerializeField] private OptionPointFlipOver[] options = new OptionPointFlipOver[0];
    private List<OptionPointFlipOver> activeOptions = new List<OptionPointFlipOver>();

    protected override void OnShowing() {
        HideOptions();
        foreach (var skillLevel in Collection) {
            OptionPointFlipOver option = GetOption(skillLevel.Data);
            SetupOption(option, skillLevel);
        }
    }

    protected override void OnVisible() {
        foreach (var option in activeOptions) {
            option.FlipUp();
        }
    }

    protected override void OnHiding() {
    }

    protected override void OnInvisible() {
        foreach (var option in activeOptions) {
            option.OnPointClicked -= PickResult;
        }
        activeOptions = new List<OptionPointFlipOver>();
    }

    private void HideOptions() {
        foreach (var option in options) {
            option.gameObject.SetActive(false);
        }
    }

    private void SetupOption(OptionPointFlipOver option, ActiveResultData activeData) {
        if (activeOptions.Contains(option) == false) {
            activeOptions.Add(option);
            option.gameObject.SetActive(true);
            option.Initialize(activeData);
            option.OnPointClicked += PickResult;
            option.ToStartPosition();
        } else {
            Debug.LogWarning("System wants to add an option that has already been added. Everything fine?");
        }
    }

    private OptionPointFlipOver GetOption(ResultDataBase desiredData) {
        foreach (var option in options) {
            if (option.Data == desiredData) {
                return option;
            }
        }
        throw new ArgumentException("No option found for " + desiredData);
    }
}