using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapItem : MonoBehaviour
{
    [SerializeField]
    private CoreManager _cM;

    public GameObject player;
    //public Grid _grid;
    public Tilemap _tilemap;

    public int trapHP;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    Tilemap tilemap = GetComponent<Tilemap>();
        //    Vector3 hitPosition = Vector3.zero;
        //    foreach (ContactPoint2D hit in collision.contacts)
        //    {
        //        Debug.Log(hit.point);
        //        hitPosition.x = hit.point.x - 0.1f;
        //        hitPosition.y = hit.point.y - 0.1f;
        //        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
        //    }
        //    Destroy(collision.gameObject);
        //}
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        //FunctionToGetRidOfTile();
    }

    void FunctionToGetRidOfTile()
    {
        Vector3Int getGridPos = new Vector3Int((int)player.transform.position.x, (int)player.transform.position.y, (int)player.transform.position.z);
        _tilemap.SetTile(getGridPos, null);   
    }
    
    public Vector3Int GetGridByPosition()
    {
        return new Vector3Int((int)gameObject.transform.position.x,(int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
    }


}
