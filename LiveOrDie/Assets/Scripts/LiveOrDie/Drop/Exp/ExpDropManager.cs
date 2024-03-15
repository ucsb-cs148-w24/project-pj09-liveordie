using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpDropManager : MonoBehaviour
{
    private ExpFactory expFactory;

    private void Start()
    {
        expFactory = new ExpFactory();
        
        //event type parameter - position of the exp
        EventMgr.Instance.AddEventListener<Vector3>("DropExp", DropExp); 
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener<Vector3>("DropExp", DropExp);
    }

    private void DropExp(Vector3 pos)
    {
        expFactory.CreateAsync(pos, (obj) =>
        {
            // can access the exp obj here
        });
    }
    
}
