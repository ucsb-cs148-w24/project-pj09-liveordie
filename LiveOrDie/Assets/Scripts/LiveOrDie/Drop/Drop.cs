using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Drop : MonoBehaviour
{
    // private float backOffset = 1f;
    private float attractSpeed = 3f;
    private float backOffSpeed = 2f;
    private float backOffDist = 1f;
    private CircleCollider2D col;

    private void OnEnable()
    {
        col = GetComponent<CircleCollider2D>();
        col.enabled = true;
        backOffDist = 1f;
    }
    

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickBox"))
        {
            StartCoroutine(AttractedCoroutine(other));
        }
    }

    //put itself back into the pool
    protected abstract void DestroySelf();
    
    protected abstract void TriggerEffect();

    IEnumerator AttractedCoroutine(Collider2D other)
    {
        col.enabled = false;
        while (backOffDist > 0)
        {
            Vector3 backV = (other.transform.position - this.transform.position) * (backOffSpeed * Time.deltaTime);
            this.transform.position -= backV;
            backOffDist -=  backV.magnitude;
            yield return new WaitForEndOfFrame();
        }
        while ((this.transform.position - other.transform.position).magnitude > 1f)
        {
            this.transform.position +=
                (other.transform.position - this.transform.position) * (attractSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        TriggerEffect();
        DestroySelf();
    }
    
}