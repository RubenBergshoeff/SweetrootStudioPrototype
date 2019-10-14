using UnityEngine;
using System.Collections;

public interface IStat {
    int Level { get; }
}

public enum StatType {
    Power, Magic
}

[System.Serializable]
public class PlayerStat : IStat {
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