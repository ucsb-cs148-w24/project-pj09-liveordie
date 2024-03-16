using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponUnlockOrbFactory : MonoBehaviour
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack)
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/WeaponUnlockOrb", (orb) =>
        {
            orb.transform.position = position;
            orb.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(orb);
        });
    }
}
