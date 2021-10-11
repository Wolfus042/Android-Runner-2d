using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings current;
    public FinishArea finArea;

    public float playerWidth = 0.05f;
    //public float widthUpperBorder;
    public float playerScore;

    [Range(0f, 1f)]
    public float moveZoneHeight;



    private float screenWidth;
    private float screenHeight;

    public GameObject buildingBlock;
    public Transform rootWorld;
    public GameObject phantomAxis;
    private float playerSpeed;
    public float jumpFactor = 1;


    [HideInInspector]public SimpleMove player;

    public void RegisterPlayerMove(SimpleMove move) {
        player = move;
    }

    private void Awake() {
        current = this;
        screenWidth = Screen.width;
        screenHeight = Screen.height; 

        /*widthUpperBorder = buildingBlock.transform.localScale.x - playerWidth;
        widthUpperBorder /= 2; //половинка от рассчитанного относительно центра*/
        ///уже не нужно — все решается коллайдерами


    }

    public void SetPlayerSpeed(float speed) {
        player.speedAndGravity.z = speed;
        playerSpeed = speed;
    }
    public float GetPlayerSpeed() {
        return playerSpeed;
    }

    ///<summary> при передаче true возвращает ширину экрана, при передаче false — его высоту </summary>
    public float getScreenMeasures(bool needWidth) {
        if (needWidth) return screenWidth;
        else return screenHeight;
    }

    /// <summary>
    /// возвращает часть экрана (от 0 до 1), что занята зоной для перемещения (не-прыжковая зона так сказать... е-мое..)
    /// </summary>
    public float getMoveZoneHeight() {
        return moveZoneHeight;
    }

    private void Start() {


    }
}
