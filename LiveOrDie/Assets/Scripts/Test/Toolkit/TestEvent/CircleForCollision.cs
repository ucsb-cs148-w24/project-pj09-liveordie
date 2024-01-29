using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Circle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.collider.gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
