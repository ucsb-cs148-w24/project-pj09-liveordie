using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyCloner : MonoBehaviour
{
    public int enemySize = 15;
    private int enemyCount = 0; //number of current wolves 
    
    private WolfFactory wolfFactory;
    private GhostFactory ghostFactory;

    public float difficulty; //multiplier
    public int spawnQuantity; //the number of enemies to be spawned at once
    public float spawnInterval; //the time between enemies to be spawned

    private float gameTime = 0f;

    void Start()
    {
        wolfFactory = new WolfFactory();
        ghostFactory = new GhostFactory();

        difficulty = 1;
        spawnQuantity = 1;
        spawnInterval = 5f;

        for (int i = 0; i < 5; i++)
        {
            NormalSpawn(); //clone 10 at the beginning
        }
        
        EventMgr.Instance.AddEventListener("EnemyDead", ReduceEnemyCount);
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
        // print(gameTime);
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("EnemyDead", ReduceEnemyCount);
        StopCoroutine(SpawnEnemyCoroutine());
    }
    
    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            // print(GetRandomSpawnPosition());
            NormalSpawn();

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    
    void NormalSpawn()
    {
        int randomizer = Random.Range(1,11);
        if(randomizer <= 8) {
            wolfFactory.CreateAsync(GetRandomSpawnPosition(), (wolf) =>
            {
                //use reference 'wolf' here if you want to use the wolf spawned
                enemyCount++;
                wolf.transform.parent = transform;
            });
        }
        else {
            ghostFactory.CreateAsync(GetRandomSpawnPosition(), (ghost) =>
            {
                enemyCount++;
                ghost.transform.parent = transform;
            });
        }
    }

    private void GhostFrenzy()
    {
        
    }
    
    
    private Vector3 GetRandomSpawnPosition() {
        float x, y;
        float offset = 5f;
        Vector3 randomSpawn = Vector3.zero;
        Vector3 spawnPosition = Vector3.zero;
        bool isHit = false;
        // if SamplePosition fails, try again
        // while(!isHit)
        // {
            int side = Random.Range(0, 4); //decide with side of the screen to spawn
            switch (side)
            {
                case 0: //left
                    x = 0;
                    y = Random.Range(0f, Screen.height);
                    randomSpawn = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                    randomSpawn.x -= offset;
                    break;
                case 1: //right
                    x = Screen.width;
                    y = Random.Range(0f, Screen.height);
                    randomSpawn = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                    randomSpawn.x += offset;
                    break;
                case 2: //top
                    y = Screen.height;
                    x = Random.Range(0f, Screen.width);
                    randomSpawn = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                    randomSpawn.y += offset;
                    break;
                case 3: //bottom
                    y = 0;
                    x = Random.Range(0f, Screen.width);
                    randomSpawn = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
                    randomSpawn.y -= offset;
                    break;

            }

            // check if the spawn position is walkable
            NavMeshHit hit;
             isHit = NavMesh.SamplePosition(randomSpawn, out hit, 5f, 1 << NavMesh.GetAreaFromName("Walkable"));
            
             if(isHit){spawnPosition = new Vector3(hit.position.x, hit.position.y, 0f);}
            print(isHit);
        // }

        return spawnPosition;

    }
    
    private void ReduceEnemyCount()
    {
        enemyCount--;
    }
    
    
    
    
}
