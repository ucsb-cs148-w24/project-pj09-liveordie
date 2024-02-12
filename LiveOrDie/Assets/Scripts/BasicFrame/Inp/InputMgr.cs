using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_AllKeysActs
{
    player1up,
    player1down,
    player1left,
    player1right,
    player2up,
    player2down,
    player2left,
    player2right
    
}


#region Input manager
//Input manager is based on event center and public mono manager
//By using listeners, you can control any gameObjects

//You dont have to call any methods of this script.
//Instead, you need to add the listeners of "KeyIsPressed", "KeyIsReleased", and "KeyIsHeld"

//If you want to add new controls, just add new key value pairs inside the dict
//(I may add methods to read from files, not implemented yet)
#endregion
public class InputMgr : Singleton<InputMgr>
{
    public Dictionary<E_AllKeysActs, KeyCode> KeySet = new Dictionary<E_AllKeysActs, KeyCode>() //dict of all the controls
    {
        {E_AllKeysActs.player1up, KeyCode.W},
        {E_AllKeysActs.player1down,KeyCode.S},
        {E_AllKeysActs.player1left, KeyCode.A},
        {E_AllKeysActs.player1right, KeyCode.D},
        {E_AllKeysActs.player2up, KeyCode.UpArrow},
        {E_AllKeysActs.player2down, KeyCode.DownArrow},
        {E_AllKeysActs.player2left, KeyCode.LeftArrow},
        {E_AllKeysActs.player2right, KeyCode.RightArrow},

    };

    private bool isSwitchOn = true; //flag to open the global check
    public InputMgr() //Constructor, uses public mono manager to open Update function
    {
        MonoMgr.Instance.AddUpdateListener(InputUpdate);
        MonoMgr.Instance.AddFixedUpdateListener(InputFixedUpdate);
    }

    private void InputUpdate() //The logic in update method
    {
        // if (!isSwitchOn) return;
        // CheckKey(E_AllKeysActs.player1up);
        // CheckKey(E_AllKeysActs.player1down);
        // CheckKey(E_AllKeysActs.player1left);
        // CheckKey(E_AllKeysActs.player1right);
        // CheckKey(E_AllKeysActs.player2up);
        // CheckKey(E_AllKeysActs.player2down);
        // CheckKey(E_AllKeysActs.player2left);
        // CheckKey(E_AllKeysActs.player2right);
        
    }

    private void InputFixedUpdate()
    {
        // if (!isSwitchOn) return;
        CheckKey(E_AllKeysActs.player1up);
        CheckKey(E_AllKeysActs.player1down);
        CheckKey(E_AllKeysActs.player1left);
        CheckKey(E_AllKeysActs.player1right);
        CheckKey(E_AllKeysActs.player2up);
        CheckKey(E_AllKeysActs.player2down);
        CheckKey(E_AllKeysActs.player2left);
        CheckKey(E_AllKeysActs.player2right);
    }
    
    /// <summary>
    ///Change key sets
    /// </summary>
    /// <param name="act">the action you want to change</param>
    /// <param name="newKey">the new key you want to change to</param>
    public void ChangeKey(E_AllKeysActs act, KeyCode newKey)
    {
         KeySet[act] = newKey;
    }
    
    private void CheckKey(E_AllKeysActs act) //check if key is pressed or released, only trigger event
    {
        if (Input.GetKeyDown(KeySet[act])) //press key
        {
            EventMgr.Instance.EventTrigger("KeyIsPressed", act);
        }
        if (Input.GetKeyUp(KeySet[act])) //release key
        {
            EventMgr.Instance.EventTrigger("KeyIsReleased", act);
        }

        if (Input.GetKey(KeySet[act])) //hold key
        {
            EventMgr.Instance.EventTrigger("KeyIsHeld", act);
        }
    }

    /// <summary>
    ///Open or close global check
    /// </summary>
    /// <param name="state">open or close</param>
    public void SwitchAllButtons(bool state)
    {
        isSwitchOn = state;
    }
}



    