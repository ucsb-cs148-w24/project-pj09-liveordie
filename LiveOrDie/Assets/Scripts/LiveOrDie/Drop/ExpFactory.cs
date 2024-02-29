using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExpFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack)
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/Exp", (wolf) =>
        {
            wolf.transform.position = position;
            wolf.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(wolf);
        });
    }
}
