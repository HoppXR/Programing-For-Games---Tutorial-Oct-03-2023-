using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    public EDifficultySelector difficultySelector;

    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate{LoadDifficultySelector(difficultySelector);});
    }

    public enum EDifficultySelector
    {
        Easy = 0,
        Normal = 1,
        Hard = 2
    }

    public void LoadDifficultySelector(EDifficultySelector diff)
    {
        SceneManager.LoadScene((int)diff);
    }
}
