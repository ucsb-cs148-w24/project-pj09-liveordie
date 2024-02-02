using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloner : MonoBehaviour
{
    public List<GameObject> points; // list of points
    public GameObject enemy; // game object of the enemy
    float waitTime; // wait time for each enemy to appear
    public GameObject enemyClone; // put the cloned enemy under the position of the empty object of the points

    IEnumerator Clone()
    {
        for (int i = 0; i < 20; i++){
            yield return new WaitForSeconds(waitTime);
            GameObject e = Instantiate(enemy.gameObject, points[Random.Range(0, points.Count)].transform.position, Quaternion.identity);
            e.transform.SetParent(enemyClone.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        waitTime = 2;
        StartCoroutine(Clone());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
