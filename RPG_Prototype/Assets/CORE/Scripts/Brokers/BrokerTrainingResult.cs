using UnityEngine;
using Doozy.Engine;

public class BrokerTrainingResult : BrokerBaseResult {

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private TrainingGameResult trainingGameResult = null;
    [SerializeField] private Transform gameControllerContainer = null;
    [SerializeField] private string uiEventStringGameResult = "";

    private TrainingGameController activeGameController = null;
    private ActiveTraining activeTraining;
    private int gainedXP = 0;

    public override void SetResult(ActiveResultData result) {
        base.SetResult(result);
        activeTraining = result as ActiveTraining;
        activeGameController = CreateActiveGameController(activeTraining);
        activeGameController.Setup(activeTraining);
        activeGameController.OnXPGain += OnXPGain;
        activeGameController.OnGameFinished += OnGameFinished;
        gainedXP = 0;
        Debug.Log(activeTraining.Data.Name);
        Debug.Log(activeTraining.Training.TargetSkill.Name);
    }

    protected override void OnVisible() {
        activeGameController.StartTraining();
    }

    protected override void OnInvisible() {
        Destroy(activeGameController.gameObject);
        activeGameController = null;
    }

    private void OnGameFinished() {
        activeGameController.Cleanup();
        playerCharacterController.AddTrainingResult(activeTraining.Training, gainedXP);
        trainingGameResult.SetResult(activeTraining, gainedXP);
        GameEventMessage.SendEvent(uiEventStringGameResult);
    }

    private void OnXPGain(int xpAmount) {
        gainedXP += xpAmount;
    }

    private TrainingGameController CreateActiveGameController(ActiveTraining activeTraining) {
        GameObject trainingControllerObject = Instantiate(activeTraining.Training.TrainingGameController.gameObject, gameControllerContainer);
        return trainingControllerObject.GetComponent<TrainingGameController>();
    }
}
