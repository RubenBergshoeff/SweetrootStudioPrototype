using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrokerFood : BrokerBase<ActiveMoodData, MoodData> {
    protected override List<ActiveMoodData> Collection {
        get {
            return SaveController.Instance.GameData.MoodCollection.ActiveMoodDataItems;
        }
    }

    protected override void OnVisible() {
    }

    protected override void OnInvisible() {
    }
}