using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTracker : MonoBehaviour
{
    public static int score = 0;

    public Text Score_UIText;

    void Start() {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            score = 0;
        }
    }
    void Update() {
        Score_UIText.text = score.ToString();
    }
}
