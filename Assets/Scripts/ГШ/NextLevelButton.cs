using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{

    public void PlayLevel() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
