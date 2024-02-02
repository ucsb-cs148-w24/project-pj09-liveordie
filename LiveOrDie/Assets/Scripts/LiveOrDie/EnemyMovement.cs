using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1.5f; // move speed of enemies
    public Transform target;
    public List<Transform> characters; // list of character to pick for target
    private int chooseTarget;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        characters.Add(GameObject.FindGameObjectWithTag("Player1").transform);
        characters.Add(GameObject.FindGameObjectWithTag("Player2").transform);
        chooseTarget = Random.Range(0, characters.Count);
        target = characters[chooseTarget];
    }

    void OnDisable()
    {
        characters.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // Debug.Log(characters[0].position);
        // Debug.Log(characters[1].position);
    }

    private void Move()
    {
        //float distance1 = Vector2.Distance (transform.position, target1.position);
        //float distance2 = Vector2.Distance (transform.position, target2.position);
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
