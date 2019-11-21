using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTestController : MonoBehaviour {
    public Action<SkillCategoryTestResult> OnTestDone;

    public BaseData Target = null;

    [SerializeField] private PlayerCharacterController playerCharacterController = null;
    [SerializeField] private Transform characterTransform = null;
    [SerializeField] private List<SkillTestWaypoint> waypoints = new List<SkillTestWaypoint>();
    private bool isRunning = false;
    private int currentWaypoint = 0;
    private SkillCategoryTestResult currentResult = null;
    private Vector3 targetPosition = Vector3.zero;

    public void StartSequence() {
        isRunning = true;
        currentWaypoint = 0;
        currentResult = new SkillCategoryTestResult();
        if (waypoints != null && waypoints.Count > 0) {
            StartCoroutine(GoToNextWaypoint());
        }
        else {
            throw new System.ArgumentNullException("No waypoints have been assigned");
        }
    }

    [ContextMenu("Find and sort waypoints")]
    private void FindAndSortWaypoints() {
        var AllWaypoints = transform.GetComponentsInChildren<SkillTestWaypoint>();
        waypoints = new List<SkillTestWaypoint>(AllWaypoints);
        waypoints.Sort((lhs, rhs) => lhs.transform.position.x.CompareTo(rhs.transform.position.x));
    }

    private IEnumerator GoToNextWaypoint() {
        targetPosition = waypoints[currentWaypoint].transform.position;
        yield return new WaitWhile(() => Vector3.Distance(characterTransform.position, targetPosition) > 0.1f);

        bool canContinue = ResolveWaypoint(waypoints[currentWaypoint]);
        if (canContinue) {
            waypoints[currentWaypoint].ShowResolveAnimation();
        }
        yield return new WaitForSeconds(waypoints[currentWaypoint].ResolveTime + 0.5f);
        if (canContinue == false) {
            SendResult();
        }
        else {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count) {
                SendResult();
            }
            else {
                StartCoroutine(GoToNextWaypoint());
            }
        }
    }

    private void Update() {
        if (isRunning == false) { return; }

        if (currentWaypoint >= waypoints.Count) {
            SendResult();
            return;
        }

        characterTransform.position = Vector3.MoveTowards(characterTransform.position, targetPosition, 0.45f * Time.deltaTime);

        //if (Vector3.Distance(characterTransform.position, waypoints[currentWaypoint].transform.position) < 0.1f) {
        //    bool canContinue = ResolveWaypoint(waypoints[currentWaypoint]);
        //    if (canContinue == false) {
        //        SendResult();
        //    }
        //    currentWaypoint++;
        //}
    }

    private bool ResolveWaypoint(SkillTestWaypoint skillTestWaypoint) {
        bool canContinue = false;

        currentResult.Unlocks.AddRange(skillTestWaypoint.unlockResults);

        if (playerCharacterController.IsSkillActive(skillTestWaypoint.TargetSkill) == false) {
            return canContinue;
        }

        int skillScore = SaveController.Instance.GameData.CharacterCollection.ActiveCharacter.GetSkillScore(skillTestWaypoint.TargetSkill);
        if (skillScore >= skillTestWaypoint.RequiredSkillScore) {
            canContinue = true;
        }

        return canContinue;
    }

    private void SendResult() {
        isRunning = false;
        OnTestDone.Invoke(currentResult);
    }
}