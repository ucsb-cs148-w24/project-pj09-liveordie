using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TalismanFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack)
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/Talisman", (talisman) =>
        {
            talisman.transform.position = position;
            talisman.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(talisman);
        });
    }
}