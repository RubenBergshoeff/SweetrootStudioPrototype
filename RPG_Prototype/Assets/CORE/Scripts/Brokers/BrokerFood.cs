using UnityEngine;
using System.Collections;

public class BrokerFood : BrokerBase<UnlockableMoodCollection, LockedMood, MoodData> {
    protected override UnlockableCollection<LockedMood, MoodData> UnlockableCollection {
        get {
            return SaveController.Instance.GameData.MoodCollection;
        }
    }
}