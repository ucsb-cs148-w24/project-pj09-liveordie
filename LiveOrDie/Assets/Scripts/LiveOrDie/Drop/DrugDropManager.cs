using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class DrugDropManager : MonoBehaviour
{
    private DrugFactory drugFactory; 
    private List<Player> players; // list referencing players so we can directly give it "effects"
    public int numDrugs, maxDrugs; // counter | max # to spawn
    private float targetTime; // startDelay | random Time to spawn
    private bool drugged; // whether under drug effect
    public float effectTime; // amount of time left until drug wears off
    private enum RANDOM_EFFECTS{
        HEALTH_DROP_STATE, // Drop health by 1/2 its current
        HEALTH_BOOST_STATE, // Increase health by 1.5 OR to full health
        SLUG_STATE, // speed reduced to 1
        SPEEDY_STATE, // speed increases by 2x
        DRUNK_STATE, // Keyboard input: WASD --> SDWA & UDLR --> DLRU
        SENSITIVE_STATE, // 10 seconds, hitting obstacles cause players to take damage

        ////////////////////////////// ANYTHING BELOW HAS YET TO BE IMPLEMENTED
        MAGIC_MUSHROOM_STATE, // Any player attack will hurt their peer Player
        BOOST_STATE, // attack power 2x
        WEAK_STATE, // attack power halves
        WITHDRAWAL_STATE, // crazy man, loose control of keyboard control
    }

    private void Start()
    {
        drugFactory = new DrugFactory();
        numDrugs = 0;
        maxDrugs = 10;
        targetTime = 5;
        effectTime = 5f;
        players = new List<Player>() {
            GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>(),
            GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>()
        };
        EventMgr.Instance.AddEventListener("DrugPicked", HandlePickedDrug);
        //event
    }

    private void Update(){
        targetTime -= Time.deltaTime; // update time for random drug spawn
        if (targetTime <= 0 && numDrugs < maxDrugs){
            DropDrug();
            targetTime = UnityEngine.Random.Range(10, 20);
        }
        if(drugged){ // if drugged, update time that it wears off 
            effectTime -= Time.deltaTime;
            if(effectTime <= 0){
                players.ForEach( p => {
                    p.ResetCharacteristics();
                });
                drugged = false;
                effectTime = 5f;
            }
        }
    }
    private void HandlePickedDrug(){
        if(!drugged){
            drugged = true;
            numDrugs--;
            // Enforce random effect on Drugged Player --> Range [0-6] for now, but will expand
            int effect = UnityEngine.Random.Range(0, 6); 
            switch(effect){
                case (int)RANDOM_EFFECTS.HEALTH_DROP_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "DOOM! Health Halved!");
                    players.ForEach(p => p.EnforceHealthEffect("drop"));
                    break;
                case (int)RANDOM_EFFECTS.HEALTH_BOOST_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "ENHANCE! Health Boost!");
                    players.ForEach(p => p.EnforceHealthEffect("boost"));
                    break;
                case (int)RANDOM_EFFECTS.SPEEDY_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "ENHANCE! Speed Boost!");
                    players.ForEach(p => p.EnforceSpeedEffect("boost"));
                    break;
                case (int)RANDOM_EFFECTS.SLUG_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "You're a slug");
                    players.ForEach(p => p.EnforceSpeedEffect("drop"));
                    break;
                case (int)RANDOM_EFFECTS.DRUNK_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "Your a drunkard");
                    players.ForEach(p => 
                    {
                        p.EnforceDrunkEffect(true);
                        p.EnforceSpeedEffect("berzerkers");
                    });
                    break;
                case (int)RANDOM_EFFECTS.SENSITIVE_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "You're sensitive");
                    players.ForEach(p => p.EnforceSensitiveState(true));
                    break;
                default:
                    break;
            }
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
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2);
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
}
