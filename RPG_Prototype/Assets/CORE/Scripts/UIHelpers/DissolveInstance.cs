using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveInstance : MonoBehaviour {
    [SerializeField] private Material dissolveMaterial = null;
    [SerializeField] private Material endMaterial = null;

    private Renderer rend;
    private float visibility = 0;
    private bool isRunning = false;

    private void OnEnable() {
        rend = GetComponent<Renderer>();
        StartSetup();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            isRunning = !isRunning;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            StartSetup();
        }
        if (isRunning == false) { return; }

        if (visibility < 1) {
            visibility += Time.deltaTime / 2.5f;
            visibility = Mathf.Clamp01(visibility);
            rend.material.SetFloat("_Visibility", visibility);
        } else if (visibility >= 1) {
            rend.material = endMaterial;
        }
    }

    private void StartSetup() {
        rend.material = dissolveMaterial;
        visibility = 0;
        rend.material.SetFloat("_Visibility", visibility);
    }
}
