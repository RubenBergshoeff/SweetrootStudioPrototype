using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerCharacterData : ScriptableObject {
    public int CurrentMoodLevel;
    public int PotentialLevel {
        get {
            return (StatPower.Level + StatMagic.Level) / 2;
        }
    }
    public PlayerStat StatPower;
    public PlayerStat StatMagic;
    public int ProvenLevel;
}
