using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public bool IsTrainingEnabled = true;
    public bool IsMoodEnabled = true;
    public bool IsControlTestEnabled = true;
    public bool IsSkillTestEnabled = true;
    public ActiveBoterkroonData BoterKroon;
    //public PlayerCharacterCollection CharacterCollection = new PlayerCharacterCollection();
    public PlayerMoodCollection MoodCollection = new PlayerMoodCollection();
    //public UnlockableSkillCollection Skills = new UnlockableSkillCollection();
    //public UnlockableTrainingCollection TrainingCollection = new UnlockableTrainingCollection();
    //public UnlockableMoodCollection MoodCollection = new UnlockableMoodCollection();
    //public UnlockableEnemyCollection EnemyCollection = new UnlockableEnemyCollection();
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

// Arena Unlock
[System.Serializable]
public class UnlockableEnemyCollection : UnlockableCollection<LockedEnemy, EnemyData> { }

[System.Serializable]
public class LockedEnemy : LockObject<EnemyData> { }