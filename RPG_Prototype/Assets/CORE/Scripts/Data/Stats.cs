using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IStat {
    int Level { get; }
}

public enum StatType {
    Power, Magic
}

[System.Serializable]
public class PlayerStat : IStat {

    public static Dictionary<int, int> MinLevelXPs = new Dictionary<int, int> {
        {0, 0},
        {1, 100},
        {2, 500},
        {3, 2000},
        {4, 5000},
        {5, 100000}
    };

    public int Level {
        get {
            if (XP < 100) { return 0; }
            if (XP < 500) { return 1; }
            if (XP < 2000) { return 2; }
            if (XP < 5000) { return 3; }
            return 4;
        }
    }
    public int XP;
}

[System.Serializable]
public class EnemyStat : IStat {
    public int Level {
        get {
            return level;
        }
    }
    [SerializeField] private int level = 1;
}