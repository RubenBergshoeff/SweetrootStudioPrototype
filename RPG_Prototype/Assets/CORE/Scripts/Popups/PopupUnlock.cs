using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopupUnlock : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI unlockMessageField = null;

    internal void Setup(UnlockResult newUnlock) {
        unlockMessageField.text = newUnlock.ResultToUnlock.Name;
    }
}
