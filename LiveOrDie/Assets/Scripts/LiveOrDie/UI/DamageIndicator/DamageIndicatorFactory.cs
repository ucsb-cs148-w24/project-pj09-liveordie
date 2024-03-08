
using UnityEngine;
using UnityEngine.Events;

public class DamageIndicatorFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack)
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/DamageIndicator", (obj) => {
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(obj);
        });
    }

}