using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalancingTurnfield : MonoBehaviour {

    private BalancingDisplay balancingDisplay = null;
    private TextMeshProUGUI textMesh = null;

    private void Awake() {
        balancingDisplay = GetComponentInParent<BalancingDisplay>();
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textMesh.text = balancingDisplay.GetBoterkroon(BalanceCase.BestCase).TurnsLeft.ToString();
    }
}
