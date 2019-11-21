using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BrokerControlTest : BrokerBaseFlipOver<ActiveSkillLevel> {
    protected override List<ActiveSkillLevel> Collection {
        get {
            return SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.GetAllActiveSkillLevels();
        }
    }
}