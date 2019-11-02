using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewContentScaler : MonoBehaviour {
    [SerializeField] private float widthPerChild = 500;
    [SerializeField] private bool vertical = false;

    public void UpdateView() {
        HorizontalLayoutGroup layoutGroup = GetComponent<HorizontalLayoutGroup>();
        float padding = 0;

        if (layoutGroup != null) {
            padding = layoutGroup.spacing;
        }

        if (!vertical) {
            float height = GetComponent<RectTransform>().sizeDelta.y;
            GetComponent<RectTransform>().sizeDelta = new Vector2(((transform.childCount - 1) * (widthPerChild + padding)), height);
        } else {
            float width = GetComponent<RectTransform>().sizeDelta.x;
            GetComponent<RectTransform>().sizeDelta = new Vector2(width, ((transform.childCount - 1) * (widthPerChild + padding)));

        }
    }
}
