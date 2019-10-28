using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    public PlayerData PlayerData = new PlayerData();
    public UnlockableSkillCollection Skills = new UnlockableSkillCollection();
}