
using UnityEngine;
using UnityEngine.Events;

public class PopupIndicatorFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack)
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/PopupIndicator", (obj) => {
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(obj);
        });
    }

}