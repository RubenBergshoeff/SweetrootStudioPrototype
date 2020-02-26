using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class BoterkroonValues : ScriptableObject {

    public static BoterkroonValues Values {
        get {
            if (valuesReference == null) {
                valuesReference = Resources.Load("BoterkroonValues") as BoterkroonValues;
            }
            return valuesReference;
        }
    }
    private static BoterkroonValues valuesReference = null;


    [Header("XP")]
    public int NormalTrainingMinXPGain = 50;
    public int NormalTrainingMaxXPGain = 100;
    public int FastTrainingMinXPGain = 10;
    public int FastTrainingMaxXPGain = 150;
    [Range(0, 1)]
    public float StartpointFastTrainingLerp = 0.3f;
    public int MaxSkillXP = 1000;

    [Header("Turns and Cost")]
    public int TurnAmountStart = 20;
    public int CostNormalTraining = 2;
    public int CostFastTraining = 1;
    public int CostControlTest = 1;
    public int CostSkillTest = 4;

    [Header("Level One Requirements")]
    [Range(0, 1)]
    public float Lvl1MaxBakeControl = 0.5f;
    [Range(0, 1)]
    public float Lvl1MaxSwordControl = 0;
    [Range(0, 1)]
    public float Lvl1MaxResearchControl = 0;


    [Header("Level Two Requirements")]
    [Range(0, 1)]
    public float Lvl2MinBakeControl = 0.4f;
    [Range(0, 1)]
    public float Lvl2MinSwordControl = 0;
    [Range(0, 1)]
    public float Lvl2MinResearchControl = 0;
    [Range(0, 1)]
    public float Lvl2MaxBakeControl = 0.6f;
    [Range(0, 1)]
    public float Lvl2MaxSwordControl = 0.3f;
    [Range(0, 1)]
    public float Lvl2MaxResearchControl = 0;


    [Header("Level Three Requirements")]
    [Range(0, 1)]
    public float Lvl3MinBakeControl = 0.5f;
    [Range(0, 1)]
    public float Lvl3MinSwordControl = 0.3f;
    [Range(0, 1)]
    public float Lvl3MinResearchControl = 0;
    [Range(0, 1)]
    public float Lvl3MaxBakeControl = 0.8f;
    [Range(0, 1)]
    public float Lvl3MaxSwordControl = 0.7f;
    [Range(0, 1)]
    public float Lvl3MaxResearchControl = 0.5f;

    [Header("Unlock Sword Requirements")]
    [Range(0, 1)]
    public float UnlockSwordMinBakeControl = 0.4f;
    [Range(0, 1)]
    public float UnlockSwordSwordControl = 0;
    [Range(0, 1)]
    public float UnlockSwordResearchControl = 0;

    [Header("Unlock Research Requirements")]
    [Range(0, 1)]
    public float UnlockResearchMinBakeControl = 0.5f;
    [Range(0, 1)]
    public float UnlockResearchSwordControl = 0.2f;
    [Range(0, 1)]
    public float UnlockResearchResearchControl = 0;
}
