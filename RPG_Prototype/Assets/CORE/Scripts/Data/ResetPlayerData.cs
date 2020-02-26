using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerData : MonoBehaviour {

    public void Reset() {
        SaveController.Instance.ResetGame();
    }
}
