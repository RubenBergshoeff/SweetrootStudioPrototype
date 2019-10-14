using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewContentScaler : MonoBehaviour {
    [SerializeField] private float widthPerChild = 500;

    private void OnValidate() {
        HorizontalLayoutGroup layoutGroup = GetComponent<HorizontalLayoutGroup>();
        float padding = 0;

        if (layoutGroup != null) {
            padding = layoutGroup.padding.horizontal;
        }

        float height = GetComponent<RectTransform>().sizeDelta.y;
        GetComponent<RectTransform>().sizeDelta = new Vector2(padding + (transform.childCount * (widthPerChild + padding)), height);
    }
}
