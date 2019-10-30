using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class MoodData : ResultDataBase {
    public int MoodImproveAmount = 1;
}

[System.Serializable]
public class ActiveMoodData : ActiveResultData {
    public MoodData MoodData {
        get {
            return Data as MoodData;
        }
    }
    public int Amount = 0;
}