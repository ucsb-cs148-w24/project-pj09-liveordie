using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public Transform Player1, Player2;
    public float ropeWidth = 0.05f; // Adjust the width of the rope
    private LineRenderer ropeRenderer;
    private HingeJoint ropeHingeJoint;
    // private Color ropeColor = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        // create LineRenderer if it doesn't exist
        ropeRenderer = GetComponent<LineRenderer>();
        if(ropeRenderer == null){
            ropeRenderer = gameObject.AddComponent<LineRenderer>();
        }

        ropeRenderer.startWidth = ropeWidth;
        ropeRenderer.endWidth = ropeWidth;
        ropeRenderer.positionCount = 2;

        Material lineMaterial = new Material(Shader.Find("Unlit/Color")); // You can use a different shader if needed
        lineMaterial.color = Color.white;
        ropeRenderer.material = lineMaterial;

        // create HingeJoint if it doesn't exist
        ropeHingeJoint = gameObject.GetComponent<HingeJoint>();
        if(ropeHingeJoint == null){
            ropeHingeJoint = gameObject.AddComponent<HingeJoint>();
        }

        // attach hingeJoint to Player1
        ropeHingeJoint.connectedBody = Player1.GetComponent<Rigidbody>();
        ropeHingeJoint.axis = Vector3.forward; // Z-axis rotation

        ropeRenderer.SetPosition(0, Player1.position);
        ropeRenderer.SetPosition(1, Player2.position);
        ropeRenderer.sortingOrder = 2;
    }
    void Update()
    {
        // Update HingeJoint's connected body based on player positions
        ropeHingeJoint.connectedBody = Player1.GetComponent<Rigidbody>();

        // Update Line Renderer positions to connect the players
        ropeRenderer.SetPosition(0, Player1.position);
        ropeRenderer.SetPosition(1, Player2.position);
    }
}
