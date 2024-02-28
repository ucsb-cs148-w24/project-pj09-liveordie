using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCloner : MonoBehaviour
{
    public int enemySize = 10;
    private int wolfCount = 0; //number of current wolves 
    
    private WolfFactory wolfFactory;
    private Wolf newWolf;


    void Clone()
    {
        wolfFactory.CreateAsync(GetRandomSpawnPosition(), (wolf) =>
        {
            //use reference 'wolf' here if you want to use the wolf spawned
            wolfCount++;
            wolf.transform.parent = transform;
        });

    }
    
    void Start()
    {
        wolfFactory = new WolfFactory();
        
        //***********************Any counting function should be moved into a centralized mgr later***************
        EventMgr.Instance.AddEventListener("WolfDead", ReduceWolfCount);
        EventMgr.Instance.AddEventListener("WolfDead", CheckIfClone);

        for (int i = 0; i < 10; i++)
        {
            Clone(); //clone 10 at the beginning
        }
    }
    

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("WolfDead", ReduceWolfCount);
        EventMgr.Instance.RemoveEventListener("WolfDead", CheckIfClone);
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

    //***********************Any counting function should be moved into a centralized mgr later***************
    private void ReduceWolfCount()
    {
        wolfCount--;
    }

    //*******************Any clone rule related functions should be moved into a centralized mgr later***************
    private void CheckIfClone()
    {
        if(wolfCount < enemySize) {  //can add some sort of delay here using Invoke
            Invoke(nameof(Clone), 5f);
        }
    }
    
    
}
