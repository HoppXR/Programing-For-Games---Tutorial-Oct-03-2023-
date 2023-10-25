using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int score = 0;

    public string nameCollectable;
    public int scoreCollectable;
    public int restoreHP;

    public Collectable(string name, int scoreValue, int restoreHPValue)
    {
        this.nameCollectable = name;
        this.score = scoreValue;
        this.restoreHP = restoreHPValue;
    }
    
    public void UpdateScore()
    {
        ScoreManager.scoreManager.UpdateScore(score);
    }

    public void UpdateHealth()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(gameObject);
            score++;
            Debug.Log("Score: " + score);
        }
    }
}
