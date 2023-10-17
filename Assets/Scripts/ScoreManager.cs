using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;
    public TextMeshProUGUI scoreUI;
    int totalscore = 0;

    private void Awake()
    {
        if(scoreManager == null)
        {
            scoreManager = this;
        }
        scoreUI.text = "Score: 0";
    }
    
    public void UpdateScore(int score)
    {
        totalscore += score;
        scoreUI.text = "Score:" + totalscore.ToString();
    }
}
