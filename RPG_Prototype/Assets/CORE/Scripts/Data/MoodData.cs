using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class MoodData : BaseData {
    public int MoodImproveAmount = 1;
}

[System.Serializable]
public class ActiveMoodData : ActiveBaseData {
    public MoodData MoodData {
        get {
            return Data as MoodData;
        }
    }
    public ActiveMoodData(MoodData data) : base(data) {

    }
    public ActiveMoodData(MoodData data, int amount) : base(data) {
        this.Amount = amount;
    }

    public int Amount = 0;
}