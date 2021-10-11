using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatcher : MonoBehaviour
{
    public Transform axis;
    public LayerMask player;
    [HideInInspector]public float angularSpeed;
    public float ifTurnRight;
    public float radius;

    private float yTurnStart;
    private bool isTurning = false;
    private float borrowedSpeed;

    private void OnTriggerEnter(Collider col) {
        if ( (player == (player | (1 << col.gameObject.layer))) && !isTurning) {
            Debug.Log("TriggerEnter: " + gameObject.name);
            yTurnStart = 0; //Settings.current.rootWorld.eulerAngles.y;
            isTurning = true;
            Settings.current.phantomAxis.transform.position = axis.position;
            Settings.current.rootWorld.SetParent(Settings.current.phantomAxis.transform);
            borrowedSpeed = Settings.current.GetPlayerSpeed();
            Settings.current.SetPlayerSpeed(0f);
        }
    }
    private void OnTriggerExit(Collider col) {
        Debug.Log("TriggerExit: " + gameObject.name);
        if (player == (player | (1 << col.gameObject.layer))) {
            if (ifTurnRight > 0) {
                Settings.current.phantomAxis.transform.rotation = Quaternion.Euler(Settings.current.phantomAxis.transform.eulerAngles.x, yTurnStart + 90f, Settings.current.phantomAxis.transform.eulerAngles.z);
                isTurning = false;
                Settings.current.SetPlayerSpeed(borrowedSpeed);
            } else {
                Settings.current.phantomAxis.transform.rotation = Quaternion.Euler(Settings.current.phantomAxis.transform.eulerAngles.x, yTurnStart - 90f, Settings.current.phantomAxis.transform.eulerAngles.z);
                isTurning = false;
                Settings.current.SetPlayerSpeed(borrowedSpeed);
            }
        }
        Settings.current.rootWorld.SetParent(null);
        Settings.current.phantomAxis.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    private void Update() {
        if (isTurning)
            Turning(Settings.current.phantomAxis.transform, borrowedSpeed);
    }

    private void Turning(Transform _transform, float speed) {
        _transform.RotateAround(Settings.current.phantomAxis.transform.position, Settings.current.phantomAxis.transform.up, speed * radius * ifTurnRight * Time.deltaTime);
        
    }
}
