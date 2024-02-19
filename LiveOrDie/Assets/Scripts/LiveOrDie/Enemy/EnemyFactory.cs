using UnityEngine;

public abstract class EnemyFactory : ScriptableObject
{   
    public abstract Enemy CreateEnemy(GameObject prefab, Vector3 position);

}
