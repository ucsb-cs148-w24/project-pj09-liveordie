using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStatusPanel : BasePanel
{
    private WeaponManager weaponManager;
    private List<Weapon> weapons;
    private GameObject weaponIcons;

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GameObject.Find("Players").GetComponentInChildren<WeaponManager>();
        weaponIcons = transform.Find("WeaponIcons").gameObject;

        EventMgr.Instance.AddEventListener("UpdateWeaponView", RefreshWeaponView);

        // for now, we just add all weapons by default
        EventMgr.Instance.EventTrigger<string>("AddWeapon", "Fireball");
        EventMgr.Instance.EventTrigger<string>("AddWeapon", "PeachWoodSword");
        EventMgr.Instance.EventTrigger<string>("AddWeapon", "IncenseBurner");
    }


    public void RefreshWeaponView()
    {
        ClearWeaponView();
        weapons = weaponManager.weapons.Values.ToList();
        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject item = ResMgr.Instance.Load<GameObject>("UI/WeaponStatusPanel/WeaponIconItem");
            item.transform.SetParent(weaponIcons.transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            WeaponIconItem weaponIconItem = item.GetComponent<WeaponIconItem>();
            weaponIconItem.Initialize(weapons[i]);
        }
    }

    private void ClearWeaponView()
    {
        foreach (Transform child in weaponIcons.transform)
        {
            Destroy(child.gameObject);
        }
    }


}
