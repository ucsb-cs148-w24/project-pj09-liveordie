using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    public Dictionary<string, Weapon> weapons;

    private List<LevelUpChoice> weaponChoices;

    private List<string> weaponAvailable;

    public void OnDisable(){
        weapons.Clear();
        Destroy(gameObject);
    }
    private void Awake() 
    {
        weapons = new Dictionary<string, Weapon>();
        weaponAvailable = new List<string>{ "Fireball", "PeachWoodSword", "IncenseBurner"};
        
        EventMgr.Instance.AddEventListener<string>("RemoveWeapon", RemoveWeapon);
        EventMgr.Instance.AddEventListener("UnlockWeapon", UnlockWeapon);
        
    }

    private void Start()
    {
        EventMgr.Instance.AddEventListener("LoadingPanelCompleted", initWeaponChoices);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener<string>("RemoveWeapon", RemoveWeapon);
        EventMgr.Instance.RemoveEventListener("LoadingPanelCompleted", initWeaponChoices);
        EventMgr.Instance.RemoveEventListener("UnlockWeapon", UnlockWeapon);
        
    }

    private void initWeaponChoices()
    {
        weaponChoices = new List<LevelUpChoice>
        {
            new (Fireball.weaponName + " (Recommended)", 
                Fireball.weaponDescription, 
                ResMgr.Instance.Load<Sprite>("Sprites/Icons/fireball_icon"),
                () => {
                    AddWeapon("Fireball");
                    weaponAvailable.Remove("Fireball");
                }),
            new (PeachWoodSword.weaponName,
                PeachWoodSword.weaponDescription,
                ResMgr.Instance.Load<Sprite>("Sprites/Icons/pws_icon"),
                () => {
                    AddWeapon("PeachWoodSword");
                    weaponAvailable.Remove("PeachWoodSword");
                }),
            new (IncenseBurner.weaponName,
                IncenseBurner.weaponDescription,
                ResMgr.Instance.Load<Sprite>("Sprites/Icons/incense_burner_icon"),
                () => {
                    AddWeapon("IncenseBurner");
                    weaponAvailable.Remove("IncenseBurner");
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

    private void UnlockWeapon()
    {
        if (weaponAvailable.Count == 0) return;
        string weaponUnlocked = weaponAvailable[Random.Range(0, weaponAvailable.Count)];
        AddWeapon(weaponUnlocked);
        weaponAvailable.Remove(weaponUnlocked);
    }

}