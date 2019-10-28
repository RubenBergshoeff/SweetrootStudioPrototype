using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokerTraining : BrokerBase<UnlockableTrainingCollection, LockedTraining, TrainingData> {
    protected override UnlockableCollection<LockedTraining, TrainingData> UnlockableCollection {
        get {
            return SaveController.Instance.GameData.TrainingCollection;
        }
    }
}
