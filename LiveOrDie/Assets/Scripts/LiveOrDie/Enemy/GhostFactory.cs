using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GhostFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack) {
        PoolMgr.Instance.GetObjAsync("Prefabs/Ghost", (ghost) =>
        {
            ghost.transform.position = position;
            ghost.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(ghost);
        });
    }
}