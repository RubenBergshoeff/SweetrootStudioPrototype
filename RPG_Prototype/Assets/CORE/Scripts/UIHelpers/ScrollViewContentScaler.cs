using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewContentScaler : MonoBehaviour {
    [SerializeField] private float widthPerChild = 500;

    public void UpdateView() {
        Debug.Log("updating view");
        HorizontalLayoutGroup layoutGroup = GetComponent<HorizontalLayoutGroup>();
        float padding = 0;

        if (layoutGroup != null) {
            padding = layoutGroup.spacing;
        }

        float height = GetComponent<RectTransform>().sizeDelta.y;
        GetComponent<RectTransform>().sizeDelta = new Vector2(((transform.childCount - 1) * (widthPerChild + padding)), height);
    }
}
