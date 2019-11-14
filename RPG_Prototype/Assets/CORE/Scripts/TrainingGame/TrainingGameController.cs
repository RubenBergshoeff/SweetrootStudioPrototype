using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrainingGameController : MonoBehaviour {
    public delegate void XPGainDelegate(int xpAmount);
    public XPGainDelegate OnXPGain;
    public Action OnGameFinished;

    public abstract void Setup(ActiveTraining training);

    public abstract void StartTraining();

    public abstract void Cleanup();
}
