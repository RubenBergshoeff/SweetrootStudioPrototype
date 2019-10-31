using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class TrainingData : ResultDataBase {
    public Skill TargetSkill = null;
    public int XPGainTraining = 250;
}

[System.Serializable]
public class ActiveTraining : ActiveResultData {
    public TrainingData Training {
        get {
            return Data as TrainingData;
        }
    }

    public ActiveTraining(TrainingData data) : base(data) {

    }

    public int TimesUsed = 0;
}
