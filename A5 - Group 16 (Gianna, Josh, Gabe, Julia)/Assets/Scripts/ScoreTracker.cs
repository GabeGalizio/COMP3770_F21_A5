using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    public static int score = 0;
    public static int levels = 1;

    public Text Score_UIText;
    public Text Levels_UIText;

    void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            score = 0;
            levels = 1;
        }
    }
    void Update() {
        Score_UIText.text = score.ToString();
        Levels_UIText.text = levels.ToString();
}
}
