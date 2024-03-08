using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Transform obj1, obj2;
    public float smoothTime = 0.5f;
    private Vector2 centerPoint;
    private Vector2 velocity;

    // Update is called once per frame
    void Update()
    {
        if(obj1 && obj2){
            centerPoint = (obj1.position + obj2.position) / 2f;
            transform.position = Vector2.SmoothDamp(transform.position, centerPoint, ref velocity, smoothTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}