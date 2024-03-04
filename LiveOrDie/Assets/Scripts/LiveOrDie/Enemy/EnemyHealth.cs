using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealth : MonoBehaviour
{
    [HideInInspector]
    public Enemy enemy; //assigned in enemy script
    
    //assigned in inspector
    public Image healthbar;
    private Color healthy = new Color(0.6f, 1, 0.6f, 1);
    public float fillBar;
    
    public void Initialize()
    {
        healthbar.color = healthy;
        UpdateHealthBar(); //update the health bar if enemy is enabled from the pool
    }

    public void UpdateHealthBar()
    {
        healthbar.fillAmount = enemy.health/fillBar;
        if(enemy.health > 2){
            healthbar.color = healthy;
        }
        else { healthbar.color = Color.red;}
    }
    
}
