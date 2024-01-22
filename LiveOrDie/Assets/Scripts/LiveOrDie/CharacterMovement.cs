using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 1.5f;
    // public static bool gameStarted = false;
    // bool isPaused;
    // public bool isDead{get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = new Vector3(0, 0, 0);
        // IsDead = false;
        // gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = this.transform.position;
        if(Input.GetKey(KeyCode.LeftArrow)){
            pos.x -= Time.deltaTime * speed;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            pos.x += Time.deltaTime * speed;
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            pos.y -= Time.deltaTime * speed;
        }
        if(Input.GetKey(KeyCode.UpArrow)){
            pos.y += Time.deltaTime * speed;
        }
        this.transform.position = pos;
        Debug.Log(this.transform.position);
    }
}
