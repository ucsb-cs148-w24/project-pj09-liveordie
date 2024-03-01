using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public Transform Player1, Player2;
    private float ropeWidth = 0.05f; // Adjust the width of the rope
    private LineRenderer ropeRenderer;

    void OnEnable(){
        // create LineRenderer if it doesn't exist
    }
    void Start()
    {
        Player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        Player2 = GameObject.FindGameObjectWithTag("Player2").transform;
        ropeRenderer = gameObject.AddComponent<LineRenderer>();
        ropeRenderer.startWidth = ropeWidth;
        ropeRenderer.endWidth = ropeWidth;
        ropeRenderer.positionCount = 2;
        ropeRenderer.material = new Material(Shader.Find("Sprites/Default"));
        ropeRenderer.startColor = Color.grey;
        ropeRenderer.endColor = Color.grey;
        ropeRenderer.sortingOrder = 2;
        ropeRenderer.SetPosition(0, new Vector3(Player1.localPosition.x, Player1.localPosition.y + 0.9f, Player1.localPosition.z));
        ropeRenderer.SetPosition(1, new Vector3(Player2.localPosition.x, Player2.localPosition.y + 0.9f, Player2.localPosition.z));
    }
    void Update()
    {
        if(!Player1 || !Player2){
            EventMgr.Instance.EventTrigger("StartShowing");
            if(gameObject) Destroy(gameObject);
        }
        else{
            // Update Line Renderer positions to connect the players
            ropeRenderer.SetPosition(0, new Vector3(Player1.localPosition.x, Player1.localPosition.y + 0.9f, Player1.localPosition.z));
            ropeRenderer.SetPosition(1, new Vector3(Player2.localPosition.x, Player2.localPosition.y + 0.9f, Player2.localPosition.z));
        }
    }
}
