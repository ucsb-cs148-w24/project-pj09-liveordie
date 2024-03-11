using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public Dictionary<string, Weapon> weapons;

    private List<LevelUpChoice> weaponChoices;

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

    private void Start()
    {
        EventMgr.Instance.AddEventListener("LoadMainSceneCompleted", initWeaponChoices);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener<string>("AddWeapon", AddWeapon);
        EventMgr.Instance.RemoveEventListener<string>("RemoveWeapon", RemoveWeapon);
        EventMgr.Instance.RemoveEventListener("LoadMainSceneCompleted", initWeaponChoices);
    }

    private void initWeaponChoices()
    {
        weaponChoices = new List<LevelUpChoice>
        {
            new (Fireball.weaponName + " (Recommended)", 
                Fireball.weaponDescription, 
                () => {
                    EventMgr.Instance.EventTrigger("AddWeapon", "Fireball");
                }),
            new (PeachWoodSword.weaponName,
                PeachWoodSword.weaponDescription,
                () => {
                    EventMgr.Instance.EventTrigger("AddWeapon", "PeachWoodSword");
                }),
            new (IncenseBurner.weaponName,
                IncenseBurner.weaponDescription,
                () => {
                    EventMgr.Instance.EventTrigger("AddWeapon", "IncenseBurner");
                }),
        };

        UIMgr.Instance.ShowPanel<LevelUpPanel>("LevelUpPanel", E_PanelLayer.Top, (panel) =>
        {
            panel.transform.Find("Title").GetComponent<TMP_Text>().text = "Choose a weapon";
            panel.initWithThree(weaponChoices);
        });
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