using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyCloner : MonoBehaviour
{
    public int enemySize = 15;  //number of max enemies
    private int enemyCount = 0; //number of current enemies 
    private int dragonCount = 0; //number of dragons
    
    private WolfFactory wolfFactory;
    private GhostFactory ghostFactory;
    private DragonFactory dragonFactory;

    public float difficulty; //multiplier
    public int spawnQuantity; //the number of enemies to be spawned at once
    public float spawnInterval; //the time between enemies to be spawned

    public float eliteMultiplier = 10f;

    void Start()
    {
        wolfFactory = new WolfFactory();
        ghostFactory = new GhostFactory();
        dragonFactory = new DragonFactory();

        difficulty = 1;
        spawnQuantity = 1;
        spawnInterval = 5f;

        for (int i = 0; i < 10; i++)
        {
            NormalSpawn(); //clone 10 at the beginning
        }
        
        EventMgr.Instance.AddEventListener("EnemyDead", ReduceEnemyCount);
        EventMgr.Instance.AddEventListener("DragonDead", ReduceDragonCount);
        StartCoroutine(SpawnEnemyCoroutine());
        StartCoroutine(DifficultyUp());
        StartCoroutine(SpawnEliteCoroutine());
        StartCoroutine(SpawnDragonCoroutine());
    }
    

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("EnemyDead", ReduceEnemyCount);
        EventMgr.Instance.RemoveEventListener("DragonDead", ReduceDragonCount);
        StopCoroutine(SpawnEnemyCoroutine());
        StopCoroutine(DifficultyUp());
        StopCoroutine(SpawnEliteCoroutine());
        StopCoroutine(SpawnDragonCoroutine());
    }
    
    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            if (enemyCount < enemySize)
            {
                NormalSpawn();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator DifficultyUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            difficulty += 0.2f;
            spawnInterval *= 0.95f;
            spawnQuantity += 1;
            enemySize += 5;
        }
    }

    private IEnumerator SpawnEliteCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(90f);
            EliteSpawn();
        }
    }
    
    private IEnumerator SpawnDragonCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(300f);
            if (dragonCount < 1) //spawn when there is no dragon, if play cannot kill it, they dont get a new one in 5 min
            {
                DragonSpawn();
            }
        }
    }
    

    
    void NormalSpawn()
    {
        int randomizer = Random.Range(0,10);
        if(randomizer < 8) {
            wolfFactory.CreateAsync(GetRandomSpawnPosition(), (wolf) =>
            {
                //use reference 'wolf' here if you want to use the wolf spawned
                enemyCount++;
                wolf.transform.parent = transform;
                Wolf wolfScript = wolf.GetComponent<Wolf>();
                wolfScript.health *=  difficulty;
                wolfScript.maxHealth *=  difficulty;
            });
        }
        else {
            ghostFactory.CreateAsync(GetRandomSpawnPosition(), (ghost) =>
            {
                enemyCount++;
                ghost.transform.parent = transform;
                Ghost ghostScript = ghost.GetComponent<Ghost>();
                ghostScript.health *=  difficulty;
                ghostScript.maxHealth *=  difficulty;
            });
        }
    }

    private void EliteSpawn()
    {
        int randomizer = Random.Range(0,10);
        if(randomizer < 5) {
            wolfFactory.CreateAsync(GetRandomSpawnPosition(), (wolf) =>
            {
                //use reference 'wolf' here if you want to use the wolf spawned
                enemyCount++;
                wolf.transform.parent = transform;
                Wolf wolfScript = wolf.GetComponent<Wolf>();
                wolfScript.health *= eliteMultiplier * difficulty;
                wolfScript.maxHealth *= eliteMultiplier * difficulty;
                wolfScript.dropEvent = "DropBigExp";
                wolf.transform.localScale *= 2;
            });
        }
        else {
            ghostFactory.CreateAsync(GetRandomSpawnPosition(), (ghost) =>
            {
                enemyCount++;
                ghost.transform.parent = transform;
                Ghost ghostScript = ghost.GetComponent<Ghost>();
                ghostScript.health *= eliteMultiplier * difficulty;
                ghostScript.maxHealth *= eliteMultiplier * difficulty;
                ghostScript.dropEvent = "DropBigExp";
                ghost.transform.localScale *= 2;
            });
        }
    }

    private void DragonSpawn()
    {
        dragonFactory.CreateAsync(GetRandomSpawnPosition(), (dragon) =>
        {
            dragonCount++;
            dragon.transform.parent = transform;
            Dragon dragonScript = dragon.GetComponent<Dragon>();
            dragonScript.health *=  difficulty;
            dragonScript.maxHealth *=  difficulty;
        });
    }
    
    
    
    private Vector3 GetRandomSpawnPosition() {
        float x, y;
        float offset = 5f;
        Vector3 randomSpawn = Vector3.zero;
        Vector3 spawnPosition = Vector3.zero;
        bool isHit = false;
        // if SamplePosition fails, try again
        while(!isHit)
        {
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

            randomSpawn.z = 0; //must do this otherwise after screen to world transform its z is the same as camera
            // check if the spawn position is walkable
            NavMeshHit hit;
             isHit = NavMesh.SamplePosition(randomSpawn, out hit, 5f, 1 << NavMesh.GetAreaFromName("Walkable"));
            
             if(isHit){spawnPosition = new Vector3(hit.position.x, hit.position.y, 0f);}
             
        }
        
        return spawnPosition;

    }
    
    private void ReduceEnemyCount()
    {
        enemyCount--;
    }

    private void ReduceDragonCount()
    {
        dragonCount--;
    }
    
    
    
    
}
