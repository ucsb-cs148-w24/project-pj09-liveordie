using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonVomit : MonoBehaviour
{
    public CharacterStat weaponDamage, weaponRate, projectileSpeed, projectileRange;
    public StatModifier damageLevelModifier, rateLevelModifier, projectileSpeedModifier, projectileRangeModifier;
    public bool autoAttackOn;
    public float cooldownTimeLeft;
    private Coroutine autoAttackRoutine;
    private Transform p1Fp, p2fp;
    private Vector3 firePointOffset = new Vector3(0,1,0);
    private SpriteRenderer p1Sprite, p2Sprite;
    private GameObject player1, player2;
    public override void Initialize() {
        vomit = GetComponent<DragonVomit>();
        health = 1000;
        damage = 50;
        SetTarget();

        weaponDamage = new CharacterStat(baseValue: 1f, minValue: 0f, maxValue: -1f);
        weaponRate = new CharacterStat(baseValue: 1f, minValue: 0.01f, maxValue: -1f);
        damageLevelModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.BaseModifier);
        rateLevelModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);

        projectileSpeed = new CharacterStat(baseValue: 10.0f, minValue: 1.0f, maxValue: 100.0f);
        projectileRange = new CharacterStat(baseValue: 20.0f, minValue: 1.0f, maxValue: 200.0f);

        weaponLevel = 1;

        autoAttackOn = false;

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        p1FirePoint = player1.transform;
        p2FirePoint = player2.transform;
        p1Sprite = player1.GetComponent<SpriteRenderer>();
        p2Sprite = player2.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        Initialize();
    }
    void OnDisable(){
        StopAutoAttack();
    }
    public virtual void StartAutoAttack()
    {
        autoAttackOn = true;
        autoAttackRoutine = StartCoroutine(AutoAttackRoutine());
    }

    protected virtual IEnumerator AutoAttackRoutine()
    {
        while(autoAttackOn) {
            for(cooldownTimeLeft = weaponRate.Value; cooldownTimeLeft > 0; cooldownTimeLeft -= Time.deltaTime) {
                yield return null;
            }
            StartAutoAttack();
        }
    }

    public virtual void StopAutoAttack()
    {
        autoAttackOn = false;
        StopCoroutine(autoAttackRoutine);
    }
}
