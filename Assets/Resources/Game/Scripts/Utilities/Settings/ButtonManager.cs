using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Color hoverColor;
    Color defaultColor;
    void Awake() {
        defaultColor = gameObject.GetComponent<Image>().color;
    }
    public void OnPointerEnter(PointerEventData eventData) {
        gameObject.GetComponent<Image>().color = hoverColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = defaultColor;
    }
}
