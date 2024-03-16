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
        EventMgr.Instance.AddEventListener<Vector3>("DropBigExp", DropBigExp);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener<Vector3>("DropExp", DropExp);
    }

    private void DropExp(Vector3 pos)
    {
        expFactory.CreateAsync(pos, (exp) =>
        {
            exp.transform.localScale = Vector3.one/2;
            exp.GetComponent<Exp>().expValue = 1f;
        });
    }

    private void DropBigExp(Vector3 pos)
    {
        expFactory.CreateAsync(pos, (exp) =>
        {
            exp.transform.localScale = Vector3.one;
            exp.GetComponent<Exp>().expValue = 10f;
        });
    }
}
