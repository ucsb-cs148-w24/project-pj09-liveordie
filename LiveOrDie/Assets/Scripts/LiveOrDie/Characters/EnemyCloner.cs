using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCloner : MonoBehaviour
{
    public int enemySize = 10;
    private int count = 0;
    public GameObject prefabEnemy; // game object of the enemy

    GameObject instance;
    void Clone()
    {

        Vector3 randomSpawn = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, 0));
        instance = Instantiate(prefabEnemy, randomSpawn, Quaternion.identity);
        instance.transform.SetParent(this.gameObject.transform);

    }

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        count = this.transform.childCount;
        if(count < enemySize) {
            Clone();
        }
    }
    void OnDisable()
    {
    }
}
