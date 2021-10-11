using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveZoneVisualiser : MonoBehaviour
{
    public Image moveZoneImage;

    private void Start() {
        if (moveZoneImage == null) {
            moveZoneImage = GetComponent<Image>();
        }
    }
    void Update() {
        Vector2 correctSize = new Vector2(Settings.current.getScreenMeasures(true), Settings.current.getScreenMeasures(false) * Settings.current.getMoveZoneHeight());
        moveZoneImage.rectTransform.sizeDelta = correctSize;
    }
}
