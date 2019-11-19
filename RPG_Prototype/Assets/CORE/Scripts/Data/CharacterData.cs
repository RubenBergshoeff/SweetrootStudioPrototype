using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class CharacterData : BaseData {
    public int StartMoodLevel = 0;
    public List<TrainingData> AvailableTrainingData = new List<TrainingData>();
    public List<SkillLevel> AvailableSkillLevels = new List<SkillLevel>();
}
