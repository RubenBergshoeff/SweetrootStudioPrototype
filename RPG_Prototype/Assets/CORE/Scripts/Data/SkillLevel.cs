using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class SkillLevel : ResultDataBase {
    public Skill Skill = null;
    public int Level = 1;
    public int XPCap = 1000;
    public int SkillCategoryScore = 5;
}

[System.Serializable]
public class ActiveSkillLevel : ActiveResultData {
    public SkillLevel SkillLevel {
        get {
            return Data as SkillLevel;
        }
    }
    public bool IsCompleted = false;
}