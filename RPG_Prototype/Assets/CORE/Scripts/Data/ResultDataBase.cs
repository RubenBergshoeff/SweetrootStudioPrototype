using UnityEngine;
using System.Collections;

public class ResultDataBase : ScriptableObject {
    public string Name;
    public Sprite Visual;
}

[System.Serializable]
public abstract class ActiveResultData {
    public ResultDataBase Data;
}

//[System.Serializable]
//public class ActiveResultData<T> where T : ResultDataBase {
//    public T Data;
//}