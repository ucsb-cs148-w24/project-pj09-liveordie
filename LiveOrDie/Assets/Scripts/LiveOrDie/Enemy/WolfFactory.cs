using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfFactory : EnemyFactory
{

    public override Enemy CreateEnemy(GameObject prefab, Vector3 position) {
        GameObject wolfInstance = Instantiate(prefab, position, Quaternion.identity);
        Wolf newWolf = wolfInstance.GetComponent<Wolf>();
        newWolf.Initialize();
        return newWolf;
    }
}