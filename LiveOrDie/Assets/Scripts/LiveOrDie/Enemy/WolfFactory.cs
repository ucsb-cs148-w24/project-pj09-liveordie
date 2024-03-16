using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WolfFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack) {
        PoolMgr.Instance.GetObjAsync("Prefabs/Wolf", (wolf) =>
        {
            wolf.GetComponent<NavMeshAgent>().Warp(position);
            wolf.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(wolf);
        });
    }
}