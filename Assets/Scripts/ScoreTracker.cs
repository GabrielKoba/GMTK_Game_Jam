using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreTracker : MonoBehaviour {

    #region Sigleton
    private static ScoreTracker instance;
    public static ScoreTracker Instance {
        get {
            if (instance == null)
                instance = FindObjectOfType<ScoreTracker>();
            return instance;
        }
    }
    #endregion

    public int score = 0000;
    public string scoreString;
    public Text scoreText;

    void Update() {
        scoreString = ("Score: " + score);
        scoreText.text = scoreString;
    }
}
