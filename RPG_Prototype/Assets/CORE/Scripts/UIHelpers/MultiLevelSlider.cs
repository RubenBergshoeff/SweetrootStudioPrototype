using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLevelSlider : MonoBehaviour {

    [SerializeField] private List<FillSlider> sliders = new List<FillSlider>();

    public void UpdateValues(params float[] values) {
        if (values.Length != sliders.Count) {
            throw new System.ArgumentException("Amount of values does not match amount of sliders");
        }

        float startpoint = 0;
        for (int i = 0; i < values.Length; i++) {
            sliders[i].SetValues(startpoint, values[i]);
            startpoint += values[i];
        }
    }
}
