using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragonFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack) {
        PoolMgr.Instance.GetObjAsync("Prefabs/Dragon", (dragon) =>
        {
            dragon.transform.position = position;
            dragon.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(dragon);
        });
    }
}