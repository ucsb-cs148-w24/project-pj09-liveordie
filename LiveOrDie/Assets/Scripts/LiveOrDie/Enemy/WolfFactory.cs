using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WolfFactory : EnemyFactory
{
    public override void CreateEnemyAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack) {
        PoolMgr.Instance.GetObjAsync("Prefabs/Wolf", (wolf) =>
        {
            wolf.transform.position = position;
            wolf.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(wolf);
        });
    }
}