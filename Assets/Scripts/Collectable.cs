using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int Score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Destroy(gameObject);
            Score++;
            Debug.Log("Score: " + Score);
        }
    }
}
