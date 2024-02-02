using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.5f; // move speed of enemies
    private GameObject target;
    private int chooseTarget;
    private SpriteRenderer render;
    void Start()
    {

    }

    void OnEnable()
    {
        render = this.GetComponent<SpriteRenderer>();
        chooseTarget = UnityEngine.Random.Range(0, 2);
        if (chooseTarget == 0) target = GameObject.FindGameObjectWithTag("Player1");
        else target = GameObject.FindGameObjectWithTag("Player2");
        if(chooseTarget == 0) render.color = Color.red;
        else render.color = Color.blue;
    }

    void OnDisable()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        TrackDistance();
        Vector3 direction = (target.transform.position - transform.position).normalized;
        this.transform.position += direction * speed * Time.deltaTime;
    }

    private void TrackDistance()
    {
        float dist = Vector3.Distance(target.transform.position, transform.position);
        if(dist <= 0.1){
            OnDisable();
        }
    }
}
