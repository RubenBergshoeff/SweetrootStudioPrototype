using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public PlayerCharacterData PlayerData = new PlayerCharacterData();
    //public UnlockableSkillCollection Skills = new UnlockableSkillCollection();
    public UnlockableTrainingCollection TrainingCollection = new UnlockableTrainingCollection();
    public UnlockableMoodCollection MoodCollection = new UnlockableMoodCollection();
}

// Training Unlock
[System.Serializable]
public class UnlockableTrainingCollection : UnlockableCollection<LockedTraining, TrainingData> { }

[System.Serializable]
public class LockedTraining : LockObject<TrainingData> { }

// Mood Unlock
[System.Serializable]
public class UnlockableMoodCollection : UnlockableCollection<LockedMood, MoodData> { }

[System.Serializable]
public class LockedMood : LockObject<MoodData> { }