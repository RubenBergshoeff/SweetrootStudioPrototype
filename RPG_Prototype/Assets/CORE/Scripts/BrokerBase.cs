using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using UnityEngine.UI;

public class BrokerBase : MonoBehaviour {
    [SerializeField] private BrokerResultBase resultController = null;
    [SerializeField] private string uiEventString = "";

    public void PickResult(ResultDataBase result) {
        resultController.SetResult(result);
        GameEventMessage.SendEvent(uiEventString);
    }
}
