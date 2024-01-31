using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator wat;
    void Start()
    {
        wat = Wait();
        StartCoroutine(wat);
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
