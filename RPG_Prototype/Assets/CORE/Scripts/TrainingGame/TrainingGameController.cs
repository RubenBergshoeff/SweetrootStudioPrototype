using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrainingGameController : MonoBehaviour {

    public abstract void Setup(ActiveTraining training);

    public abstract void StartTraining();

    public abstract void Cleanup();
}
