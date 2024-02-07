using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public Transform Player1, Player2;
    public float ropeWidth = 0.05f; // Adjust the width of the rope
    private LineRenderer ropeRenderer;
    // public Color c = Color.grey;
    // private SpringJoint2D spring; --> Worry about this later
    // Start is called before the first frame update

    void OnEnable(){
        // create LineRenderer if it doesn't exist
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
        // spring = this.GetComponent<SpringJoint2D>();
        // spring.distance = 5.0f;
        // spring.dampingRatio = 0.9f;
        // spring.frequency = 0.8f;

        ropeRenderer.SetPosition(0, Player1.position);
        ropeRenderer.SetPosition(1, Player2.position);
    }
    void Start()
    {

    }
    void Update()
    {
        // Update Line Renderer positions to connect the players
        ropeRenderer.SetPosition(0, Player1.position);
        ropeRenderer.SetPosition(1, Player2.position);
    }
}
