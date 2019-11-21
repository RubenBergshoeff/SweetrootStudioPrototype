using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BrokerSkillTest : BrokerBaseFlipOver<ActiveSkillCategory> {
    protected override List<ActiveSkillCategory> Collection {
        get {
            return SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.ActiveSkillCategories;
        }
    }
}
