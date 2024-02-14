using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 10f; // can be changed
    private EnemyMovement enemy_Control;
    private Image healthbar;
    private Color healthy = new Color(0.6f, 1, 0.6f, 1);
    void OnEnable(){
        foreach(var image in GetComponentsInChildren<Image>()){ if(image.name == "FillBlock") healthbar = image; }
        healthbar.color = healthy;
    }

    public void DecreaseHealth(){
        enemyHealth--;
        healthbar.fillAmount = enemyHealth/10;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemy_Control = transform.parent.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0){
            enemy_Control.Kill();
        }
        else if(enemyHealth > 2){
            healthbar.color = healthy;
        }
        else { healthbar.color = Color.red;}
    }
    void OnDisable(){
        if(gameObject) Destroy(gameObject);
    }
}
