using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreBoardPanel : BasePanel
{
    private TMP_Text scoreText; // displays player score
    private TMP_Text mysteryText; // displays mystery drug effects

    //******************************************************************************************
    private int totalScore = 0; //it should not be here, model should be separate from view, should be changed later
    private float effectDisplayTimer = 5f; // displays mystery drug effect text for 3 seconds, then disappears
    private bool underTemporaryEffect;
    void Start() {
        scoreText = GetUIComponent<TMP_Text>("PlayerScore");
        mysteryText = GetUIComponent<TMP_Text>("DrugEffect");
        scoreText.text = "Score: "+ totalScore;
        mysteryText.text = "";
        EventMgr.Instance.AddEventListener<int>("IncrementScore", incrementScore);
        EventMgr.Instance.AddEventListener<string>("DrugText", updateMysteryDrug);
        underTemporaryEffect = false;
     }

     void Update(){
        if(underTemporaryEffect){
            effectDisplayTimer -= Time.deltaTime;
            if(effectDisplayTimer <= 0) {
                mysteryText.text = "";
                effectDisplayTimer = 5f;
                underTemporaryEffect = false;
            }
        }
     }
    void OnDestroy(){
        EventMgr.Instance.RemoveEventListener<int>("IncrementScore", incrementScore);
        EventMgr.Instance.AddEventListener<string>("DrugText", updateMysteryDrug);
    }

    private void updateMysteryDrug(string effect){
        mysteryText.text = effect;
        underTemporaryEffect = true;
    }
    
    private void incrementScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score: "+ totalScore;
        EventMgr.Instance.EventTrigger("SendScore", (float)totalScore);
    } 
}
