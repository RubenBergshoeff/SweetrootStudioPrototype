using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class SkillCategory : ScriptableObject {
    public string Name = "SkillCat";
    public Sprite Visual = null;
    public Color color = Color.grey;
}
