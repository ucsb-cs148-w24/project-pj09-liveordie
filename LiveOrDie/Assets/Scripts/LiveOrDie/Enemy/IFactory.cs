using UnityEngine;
using UnityEngine.Events;

public interface IFactory
{
    // public abstract Enemy CreateEnemyAsync(Vector3 position);
    
    
    //AsyncLoading always needs a call back function to return the actual object,
    //no return in this function
    public abstract void CreateAsync( Vector3 position, UnityAction<GameObject> AfterPoolCallBack);

}
