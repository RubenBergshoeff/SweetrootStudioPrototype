using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class EnemyData : BaseData {
    public int Level {
        get {
            return TotalPoints / 2;
        }
    }
    public int TotalPoints {
        get {
            return StatPower.Level + StatMagic.Level;
        }
    }

    public EnemyStat StatPower;
    public EnemyStat StatMagic;
    public Sprite ArenaImage;
    public TrainingData NewTrainingData;
}

[System.Serializable]
public class ActiveEnemyData : ActiveBaseData {
    public EnemyData EnemyData {
        get {
            return Data as EnemyData;
        }
    }
}