using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public enum Instigator {
    User,
    System
}

public class SaveController : MonoBehaviour {

    public static bool IsInitialized;
    public static Action OnInitializedAction;
    public static SaveController Instance {
        get {
            return privateInstance;
        }
    }
    private static SaveController privateInstance;

    public GameData GameData;

    public Action OnLoadAction;
    public Action OnSaveAction;

    private void Awake() {
        if (privateInstance == null) {
            privateInstance = this;
        } else if (privateInstance != this) {
            Destroy(gameObject);
        }

        GameData = new GameData();
        LoadGame(Instigator.System);
        DontDestroyOnLoad(gameObject);

        IsInitialized = true;
        OnInitializedAction?.Invoke();
    }

    public void SaveGame() {
        SaveGame(Instigator.User);
    }

    public void LoadGame() {
        LoadGame(Instigator.User);
    }

    public void ResetGame() {
        if (File.Exists(GetSavePath())) {
            File.Delete(GetSavePath());
        }
        GameData = new GameData();
        OnLoadAction?.Invoke();
    }

    private void SaveGame(Instigator instigator) {
        LogController.LogAction(instigator, "saved game");

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(GetSavePath());
        bf.Serialize(file, GameData);
        file.Close();
        Debug.Log("Game Saved");

        OnSaveAction?.Invoke();
    }

    private void LoadGame(Instigator instigator) {
        LogController.LogAction(instigator, "loaded game");

        if (File.Exists(GetSavePath()) == false) {
            Debug.Log("No Game Saved");
        } else {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(GetSavePath(), FileMode.Open);
            GameData = (GameData)bf.Deserialize(file);
            file.Close();
            Debug.Log("Game Loaded");

            OnLoadAction?.Invoke();
        }
    }

    private void OnApplicationQuit() {
        SaveGame(Instigator.System);
    }

    private string GetSavePath() {
        return Application.persistentDataPath + "/gamesave.save";
    }
}
