using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class GameDataSettings : ScriptableObject {
    public bool IsTrainingEnabled = true;
    public bool IsMoodEnabled = true;
    public bool IsControlTestEnabled = true;
    public bool IsSkillTestEnabled = true;
    public List<CharacterData> StartCharacters = new List<CharacterData>();
    public List<MoodDataAmountSet> StartMoodItems = new List<MoodDataAmountSet>();
}

[System.Serializable]
public class MoodDataAmountSet {
    public MoodData MoodData = null;
    public int Amount = 1;
}