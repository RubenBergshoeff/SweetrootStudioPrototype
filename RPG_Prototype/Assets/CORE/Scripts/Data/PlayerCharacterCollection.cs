using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerCharacterCollection {
    public PlayerCharacterData ActiveCharacter {
        get {
            return Characters[selectedCharacter];
        }
    }

    public List<PlayerCharacterData> Characters = new List<PlayerCharacterData>();
    private int selectedCharacter = 0;
}
