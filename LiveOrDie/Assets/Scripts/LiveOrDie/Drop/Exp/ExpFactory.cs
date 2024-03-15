using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExpFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack)
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/Exp", (exp) =>
        {
            exp.transform.position = position;
            exp.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(exp);
        });
    }
}
