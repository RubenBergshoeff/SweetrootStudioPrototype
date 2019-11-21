using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTestController : MonoBehaviour {
    [SerializeField] private Transform followPoint = null;
    [SerializeField] private Vector2 clampXPosition = new Vector2();

    private void Update() {
        float xPos = followPoint.localPosition.x;
        xPos = Mathf.Clamp(xPos, clampXPosition.x, clampXPosition.y);
        transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
    }
}
