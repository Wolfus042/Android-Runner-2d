using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    public float scoreValue;
    public float speedIncrement;
    public float scoreMultiplier = 0;
    public GameObject pickupEffect;
    public GameObject self;

    public void Terminate() {
        if (pickupEffect != null) pickupEffect.SetActive(true);
        Settings.current.playerScore += scoreValue;
        Settings.current.SetPlayerSpeed(Settings.current.GetPlayerSpeed() + speedIncrement);
        
        if(scoreMultiplier != 0) {
            Settings.current.playerScore *= scoreMultiplier;
            Settings.current.player.DestroyPlayer();
            self = null;
        }

        Debug.Log("New score = " + Settings.current.playerScore.ToString());
        Destroy(self);
    }
}
