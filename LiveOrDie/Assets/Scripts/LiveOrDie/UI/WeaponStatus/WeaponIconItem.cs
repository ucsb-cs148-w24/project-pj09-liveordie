using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponIconItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Weapon weapon;
    private Image weaponIconImage;
    private Image weaponIconCooldownImage;
    private Image weaponIconAutoAttackOutline;
    private TMP_Text WeaponIconLevelText;
    private GameObject WeaponDetailsPanel;
    private TMP_Text WeaponDetailsTitleText;
    private TMP_Text WeaponDetailsBodyText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(Weapon _weapon)
    {
        weaponIconImage = transform.Find("WeaponIconImage").GetComponent<Image>();
        weaponIconCooldownImage = transform.Find("WeaponIconCooldownImage").GetComponent<Image>();
        weaponIconAutoAttackOutline = transform.Find("WeaponIconAutoAttackOutline").GetComponent<Image>();
        WeaponIconLevelText = weaponIconImage.transform.Find("WeaponIconLevelText").GetComponent<TMP_Text>();
        WeaponDetailsPanel = transform.Find("WeaponDetailsPanel").gameObject;
        WeaponDetailsTitleText = transform.Find("WeaponDetailsPanel/WeaponDetailsTitleText").GetComponent<TMP_Text>();
        WeaponDetailsBodyText = transform.Find("WeaponDetailsPanel/WeaponDetailsBodyText").GetComponent<TMP_Text>();


        weapon = _weapon;

        weaponIconImage.sprite = weapon.GetWeaponIcon();
        
        EventMgr.Instance.AddEventListener("LevelUpWeapon", UpdateLevelText);
        EventMgr.Instance.AddEventListener("LevelUpWeapon", UpdateDetailsPanel);

        UpdateLevelText();
        UpdateDetailsPanel();
        UpdateAutoAttackVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon != null) {
            UpdateCooldown();
        }
    }

    void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("LevelUpWeapon", UpdateLevelText);
        EventMgr.Instance.RemoveEventListener("LevelUpWeapon", UpdateDetailsPanel);
    }

    private void UpdateLevelText()
    {
        WeaponIconLevelText.text = weapon.weaponLevel.ToString();
    }

    private void UpdateDetailsPanel()
    {
        WeaponDetailsTitleText.text = weapon.GetWeaponName();
        WeaponDetailsBodyText.text = weapon.GetDetailString();
    }

    private void UpdateCooldown()
    {
        if(weapon.cooldownTimeLeft > 0) {
            weaponIconCooldownImage.fillAmount = weapon.cooldownTimeLeft / weapon.weaponRate.Value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(weapon.autoAttackOn) {
            weapon.StopAutoAttack();
        } else {
            weapon.StartAutoAttack();
        }
        UpdateAutoAttackVisual();
    }

    private void UpdateAutoAttackVisual()
    {
        if(weapon.autoAttackOn) {
            weaponIconAutoAttackOutline.color = Color.yellow;
        } else {
            weaponIconAutoAttackOutline.color = Color.gray;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        WeaponDetailsPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        WeaponDetailsPanel.SetActive(false);
    }

}
