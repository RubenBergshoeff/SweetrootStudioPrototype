using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.UI;
using System;


public abstract class BrokerBase : UIDisplayController {
    [SerializeField] private BrokerBaseResult resultController = null;
    [SerializeField] private string uiEventString = "";

    public void PickResult(ActiveBaseData result) {
        resultController.SetResult(result);
        GameEventMessage.SendEvent(uiEventString);
    }
}

public abstract class BrokerBase<T, U> : BrokerBase where T : ActiveBaseData {
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

    private void AddItem(ActiveBaseData item) {
        BrokerButton newButton = Instantiate(templateButton.gameObject, buttonContainer).GetComponent<BrokerButton>();
        newButton.SetupButton(this, item);
    }
}
