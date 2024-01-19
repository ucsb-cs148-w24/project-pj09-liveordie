using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tiles_Script : MonoBehaviour
{

    public Tile tile1 ;
    public Tile tile2 ;

    private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = gameObject.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.down);

        if(hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Left Click");
                Vector3Int tilePos = tilemap.WorldToCell(hit.point);
                Tile tile = tilemap.GetTile<Tile>(tilePos);
                if (tile == tile1)
                {
                    tilemap.SetTile(tilePos, tile2);
                }
                else
                {
                    tilemap.SetTile(tilePos, tile1);
                }
            }
        }
    }
}
