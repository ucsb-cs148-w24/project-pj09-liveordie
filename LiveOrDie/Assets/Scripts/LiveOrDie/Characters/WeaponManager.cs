using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public Dictionary<string, Weapon> weapons;

    public void OnDisable(){
        weapons.Clear();
        Destroy(gameObject);
    }
    private void Start() 
    {
        weapons = new Dictionary<string, Weapon>();
        // for now, we just add fireball weapon by default
        AddWeapon("Fireball");
        AddWeapon("PeachWoodSword");
        AddWeapon("IncenseBurner");
    }

    public void AddWeapon(string name)
    {
        ResMgr.Instance.LoadAsync<GameObject>("Prefabs/Weapons/" + name, (obj) => {
            obj.transform.parent = transform;
            Weapon weapon = obj.GetComponent<Weapon>();
            if(weapon != null) {
                weapons.Add(name, weapon);
                weapon.Initialize();
                weapon.StartAutoAttack();
            }
        });
    }

    public void RemoveWeapon(string name)
    {
        if(weapons.ContainsKey(name)) {
            weapons[name].StopAutoAttack();
            Destroy(weapons[name].gameObject);
            weapons.Remove(name);
        }
    }

}