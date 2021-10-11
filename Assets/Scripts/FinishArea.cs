using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FinishArea : MonoBehaviour
{
    public LayerMask player;



    public float barValue;
    public float barValueMax;
    public float clickForce;
    public float constDownForce;

    public float scoreHeightMax;
    public float finishPercent;

    public Image image;
    public GameObject graphs;

    public bool isWorking = false;

    private void Start() {
        isWorking = false;
        scoreHeightMax = image.rectTransform.sizeDelta.y;
    }

    private void Update() {
        if (isWorking) {
            barValue -= constDownForce;
            float newHeight = barValue / barValueMax * scoreHeightMax;
            if (newHeight > scoreHeightMax) {
                newHeight = scoreHeightMax;
                barValue = barValueMax;
            }
            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, newHeight); 
        }
    }

    public void Finish() {
        graphs.SetActive(true);
        isWorking = true;
    }

    private void OnTriggerExit(Collider col) {
            isWorking = false;
            finishPercent = barValue / barValueMax;
            Debug.Log("percent equals " + finishPercent.ToString());
    }
}
