using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloner : MonoBehaviour
{
    public List<GameObject> points; // list of the points that enemies will appear
    public GameObject enemy; // game object of enemy
    public float waitTime; // wait time for an enemy to appear on the screen
    public GameObject enemyClone; // put enemies under empty object after creating
    
    IEnumerator Clone(){
        for(int i = 0; i < 100; i++){
            yield return new WaitForSeconds(waitTime);
            GameObject e = Instantiate(enemy.gameObject, points[Random.Range(0, points.Count)].transform.position, Quaternion.identity);
            e.transform.SetParent(enemyClone.transform);
        }
    }

    private void Start(){
        StartCoroutine(Clone());
    }
}
