using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyFactory
{
    // public abstract Enemy CreateEnemyAsync(Vector3 position);
    
    
    //AsyncLoading always needs a call back function to return the actual object,
    //no return in this function
    public abstract void CreateEnemyAsync( Vector3 position, UnityAction<GameObject> AfterPoolCallBack);

}
