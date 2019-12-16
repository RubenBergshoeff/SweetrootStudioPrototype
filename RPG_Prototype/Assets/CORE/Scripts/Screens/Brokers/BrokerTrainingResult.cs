using UnityEngine;
using Doozy.Engine;
using System.Collections;
using TMPro;
using System;

public class BrokerTrainingResult : UIDisplayController {
    [SerializeField] private TextMeshProUGUI textmeshSkillName = null;
    [SerializeField] private string uiEventStringDone = "";

    private BoterkroonSkills currentskill;
    private TrainingType currentType;

    //[SerializeField] private PlayerCharacterController playerCharacterController = null;
    //[SerializeField] private TMPro.TextMeshProUGUI textMeshTrainingName = null;
    //[SerializeField] private UnityEngine.UI.Image imageTraining = null;
    //[SerializeField] private TrainingGameResult trainingGameResult = null;
    //[SerializeField] private Transform gameControllerContainer = null;
    //[SerializeField] private string uiEventStringGameResult = "";

    //private TrainingGameController activeGameController = null;
    //private ActiveTraining activeTraining;
    //private int gainedXP = 0;

    public void SetResult(BoterkroonSkills result, TrainingType trainingType) {
        this.currentskill = result;
        this.currentType = trainingType;
        //activeTraining = result as ActiveTraining;
        //textMeshTrainingName.text = activeTraining.Training.Name;
        //imageTraining.sprite = activeTraining.Training.Visual;
        //activeGameController = CreateActiveGameController(activeTraining);
        //activeGameController.Setup(activeTraining);
        //activeGameController.OnXPGain += OnXPGain;
        //activeGameController.OnGameFinished += OnGameFinished;
        //gainedXP = 0;
        //Debug.Log(activeTraining.Data.Name);
        //Debug.Log(activeTraining.Training.TargetSkill.Name);
    }

    protected override void OnShowing() {
        textmeshSkillName.text = GetTrainingText(currentskill);
        CreateTrainingResult();
    }

    private string GetTrainingText(BoterkroonSkills currentskill) {
        switch (currentskill) {
            case BoterkroonSkills.Baking:
                return "Bakken";
            case BoterkroonSkills.Sword:
                return "Zwaardvechten";
            case BoterkroonSkills.Research:
                return "Onderzoeken";
        }
        throw new NotImplementedException();
    }

    protected override void OnVisible() {
        //activeGameController.StartTraining();
        StartCoroutine(TrainingAnimation());
    }

    protected override void OnHiding() {

    }

    protected override void OnInvisible() {
        //Destroy(activeGameController.gameObject);
        //activeGameController = null;
    }

    private void CreateTrainingResult() {
        int xpGain = 0;
        switch (currentType) {
            case TrainingType.Slow:
                xpGain = UnityEngine.Random.Range(BoterkroonValues.Values.NormalTrainingMinXPGain, BoterkroonValues.Values.NormalTrainingMaxXPGain);
                SaveController.Instance.GameData.BoterKroon.TurnsLeft -= BoterkroonValues.Values.CostNormalTraining;
                break;
            case TrainingType.Fast:
                float skillControl = Mathf.Max(0, GetSkillControl() - BoterkroonValues.Values.StartpointFastTrainingLerp);
                float skillControlLerpPoint = skillControl / (1 - BoterkroonValues.Values.StartpointFastTrainingLerp);
                xpGain = Mathf.FloorToInt(Mathf.Lerp(BoterkroonValues.Values.FastTrainingMinXPGain, BoterkroonValues.Values.FastTrainingMaxXPGain, skillControlLerpPoint));
                SaveController.Instance.GameData.BoterKroon.TurnsLeft -= BoterkroonValues.Values.CostFastTraining;
                break;
        }
        BoterkroonTrainingResult result = new BoterkroonTrainingResult(xpGain);
        SaveController.Instance.GameData.BoterKroon.GetTrainingResultsFor(currentskill).Add(result);
    }

    private float GetSkillControl() {
        int currentXPLevel = 0;
        foreach (var trainingResult in SaveController.Instance.GameData.BoterKroon.GetTrainingResultsFor(currentskill)) {
            currentXPLevel += trainingResult.GainedXP;
        }
        return currentXPLevel / BoterkroonValues.Values.MaxSkillXP;
    }

    private IEnumerator TrainingAnimation() {
        yield return new WaitForSeconds(3);

        GameEventMessage.SendEvent(uiEventStringDone);
    }

    //private void OnGameFinished(TrainingGameResultFeedbackData data) {
    //    activeGameController.Cleanup();
    //    playerCharacterController.AddTrainingResult(activeTraining.Training, gainedXP);
    //    activeGameController.OnXPGain -= OnXPGain;
    //    activeGameController.OnGameFinished -= OnGameFinished;
    //    trainingGameResult.SetResult(activeTraining, gainedXP, data);
    //    GameEventMessage.SendEvent(uiEventStringGameResult);
    //}

    //private void OnXPGain(int xpAmount) {
    //    gainedXP += xpAmount;
    //}

    //private TrainingGameController CreateActiveGameController(ActiveTraining activeTraining) {
    //    GameObject trainingControllerObject = Instantiate(activeTraining.Training.TrainingGameController.gameObject, gameControllerContainer);
    //    return trainingControllerObject.GetComponent<TrainingGameController>();
    //}
}
