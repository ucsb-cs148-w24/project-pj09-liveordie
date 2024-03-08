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
    private string effectText; // saves most recent text
    //******************************************************************************************
    private int totalScore = 0; //it should not be here, model should be separate from view, should be changed later
    private float effectDisplayTimer; // displays mystery drug effect text for 3 seconds, then disappears
    private bool underTemporaryEffect, needTimer, mushroomEffect;

    void Start() {
        scoreText = GetUIComponent<TMP_Text>("PlayerScore");
        mysteryText = GetUIComponent<TMP_Text>("DrugEffect");
        scoreText.text = "Score: "+ totalScore;
        mysteryText.text = "";
        effectText = "";
        effectDisplayTimer = 0f;
        EventMgr.Instance.AddEventListener<int>("IncrementScore", incrementScore);
        EventMgr.Instance.AddEventListener<string>("DrugText", updateMysteryDrug);
        EventMgr.Instance.AddEventListener<float>("TimeText", updateTimeLeft);
        EventMgr.Instance.AddEventListener<bool>("MagicMushroom", handleMagic);
        underTemporaryEffect = false;
        needTimer = false;
        mushroomEffect = false;
     }

     void Update(){
        if(underTemporaryEffect){
            effectDisplayTimer -= Time.deltaTime;
            mysteryText.text = needTimer ? ((int)effectDisplayTimer).ToString() + ": " + effectText : effectText;
            if(effectDisplayTimer <= 0) {
                mysteryText.text = "";
                underTemporaryEffect = false;
                needTimer = false;
                mushroomEffect = false;
            }
        }
     }
    void OnDestroy(){
        EventMgr.Instance.RemoveEventListener<int>("IncrementScore", incrementScore);
        EventMgr.Instance.RemoveEventListener<string>("DrugText", updateMysteryDrug);
        EventMgr.Instance.RemoveEventListener<float>("TimeText", updateTimeLeft);
    }
    private void handleMagic(bool val){
        mushroomEffect = true;
    }
    private void updateMysteryDrug(string effect){
        effectText = effect;
        underTemporaryEffect = true;
    }
    
    private void updateTimeLeft(float time){
        effectDisplayTimer = time;
        needTimer = time > 5f ? true: false;
    }
    private void incrementScore(int score)
    {
        totalScore = mushroomEffect ? totalScore - score: totalScore + score;
        scoreText.text = "Score: "+ totalScore;
        EventMgr.Instance.EventTrigger("SendScore", (float)totalScore);
    } 
}
