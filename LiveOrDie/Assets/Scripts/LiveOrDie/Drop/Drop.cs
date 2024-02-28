using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Drop : MonoBehaviour
{
    private float backOffset = 1f;
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickBox"))
        {
            TriggerEffect();
            DestroySelf();
        }
    }

    //put itself back into the pool
    protected abstract void DestroySelf();
    
    protected abstract void TriggerEffect();

    // private void Attracted(Vector3 pos)
    // {
    //     Vector3 dir = this.transform.position - pos;
    //     this.transform.position -= 
    // }
    
}
