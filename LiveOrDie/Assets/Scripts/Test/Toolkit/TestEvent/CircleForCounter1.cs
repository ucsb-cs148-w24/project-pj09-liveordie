using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CircleForCounter1 : MonoBehaviour
{
    private UnityAction increaseCountAction; //Event listener
    private UnityAction<string> getInfoAction; //Event listener with parameter

    private string info = "info from circle1";

    public Counter1 counter; //reference to the counter script
    
    private void Start()
    {
        increaseCountAction += counter.IncreaseCounter; //add function to the listener
        getInfoAction += counter.GetInfoFromCollider; //add function with parameter to listener
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        increaseCountAction.Invoke(); //trigger event listener
        getInfoAction.Invoke(info); //trigger event listener with parameter
    }
    
}
