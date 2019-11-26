using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTestEnvironmentController : MonoBehaviour {
    [SerializeField] private DissolveInstance[] dissolveInstances = new DissolveInstance[0];
    private int currentIndex = 0;

    public void Initialize() {
        currentIndex = 0;
    }

    public void UpdateChildren(float currentXPos) {
        while ((currentIndex < dissolveInstances.Length) && (dissolveInstances[currentIndex].transform.position.x < currentXPos)) {
            dissolveInstances[currentIndex].StartDissolve();
            currentIndex++;
        }
    }

    [ContextMenu("Get all childinstances")]
    private void GetAllChildInstances() {
        dissolveInstances = GetComponentsInChildren<DissolveInstance>();
        List<DissolveInstance> sortList = new List<DissolveInstance>(dissolveInstances);
        sortList.Sort((lhs, rhs) => lhs.transform.position.x.CompareTo(rhs.transform.position.x));
        dissolveInstances = sortList.ToArray();
    }
}
