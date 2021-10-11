using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDrawer : MonoBehaviour
{
    public Text text;

    private void Update() {
        text.text = Settings.current.playerScore.ToString();
    }
}
