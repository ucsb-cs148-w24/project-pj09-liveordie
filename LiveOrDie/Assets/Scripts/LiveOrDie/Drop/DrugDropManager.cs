using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class DrugDropManager : MonoBehaviour
{
    private DrugFactory drugFactory; 
    private TalismanFactory talismanFactory;
    private List<Player> players; // list referencing players so we can directly give it "effects"
    
    [HideInInspector]
    public int numSpawn, maxSpawn; // counter | max # to spawn
    [HideInInspector]
    public float effectTime, targetTime; // time left before wearoff & random Time to spawn
    private bool drugged; // T/F under drug effect
    private enum RANDOM_EFFECTS{
        HEALTH_DROP_STATE, // Halves health
        HEALTH_BOOST_STATE, // Full health
        SLUG_STATE, // halfs speed
        SPEEDY_STATE, // 1.5*speed
        DRUNK_STATE, // erratic movement
        SENSITIVE_STATE, // enemy attacks double
        MAGIC_MUSHROOM_STATE, // player attacks reduce experience level
        REINCARNATION, // remove all modifiers --> everything resets to level 1 player properties
        DOPPLE_GANGER, // players becomes enemies
        ENCHANTMENT, // 1 type of enemy prefab can now hurt other enemies
        NAUSEA, // Earthquake! camera is shaking!
    }
    private enum TO_IMPLEMENT{
        HALF_BLINDNESS, // only see half of the screen
        TOTAL_SOLAR_ECLIPSE, // brightness dims
        BLUE_MOON, // Wolf attacks are tripled in damage
        NULLIFY, // Weapons don't do any damage
    }
    private void Start()
    {
        drugFactory = new DrugFactory();
        talismanFactory = new TalismanFactory();
        drugged = false;
        numSpawn = 0;
        maxSpawn = 10;
        targetTime = 5f;
        effectTime = UnityEngine.Random.Range(10, 20);
        players = new List<Player>() {
            GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>(),
            GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>()
        };
        EventMgr.Instance.AddEventListener("DrugPicked", HandlePickedDrug);
        //event
    }
    private void Update(){
        targetTime -= Time.deltaTime; // update time for random drug spawn
        if (targetTime <= 0 && numSpawn < maxSpawn){
            DropDrug();
            targetTime = UnityEngine.Random.Range(10, 20);
        }
        if(drugged){ // if drugged, update time that it wears off 
            effectTime -= Time.deltaTime;
            if(effectTime <= 0){
                players.ForEach( p => p.ResetCharacteristics());
                EventMgr.Instance.EventTrigger("Nausea", false);
                effectTime = UnityEngine.Random.Range(5, 10);
                drugged = false;
            }
        }
    }
    private void HandlePickedDrug(){
        if(!drugged){
            drugged = true; numSpawn--;
            // Enforce random effect on Drugged Player --> Range [0-7]
            int effect = 10; // UnityEngine.Random.Range(0, 6); 
            switch(effect){
                case (int)RANDOM_EFFECTS.HEALTH_DROP_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "Half Health");
                    EventMgr.Instance.EventTrigger("TimeText", 5f);
                    effectTime = 1f;
                    players.ForEach(p => p.EnforcePlayerEffect("drop health"));
                    break;
                case (int)RANDOM_EFFECTS.HEALTH_BOOST_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "Health Boost");
                    EventMgr.Instance.EventTrigger("TimeText", 5f);
                    effectTime = 1f;
                    players.ForEach(p => p.EnforcePlayerEffect("boost health"));
                    break;
                case (int)RANDOM_EFFECTS.SPEEDY_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "Speed Boost");
                    EventMgr.Instance.EventTrigger("TimeText", effectTime);
                    players.ForEach(p => p.EnforcePlayerEffect("boost speed"));
                    break;
                case (int)RANDOM_EFFECTS.SLUG_STATE:
                    EventMgr.Instance.EventTrigger("DrugText", "Slug Speed");
                    EventMgr.Instance.EventTrigger("TimeText", effectTime);
                    players.ForEach(p => p.EnforcePlayerEffect("drop speed"));
                    break;
                case (int)RANDOM_EFFECTS.DRUNK_STATE:
                    EventMgr.Instance.EventTrigger("TimeText", effectTime);
                    EventMgr.Instance.EventTrigger("DrugText", "Drunk Mode");
                    players.ForEach(p => p.EnforcePlayerEffect("drunk") );
                    break;
                case (int)RANDOM_EFFECTS.SENSITIVE_STATE:
                    EventMgr.Instance.EventTrigger("TimeText", effectTime);
                    EventMgr.Instance.EventTrigger("DrugText", "Double Damage");
                    players.ForEach(p => p.EnforcePlayerEffect("sensitive"));
                    break;
                case (int)RANDOM_EFFECTS.MAGIC_MUSHROOM_STATE:
                    EventMgr.Instance.EventTrigger("TimeText", effectTime);
                    EventMgr.Instance.EventTrigger("DrugText", "Magic Mushroom");
                    EventMgr.Instance.EventTrigger("MagicMushroom", true);
                    break;
                case(int)RANDOM_EFFECTS.NAUSEA:
                    EventMgr.Instance.EventTrigger("Nausea", true);
                    EventMgr.Instance.EventTrigger("TimeText", effectTime);
                    EventMgr.Instance.EventTrigger("DrugText", "Earthquake!");
                    players.ForEach(p => p.EnforcePlayerEffect("nausea"));
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
        if(numSpawn < maxSpawn){
            numSpawn++;
            int prefab = UnityEngine.Random.Range(0, 2); // random choose between talisman & drug
            switch (prefab){
                case 0:
                    drugFactory.CreateAsync(RandomSpawnPosition(), (obj) =>
                    {
                        // do stuff to obj here
                    });
                    break;
                case 1:
                    talismanFactory.CreateAsync(RandomSpawnPosition(), (obj) =>
                    {
                        // do stuff to obj here
                    });
                    break;
                default:
                    break;
            }
            
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
