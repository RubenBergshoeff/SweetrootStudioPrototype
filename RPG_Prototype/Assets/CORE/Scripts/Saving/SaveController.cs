using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Instigator {
    User,
    System
}

public enum UnlockReturn {
    NewUnlock,
    FailedUnlock,
    WasUnlocked
}

public class SaveController : MonoBehaviour {

    public static SaveController Instance {
        get {
            return privateInstance;
        }
    }
    public static bool IsInitialized;
    private static SaveController privateInstance;

    public GameData GameData;

    public Action OnLoadAction;
    public Action OnSaveAction;

    [SerializeField] private GameDataSettings preDefinedSettings = null;

    private void Awake() {
        if (privateInstance == null) {
            privateInstance = this;
        }
        else if (privateInstance != this) {
            Destroy(gameObject);
        }

        GameData = new GameData();
        LoadGame(Instigator.System);
        DontDestroyOnLoad(gameObject);

        IsInitialized = true;
    }

    private void Start() {
        SceneManager.LoadScene("Main");
    }

    public void SaveGame() {
        SaveGame(Instigator.User);
    }

    public void LoadGame() {
        LoadGame(Instigator.User);
    }

    [ContextMenu("Resest Game Data")]
    public void ResetGame() {
        if (File.Exists(GetSavePath())) {
            File.Delete(GetSavePath());
        }
        LoadDefaultGame();
    }

    private void SaveGame(Instigator instigator) {
        LogController.LogAction(instigator, "saved game");

        string jsonSaveData = JsonUtility.ToJson(GameData);

        TextWriter tw = new StreamWriter(GetSavePath());
        tw.Write(jsonSaveData);
        tw.Close();

        Debug.Log("Game Saved");

        OnSaveAction?.Invoke();
    }

    private void LoadGame(Instigator instigator) {
        LogController.LogAction(instigator, "loaded game");

        if (File.Exists(GetSavePath()) == false) {
            Debug.Log("No Game Saved");
            LoadDefaultGame();
        }
        else {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(GetSavePath(), FileMode.Open);
            //GameData = (GameData)bf.Deserialize(file);
            //file.Close();
            string jsonSaveData = File.ReadAllText(GetSavePath());
            try {
                GameData = JsonUtility.FromJson(jsonSaveData, typeof(GameData)) as GameData;
                Debug.Log("Game Loaded");
                OnLoadAction?.Invoke();
            }
            catch {
                Debug.LogWarning("Something went wrong casting the json, loading default game");
                LoadDefaultGame();
            }
        }
    }

    private void LoadDefaultGame() {
        GameData = new GameData();
        if (preDefinedSettings != null) {
            GameData = new GameData();
            foreach (var characterData in preDefinedSettings.StartCharacters) {
                ActiveCharacterData activeCharacterData = new ActiveCharacterData(characterData);
                GameData.CharacterCollection.Characters.Add(activeCharacterData);
            }
            foreach (var moodDataSet in preDefinedSettings.StartMoodItems) {
                GameData.MoodCollection.AddMoodData(moodDataSet.MoodData, moodDataSet.Amount);
            }
            GameData.IsMoodEnabled = preDefinedSettings.IsMoodEnabled;
            GameData.IsTrainingEnabled = preDefinedSettings.IsTrainingEnabled;
            GameData.IsControlTestEnabled = preDefinedSettings.IsControlTestEnabled;
            GameData.IsSkillTestEnabled = preDefinedSettings.IsSkillTestEnabled;
        }
        OnLoadAction?.Invoke();
    }

    private void OnApplicationQuit() {
        SaveGame(Instigator.System);
    }

    private string GetSavePath() {
        return Application.persistentDataPath + "/gamesave.save";
    }
}
