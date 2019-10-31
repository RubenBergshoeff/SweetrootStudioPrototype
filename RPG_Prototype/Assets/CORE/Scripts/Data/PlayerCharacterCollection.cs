using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerCharacterCollection {
    public ActiveCharacterData ActiveCharacter {
        get {
            return Characters[selectedCharacter];
        }
    }

    public List<ActiveCharacterData> Characters = new List<ActiveCharacterData>();
    private int selectedCharacter = 0;
}
