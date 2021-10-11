using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    // в этом скрипте мы обрабатываем, что происходит при столкновении с разными объедками
    GameObject deathEffect;
    public LayerMask bonuses;
    public LayerMask dangers;
    public LayerMask finalArea;
    public LayerMask ground;
    public LayerMask jumper;

    public GameObject deathText;
    


    public SimpleMove moveScript;

    private void OnTriggerEnter(Collider col) {
        Debug.Log("COL: "+ col.gameObject.name.ToString());
        if (bonuses == (bonuses | (1 << col.gameObject.layer))) {
            BonusItem item = col.gameObject.GetComponent<BonusItem>();
            if (item != null) item.Terminate();
        } else if (dangers == (dangers | (1 << col.gameObject.layer))) {
            Spiked();
        } else if (finalArea == (finalArea | (1 << col.gameObject.layer))) {
            Settings.current.finArea.Finish();
            InputManager.current.ifOnFinishZone = true;
        } else if(jumper == (jumper | (1 << col.gameObject.layer))) {
            FinalJump(Settings.current.finArea.finishPercent);
        }
    }

    public void FinalJump(float percent) {
        Settings.current.player.isJumping = true;
        Settings.current.player.animator.SetBool("isJumpin", true);
        Settings.current.player.currentJumpingTime = 0;
        Settings.current.player.jumpDuration = percent;
    }

    private void Spiked() {
        deathText.SetActive(true);
        moveScript.DestroyPlayer();
        InputManager.current.enabled = false;
        Debug.Log("You are dead!");
    }

}
