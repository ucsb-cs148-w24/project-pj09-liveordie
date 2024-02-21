using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCloner : MonoBehaviour
{
    public int enemySize = 10;
    private int count = 0;

    public GameObject wolfPrefab;
    private WolfFactory wolfFactory;
    private Wolf newWolf;


    void Clone()
    {
        newWolf = wolfFactory.CreateEnemy(wolfPrefab, GetRandomSpawnPosition()) as Wolf;
        newWolf.gameObject.transform.SetParent(this.gameObject.transform);
    }

    // Start is called before the first frame update
    void Start()
    {   
        wolfFactory = WolfFactory.CreateInstance<WolfFactory>();
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

    private Vector3 GetRandomSpawnPosition() {
        // get random x and y with min radius 1 and max radius 1.3
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
        float radius = 1.2f;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        // Debug.Log(x);
        // Debug.Log(y);

        // convert viewport coordinates to world coordinates
        Vector3 randomSpawn = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));
        randomSpawn.z = 0;

        // check if the spawn position is walkable
        NavMeshHit hit;
        NavMesh.SamplePosition(randomSpawn, out hit, 60, 1 << NavMesh.GetAreaFromName("Walkable"));
        Vector3 spawnPosition = new Vector3(hit.position.x, hit.position.y, 0f) * 0.95f;     // multiply by 0.9 since spawning on the edge of the nav surface doesn't work
        // Debug.Log(spawnPosition);
        return spawnPosition;
    }
}
