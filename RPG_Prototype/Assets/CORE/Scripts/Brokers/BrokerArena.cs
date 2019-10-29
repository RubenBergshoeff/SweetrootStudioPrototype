using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BrokerArena : BrokerBase<UnlockableEnemyCollection, LockedEnemy, EnemyData> {
    protected override UnlockableCollection<LockedEnemy, EnemyData> UnlockableCollection {
        get {
            return SaveController.Instance.GameData.EnemyCollection;
        }
    }
}