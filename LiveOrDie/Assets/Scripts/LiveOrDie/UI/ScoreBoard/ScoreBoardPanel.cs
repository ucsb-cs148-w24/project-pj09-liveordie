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

    //******************************************************************************************
    private int totalScore = 0; //it should not be here, model should be separate from view, should be changed later


    void Start() {
        scoreText = GetUIComponent<TMP_Text>("PlayerScore");
        scoreText.text = "Score: "+ totalScore;
        EventMgr.Instance.AddEventListener<int>("IncrementScore", incrementScore);
     }
    void OnDestroy(){
        EventMgr.Instance.RemoveEventListener<int>("IncrementScore", incrementScore);
    }
    
    private void incrementScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score: "+ totalScore;
        EventMgr.Instance.EventTrigger("SendScore", (float)totalScore);
    } 
}
