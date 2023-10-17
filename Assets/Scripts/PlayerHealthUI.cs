using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;
    Transform ui;
    Image healthSlider;

    private void Start()
    {
        ui = Instantiate(uiPrefab,target).transform;
        ui.SetParent(target);
        healthSlider = ui.GetChild(0).GetComponent<Image>();
    }

    void OnHealthChanged(int maxHealth, int currentHealth)
    {
        if(ui != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercentage;
        }
    }
}
