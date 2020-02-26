using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class FlipPlane : MonoBehaviour {
    [SerializeField] private Vector3 startEulerRotation;
    [SerializeField] private Vector3 endEulerRotation;
    [SerializeField] private float flipDelay = 0;

    public void GoToStartRotation() {
        transform.rotation = Quaternion.Euler(startEulerRotation);
    }

    [ContextMenu("DoFlip")]
    public void FlipUp() {
        StartCoroutine(FlipUpDelayed(flipDelay));
    }

    private IEnumerator FlipUpDelayed(float delay) {
        yield return new WaitForSeconds(delay);
        transform.rotation = Quaternion.Euler(startEulerRotation);
        transform.DOLocalRotate(endEulerRotation, UnityEngine.Random.Range(1.1f, 1.4f)).SetEase(Ease.InOutQuart);
    }

    [ContextMenu("Current Rotation as Start Rotation")]
    private void CurrentToStartRotation() {
        startEulerRotation = transform.rotation.eulerAngles;
    }

    [ContextMenu("Current Rotation as End Rotation")]
    private void CurrentToEndRotation() {
        endEulerRotation = transform.rotation.eulerAngles;
    }
}