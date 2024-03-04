using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DrugDropManager : MonoBehaviour
{
    private DrugFactory drugFactory;
    private List<Player> players;
    public int numDrugs;
    private int maxDrugs;

    private enum RANDOM_EFFECTS{
        DRUNK_STATE, // Keyboard input: WASD --> SDWA & UDLR --> DLRU
        OVERKILL_STATE, // ALL WEAPONS ACTIVATED
        SENSITIVE_STATE, // 10 seconds, hitting obstacles cause players to take damage
        MAGIC_MUSHROOM_STATE, // Any player attack will hurt their peer Player instad of the wolves
        HEALTH_DROP_STATE, // Drop health by 1/2 its current
        HEALTH_BOOST_STATE, // Increase health by 1.5 OR to full health
        SLUG_STATE, // speed reduced to 1
        SPEEDY_STATE, // speed increases by 2x
        BOOST_STATE, // attack power 2x
        WEAK_STATE, // attack power halves
        WITHDRAWAL_STATE, // crazy man
    }

    private void Start()
    {
        drugFactory = new DrugFactory();
        numDrugs = 0;
        maxDrugs = 10;
        players = GetComponents<Player>().ToList();
        EventMgr.Instance.AddEventListener("DrugPicked", HandlePickedDrug);
        //event
    }

    private void Update(){
        if(numDrugs < maxDrugs){
            DropDrug();
        }
    }

    private void HandlePickedDrug(){
        numDrugs--;
        // Enforce random effect on Drugged Player
        int effect = GetRandomMysteryDrugEffect();
        switch(effect){
            case (int)RANDOM_EFFECTS.HEALTH_DROP_STATE:
                players.ForEach(p => p.DropHealthEffect());
                break;
            case (int)RANDOM_EFFECTS.HEALTH_BOOST_STATE:
                players.ForEach(p => p.BoostHealthEffect());
                break;
            case (int)RANDOM_EFFECTS.DRUNK_STATE:
                break;
            case (int)RANDOM_EFFECTS.SPEEDY_STATE:
                break;
            case (int)RANDOM_EFFECTS.BOOST_STATE:
                break;
        }
    }

    private void OnDestroy()
    {
        EventMgr.Instance.RemoveEventListener("DrugPicked", HandlePickedDrug);
    }

    private void DropDrug(){
        if(numDrugs < maxDrugs){
            numDrugs++;
            drugFactory.CreateAsync(RandomSpawnPosition(), (obj) =>
            {
                // do stuff to obj here
            });
        }
    } 
    private Vector3 RandomSpawnPosition() {
        // get random x and y with min radius 1 and max radius 1.3
        float angle = Random.Range(0f, Mathf.PI * 2);
        float radius = 0.5f;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // convert viewport coordinates to world coordinates
        Vector3 randomSpawn = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));
        randomSpawn.z = 0;

        // check if the spawn position is walkable
        NavMeshHit hit;
        NavMesh.SamplePosition(randomSpawn, out hit, 60, 1 << NavMesh.GetAreaFromName("Walkable"));
        Vector3 spawnPosition = new Vector3(hit.position.x, hit.position.y, 0f) * 0.95f;
        return spawnPosition;
    } 
    // Chooses a random effect upon drug intake
    private int GetRandomMysteryDrugEffect(){
        return Random.Range(0, 12);
    }
}
