using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class Scoreboard : MonoBehaviour
{
    private int points; // keeps track of points
    private CharacterMovement player; // keeps track of parent
    private TMP_Text scoreText; // displays player score

    /// <summary>
    /// Fetches player's current score, returns integer score
    /// </summary>
    /// <param name="playerNum">the unique player ID</param>
    public int getScore() {return points;} // display Score
    
    /// <summary>
    /// Increments player's current score, no return
    /// </summary>
    /// <param name="playerNum">the unique player ID</param>
    public void incrementScore(int playerNum) { 
        if (playerNum == player.whichCharacter){
            points++; 
        }
    } // will move to Scoreboard script

    // GAME FUNCTIONS
    void OnEnable(){
        player = GetComponentInParent<CharacterMovement>();
        scoreText = GetComponent<TMP_Text>();
    }
    void Start() {
        points = 0;
        scoreText.text = (player.whichCharacter + "'s Score: " + points).ToString();
        EventMgr.Instance.AddEventListener<int>("IncrementScore", incrementScore);
     }
    void Update() {
        scoreText.text = (player.whichCharacter + "'s Score: " + points).ToString();
    }
    void OnDisable(){
        Destroy(scoreText);
        EventMgr.Instance.RemoveEventListener<int>("IncrementScore", incrementScore);
    }
}
