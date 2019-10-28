using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.UI;
using System;


public abstract class BrokerBase : MonoBehaviour {
    [SerializeField] private BrokerResultBase resultController = null;
    [SerializeField] private string uiEventString = "";

    public void PickResult(ResultDataBase result) {
        resultController.SetResult(result);
        GameEventMessage.SendEvent(uiEventString);
    }
}

public abstract class BrokerBase<T, U, V> : BrokerBase where T : UnlockableCollection<U, V> where U : LockObject<V> where V : ResultDataBase {
    [SerializeField] private BrokerButton templateButton = null;
    [SerializeField] private Transform buttonContainer = null;

    protected abstract UnlockableCollection<U, V> UnlockableCollection {
        get;
    }

    private void OnEnable() {
        templateButton.gameObject.SetActive(true);
        for (int i = buttonContainer.childCount - 1; i >= 0; i--) {
            if (buttonContainer.GetChild(i).gameObject != templateButton.gameObject) {
                Destroy(buttonContainer.GetChild(i).gameObject);
                buttonContainer.GetChild(i).SetParent(null);
            }
        }

        foreach (var item in UnlockableCollection.Objects) {
            if (item.IsUnlocked) {
                AddItem(item.Object);
            }
        }

        templateButton.gameObject.SetActive(false);
        GetComponentInChildren<ScrollViewContentScaler>().UpdateView();
    }

    private void AddItem(V item) {
        BrokerButton newButton = Instantiate(templateButton.gameObject, buttonContainer).GetComponent<BrokerButton>();
        newButton.SetupButton(this, item);
    }
}