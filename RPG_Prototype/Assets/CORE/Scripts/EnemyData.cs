using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class EnemyData : ResultDataBase {
    public int Level {
        get {
            return (StatPower.Level + StatMagic.Level) / 2;
        }
    }

    public EnemyStat StatPower;
    public EnemyStat StatMagic;
}
