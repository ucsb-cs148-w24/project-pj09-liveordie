using UnityEngine;
using UnityEngine.AI;

public class EnemyCloner : MonoBehaviour
{
    public int enemySize = 15;
    private int enemyCount = 0; //number of current wolves 
    
    private WolfFactory wolfFactory;
    private Wolf newWolf;

    private GhostFactory ghostFactory;
    private Ghost newGhost;

    int randomizer;

    void Clone()
    {
        randomizer = UnityEngine.Random.Range(1,11);
        // randomizer = 10;
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
    
    void Start()
    {
        wolfFactory = new WolfFactory();
        
        //***********************Any counting function should be moved into a centralized mgr later***************
        EventMgr.Instance.AddEventListener("WolfDead", ReduceEnemyCount);
        EventMgr.Instance.AddEventListener("WolfDead", CheckIfClone);

        ghostFactory = new GhostFactory();

        for (int i = 0; i < 10; i++)
        {
            Clone(); //clone 10 at the beginning
        }
    }
    

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("WolfDead", ReduceEnemyCount);
        EventMgr.Instance.RemoveEventListener("WolfDead", CheckIfClone);
    }

    private Vector3 GetRandomSpawnPosition() {
        float x = 0, y = 0;
        float offset = 5;
        Vector3 randomSpawn = Vector3.zero;
        Vector3 spawnPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, 0);
        // if SamplePosition fails, try again
        while (float.IsInfinity(spawnPosition.x) || float.IsInfinity(spawnPosition.y))
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
            

            // check if the spawn position is walkable
            NavMeshHit hit;
            NavMesh.SamplePosition(randomSpawn, out hit, 0.1f, 1 << NavMesh.GetAreaFromName("Walkable"));
            
            spawnPosition = new Vector3(hit.position.x, hit.position.y, 0f);
        }

        return spawnPosition;
    }

    //***********************Any counting function should be moved into a centralized mgr later***************
    private void ReduceEnemyCount()
    {
        enemyCount--;
    }

    //*******************Any clone rule related functions should be moved into a centralized mgr later***************
    private void CheckIfClone()
    {
        if(enemyCount < enemySize) {  //can add some sort of delay here using Invoke
            Invoke(nameof(Clone), 5f);
        }
    }
    
    
}
