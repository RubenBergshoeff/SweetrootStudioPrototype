using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerCharacterData {
    public int CurrentMoodLevel;
    public int TotalPoints {
        get {
            return (StatPower.Level + StatMagic.Level);
        }
    }
    public PlayerStat StatPower;
    public PlayerStat StatMagic;
    public int ProvenLevel;
}
