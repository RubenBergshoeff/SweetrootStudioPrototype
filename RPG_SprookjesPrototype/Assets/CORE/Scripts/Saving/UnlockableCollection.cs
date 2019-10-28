using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnlockableSkillCollection : UnlockableCollection<LockedSkill, Skill> {
}

[System.Serializable]
public class LockedSkill : LockObject<Skill> {
}

[System.Serializable]
public class UnlockableCollection<T, U> where T : LockObject<U> where U : MonoBehaviour {
    public List<T> Objects = new List<T>();

    public bool GetLockState(U item) {
        foreach (var obj in Objects) {
            if (obj.Object == item) {
                return obj.IsUnlocked;
            }
        }
        throw new System.ArgumentException("No value found for " + item);
    }

    public void Lock(U item) {
        foreach (var obj in Objects) {
            if (obj.Object == item) {
                obj.IsUnlocked = false;
                return;
            }
        }
        throw new System.ArgumentException("No value found for " + item);
    }

    public void Unlock(U item) {
        foreach (var obj in Objects) {
            if (obj.Object == item) {
                obj.IsUnlocked = true;
                return;
            }
        }
        throw new System.ArgumentException("No value found for " + item);
    }
}

[System.Serializable]
public class LockObject<T> where T : MonoBehaviour {
    public T Object;
    public bool IsUnlocked;
}
