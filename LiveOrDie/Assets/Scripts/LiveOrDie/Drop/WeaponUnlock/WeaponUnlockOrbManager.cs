using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlockOrbManager : MonoBehaviour
{
    private WeaponUnlockOrbFactory orbFactory;

    private void Start()
    {
        orbFactory = new WeaponUnlockOrbFactory();
        
        //event type parameter - position of the exp
        EventMgr.Instance.AddEventListener<Vector3>("DropWeaponUnlock", DropOrb); 
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener<Vector3>("DropWeaponUnlock", DropOrb);
    }

    private void DropOrb(Vector3 pos)
    {
        orbFactory.CreateAsync(pos, (obj) =>
        {
            // can access the exp obj here
        });
    }
}
