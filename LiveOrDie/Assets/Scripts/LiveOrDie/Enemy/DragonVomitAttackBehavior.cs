using System.Collections;
using UnityEngine;

public class DragonVomitAttackBehavior : MonoBehaviour
{
    public CharacterStat attackDamage, attackRate, projectileRange;
    public StatModifier damageModifier, rateModifier;
    private Rigidbody2D vomitRb;
    // private SpriteRenderer dragonSprite;
    public bool autoAttackOn;
    public float cooldownTimeLeft;
    private Coroutine autoAttackRoutine;
    private Vector3 firePointOffset = new Vector3(0,1,0);
    private GameObject dragon;
    private Transform dragonFp;

    public void Initialize()
    {
        attackDamage = new CharacterStat(baseValue: 100f, minValue: 0f, maxValue: -1f);
        attackRate = new CharacterStat(baseValue: 1f, minValue: 0.10f, maxValue: -1f);
        damageModifier = new StatModifier(StatModifierType.Flat, 0f, StatModifierOrder.BaseModifier);
        rateModifier = new StatModifier(StatModifierType.PercentMult, 100f, StatModifierOrder.BaseModifier);
        projectileRange = new CharacterStat(baseValue: 40.0f, minValue: 1.0f, maxValue: 200.0f);
        
        dragon = GameObject.Find("Dragon");
        dragonFp = dragon.transform;
        vomitRb = GetComponent<Rigidbody2D>();
        // dragonSprite = dragon.GetComponent<SpriteRenderer>();
        autoAttackOn = false;
        
    }

    public void Fire(bool flipX) 
    {
        // dragonSprite.flipX = flipX;
        // Vector2 direction = new Vector2(flipX ? 1 : -1, 0); 
        // vomitRb.velocity = direction * attackRate.Value;
        // StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(projectileRange.Value/attackRate.Value);
        // if(gameObject != null) {
        //     PoolMgr.Instance.PushObj("Prefabs/Weapons/Vomit",this.gameObject);
        // }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // if(other.CompareTag("Player1") || other.CompareTag("Player2")) {
        //     other.GetComponent<Player>().healthbar.DecreaseHealth(attackDamage.Value);
        //     PoolMgr.Instance.PushObj("Prefabs/Weapons/Vomit",this.gameObject);
        // }
    }
     void OnDisable(){
        // StopAutoAttack();
    }
    public void StartAutoAttack()
    {
        // autoAttackOn = true;
        // autoAttackRoutine = StartCoroutine(AutoAttackRoutine());
    }

    protected IEnumerator AutoAttackRoutine()
    {
        // while(autoAttackOn) {
        //     for(cooldownTimeLeft = attackRate.Value; cooldownTimeLeft > 0; cooldownTimeLeft -= Time.deltaTime) {
                yield return null;
        //     }
        //     Fire(dragonSprite.flipX);
        // }
    }

    public void StopAutoAttack()
    {
        // autoAttackOn = false;
        // StopCoroutine(autoAttackRoutine);
    }

}