using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class SkillLevel : ScriptableObject {
    public Skill Skill = null;
    public int Level = 1;
    public int XPCap = 1000;
    public int SkillCategoryScore = 5;
    public Sprite Visual = null;
}