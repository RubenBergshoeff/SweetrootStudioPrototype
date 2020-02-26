using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BalancingControlTestButton : MonoBehaviour {
    [SerializeField] private BoterkroonSkills targetSkill = BoterkroonSkills.Baking;

    private BalancingDisplay balancingDisplay = null;

    private void OnEnable() {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        balancingDisplay = GetComponentInParent<BalancingDisplay>();
    }

    private void OnButtonClicked() {
        balancingDisplay.ApplyControlTest(targetSkill);
    }

    private void OnDisable() {
        GetComponent<Button>().onClick.RemoveListener(OnButtonClicked);
    }
}
