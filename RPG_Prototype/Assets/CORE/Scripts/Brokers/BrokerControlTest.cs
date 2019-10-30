using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BrokerControlTest : BrokerBase<ActiveSkillLevel, SkillLevel> {
    protected override List<ActiveSkillLevel> Collection {
        get {
            return SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.GetAllActiveSkillLevels();
        }
    }
}