using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealth : MonoBehaviour
{
    private Enemy enemy;
    private Image healthbar;
    private Color healthy = new Color(0.6f, 1, 0.6f, 1);
    void OnEnable(){
        foreach(var image in GetComponentsInChildren<Image>()){ if(image.name == "FillBlock") healthbar = image; }
        healthbar.color = healthy;
    }

    public void DecreaseHealth(){
        enemy.health--;
        healthbar.fillAmount = enemy.health/10f;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemy = transform.parent.GetComponent<Wolf>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.health <= 0){
            enemy.Kill();
        }
        else if(enemy.health > 2){
            healthbar.color = healthy;
        }
        else { healthbar.color = Color.red;}
    }
    void OnDisable(){
        if(gameObject) Destroy(gameObject);
    }
}
