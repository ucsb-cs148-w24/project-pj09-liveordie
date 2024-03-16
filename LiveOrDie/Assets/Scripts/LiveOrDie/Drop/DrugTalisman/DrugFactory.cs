using UnityEngine;
using UnityEngine.Events;

public class DrugFactory : IFactory
{
    public void CreateAsync(Vector3 position, UnityAction<GameObject> AfterPoolCallBack)
    {
        PoolMgr.Instance.GetObjAsync("Prefabs/Drug", (drug) =>
        {
            drug.transform.position = position;
            drug.transform.rotation = Quaternion.identity;
            AfterPoolCallBack(drug);
        });
    }
}