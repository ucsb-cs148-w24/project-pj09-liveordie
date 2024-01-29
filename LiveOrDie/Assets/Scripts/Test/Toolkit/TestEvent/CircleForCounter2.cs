using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleForCounter2 : MonoBehaviour
{
    private string info = "info from circle 2";
    private void OnCollisionEnter2D(Collision2D other)
    {
        EventMgr.Instance.EventTrigger("HitCube"); 
        EventMgr.Instance.EventTrigger("HitCubeWithInfo", info);
    }
}
