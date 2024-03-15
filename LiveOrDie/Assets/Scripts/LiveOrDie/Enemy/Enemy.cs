using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public int health {get; set;}
    public int damage {get; set;}
    public GameObject target {get; set;}
    public Material originalMat;
    public Material highlightMat;
    protected EnemyAttack attack;
    protected PopupIndicatorFactory damageIndicatorFactory = new PopupIndicatorFactory();
    protected SpriteRenderer render;
    protected Coroutine highlightCoroutine;

    public string EnemyAttackAudioName;

    public abstract void Initialize();

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        damageIndicatorFactory.CreateAsync(transform.position, (obj) => {
            obj.transform.SetParent(transform);
            obj.GetComponentInChildren<PopupIndicator>().Initialize(damage.ToString());
        });
        DamageHighlight();
    }

    protected virtual void DamageHighlight() 
    {
        if (highlightCoroutine != null) StopCoroutine(highlightCoroutine);
        if (gameObject.activeSelf) highlightCoroutine = StartCoroutine(DamageHighlightCoroutine());
    }

    protected virtual IEnumerator DamageHighlightCoroutine()
    {
        render.material = highlightMat;
        yield return new WaitForSeconds(0.1f);
        render.material = originalMat;
    }

    protected virtual void Die() {}

}
