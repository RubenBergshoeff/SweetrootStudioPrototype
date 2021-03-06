﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class SkillLevel : BaseData {
    public Skill Skill = null;
    public int Level = 1;
    public int XPCap = 1000;
    public int SkillCategoryScore = 5;
}

[System.Serializable]
public class ActiveSkillLevel : ActiveBaseData {
    public SkillLevel SkillLevel {
        get {
            return Data as SkillLevel;
        }
    }

    public ActiveSkillLevel(SkillLevel data) : base(data) {

    }

    public bool IsCompleted = false;
    public float LastCompletionLevel = 0;
}