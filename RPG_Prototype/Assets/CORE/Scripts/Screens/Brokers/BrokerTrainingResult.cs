using UnityEngine;
using Doozy.Engine;
using System.Collections;
using TMPro;

public class BrokerTrainingResult : UIDisplayController {
    [SerializeField] private TextMeshProUGUI textmeshSkillName = null;
    [SerializeField] private string uiEventStringDone = "";

    private BoterkroonSkills currentskill;

    //[SerializeField] private PlayerCharacterController playerCharacterController = null;
    //[SerializeField] private TMPro.TextMeshProUGUI textMeshTrainingName = null;
    //[SerializeField] private UnityEngine.UI.Image imageTraining = null;
    //[SerializeField] private TrainingGameResult trainingGameResult = null;
    //[SerializeField] private Transform gameControllerContainer = null;
    //[SerializeField] private string uiEventStringGameResult = "";

    //private TrainingGameController activeGameController = null;
    //private ActiveTraining activeTraining;
    //private int gainedXP = 0;

    public void SetResult(BoterkroonSkills result) {
        this.currentskill = result;
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
        textmeshSkillName.text = currentskill.ToString();
        CreateTrainingResult();
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
        BoterkroonTrainingResult result = new BoterkroonTrainingResult(100);
        SaveController.Instance.GameData.BoterKroon.GetTrainingResultsFor(currentskill).Add(result);
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
