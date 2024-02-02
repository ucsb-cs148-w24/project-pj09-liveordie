using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public Transform Player1, Player2;
    public float ropeWidth = 0.05f; // Adjust the width of the rope
    private LineRenderer ropeRenderer;
    private HingeJoint ropeHingeJoint;
    public Color c = Color.white;
    private Rigidbody p1;
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
        ropeRenderer.SetColors(c, c);

        // create HingeJoint if it doesn't exist
        ropeHingeJoint = gameObject.GetComponent<HingeJoint>();
        if(ropeHingeJoint == null){
            ropeHingeJoint = gameObject.AddComponent<HingeJoint>();
        }

        // attach hingeJoint to Player1
        ropeHingeJoint.connectedBody = Player1.GetComponent<Rigidbody>();
        p1 = Player1.GetComponent<Rigidbody>();
        ropeHingeJoint.axis = Vector3.forward; // Z-axis rotation

        ropeRenderer.SetPosition(0, Player1.position);
        ropeRenderer.SetPosition(1, Player2.position);
    }
    void Start()
    {

    }
    void Update()
    {
        // Update HingeJoint's connected body based on player positions
        ropeHingeJoint.connectedBody = p1;

        // Update Line Renderer positions to connect the players
        ropeRenderer.SetPosition(0, Player1.position);
        ropeRenderer.SetPosition(1, Player2.position);
    }
}
