using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class CharacterHealth : MonoBehaviour
{
    private float characterHealth = 50f; // can be changed
    private CharacterMovement character_control;
    private Image healthbar;
    private Color healthy = new Color(0.6f, 1, 0.6f, 1);
    private Transform trans;
    void OnEnable(){
        character_control = GetComponentInParent<CharacterMovement>();
        trans = GetComponentInParent<Transform>();
        Vector3 posit = new Vector3(trans.localPosition.x, trans.localPosition.y + 2.0f, 0);
        transform.localPosition = posit;
        foreach(var image in gameObject.GetComponentsInChildren<Image>())
        {   
            if(image.name == "FillBlock") healthbar = image; 
        }
        healthbar.color = healthy;
    }

    public void DecreaseHealth(){
        characterHealth--;
        healthbar.fillAmount = characterHealth/50;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (characterHealth <= 0){
            character_control.Kill();
        }
        else if(characterHealth > 20){
            healthbar.color = healthy;
        }
        else { healthbar.color = Color.red;}
    }
    void OnDisable(){
        if(gameObject) Destroy(gameObject);
    }
}
