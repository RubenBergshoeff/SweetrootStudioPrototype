using UnityEngine;
using System.Collections;

public class BaseData : ScriptableObject {
    public string Name;
    public Sprite Visual;
}

[System.Serializable]
public abstract class ActiveBaseData {
    public ActiveBaseData() {

    }
    public ActiveBaseData(BaseData data) {
        this.Data = data;
    }

    public BaseData Data;
}

//[System.Serializable]
//public class ActiveResultData<T> where T : ResultDataBase {
//    public T Data;
//}