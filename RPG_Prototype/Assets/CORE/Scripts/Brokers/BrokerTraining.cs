using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrokerTraining : BrokerBase<ActiveTraining, TrainingData> {
    protected override List<ActiveTraining> Collection {
        get {
            return SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.GetAllActiveTrainingData();
        }
    }
}
