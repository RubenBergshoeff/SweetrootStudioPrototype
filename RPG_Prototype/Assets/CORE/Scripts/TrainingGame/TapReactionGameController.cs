using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TapReactionGameController : TrainingGameController {

    [SerializeField] private TapReactionItem tapReactionItemTemplate = null;
    [SerializeField] private float allowedReactionTime = 4f;

    private List<TapReactionItem> reactionItems = new List<TapReactionItem>();

    public override void Setup(ActiveTraining training) {
        tapReactionItemTemplate.gameObject.SetActive(false);
    }

    public override void StartTraining() {
        throw new System.NotImplementedException();
    }

    public override void Cleanup() {
        throw new System.NotImplementedException();
    }
}
