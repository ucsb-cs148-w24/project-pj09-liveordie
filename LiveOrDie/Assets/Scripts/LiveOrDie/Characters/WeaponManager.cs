using UnityEngine;
using System.Collections.Generic;


public class WeaponManager : MonoBehaviour
{
    public Dictionary<string, Weapon> weapons;

    public void OnDisable(){
        weapons.Clear();
        Destroy(gameObject);
    }
    private void Awake() 
    {
        weapons = new Dictionary<string, Weapon>();

        EventMgr.Instance.AddEventListener<string>("AddWeapon", AddWeapon);
        EventMgr.Instance.AddEventListener<string>("RemoveWeapon", RemoveWeapon);
        
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener<string>("AddWeapon", AddWeapon);
        EventMgr.Instance.RemoveEventListener<string>("RemoveWeapon", RemoveWeapon);
    }

    public void AddWeapon(string name)
    {
        ResMgr.Instance.LoadAsync<GameObject>("Prefabs/Weapons/" + name, (obj) => {
            obj.transform.parent = transform;
            Weapon weapon = obj.GetComponent<Weapon>();
            if(weapon != null) {
                weapons.Add(name, obj.GetComponent<Weapon>());
                weapon.Initialize();
                weapon.StartAutoAttack();
            }
            EventMgr.Instance.EventTrigger("UpdateWeaponView");
        });
    }

    public void RemoveWeapon(string name)
    {
        if(weapons.ContainsKey(name)) {
            weapons[name].StopAutoAttack();
            Destroy(weapons[name].gameObject);
            weapons.Remove(name);
            EventMgr.Instance.EventTrigger("UpdateWeaponView");
        }
    }

}