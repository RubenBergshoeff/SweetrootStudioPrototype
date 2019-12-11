using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLevelSlider : MonoBehaviour {

    [SerializeField] private Gradient gradient;
    [SerializeField] private List<FillSlider> sliders = new List<FillSlider>();
    [SerializeField] private FillSlider templateSlider = null;

    private void OnEnable() {
        templateSlider.gameObject.SetActive(false);
    }

    public void UpdateValues(params float[] values) {
        if (values.Length != sliders.Count) {
            UpdateSliderAmount(values.Length);
        }

        float startpoint = 0;
        for (int i = 0; i < values.Length; i++) {
            sliders[i].SetValues(startpoint, values[i], gradient.Evaluate(startpoint));
            startpoint += values[i];
        }
    }

    private void UpdateSliderAmount(int length) {
        while (sliders.Count > length) {
            sliders.RemoveAt(sliders.Count - 1);
        }
        while (sliders.Count < length) {
            templateSlider.gameObject.SetActive(true);
            GameObject newSliderObject = Instantiate(templateSlider.gameObject, templateSlider.transform.parent);
            FillSlider slider = newSliderObject.GetComponent<FillSlider>();
            sliders.Add(slider);
            templateSlider.gameObject.SetActive(false);
        }
    }
}
