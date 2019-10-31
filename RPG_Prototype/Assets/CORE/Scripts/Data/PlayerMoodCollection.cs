using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerMoodCollection {
    public List<ActiveMoodData> ActiveMoodDataItems = new List<ActiveMoodData>();

    public void AddMoodData(MoodData data, int amount) {
        ActiveMoodData activeMoodData = GetActiveMoodData(data);
        if (activeMoodData == null) {
            activeMoodData = new ActiveMoodData(data);
            ActiveMoodDataItems.Add(activeMoodData);
        }
        activeMoodData.Amount += amount;
    }

    public ActiveMoodData GetActiveMoodData(MoodData data) {
        foreach (var activeMoodData in ActiveMoodDataItems) {
            if (activeMoodData.MoodData == data) {
                return activeMoodData;
            }
        }
        return null;
    }
}