using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapItem : MonoBehaviour
{
    [SerializeField]
    private CoreManager _cM;

    [SerializeField]
    private int _trapDamage = 5;

    public GameObject player;
    //public Grid _grid;
    public Tilemap _tilemap;
    //private void OnTriggerEnter(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //        Destroy(collision.gameObject);
    //    else if (collision.gameObject.tag == "Player")
    //        _cM.CoreHp -= _trapDamage;

    //    Destroy(gameObject);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //    _cM.CoreHp -= _trapDamage;

        //Destroy(gameObject);
    }


    private void Update()
    {
            FunctionToGetRidOfTile();

    }
    void FunctionToGetRidOfTile()
    {
        Vector3Int getGridPos = new Vector3Int((int)player.transform.position.x, (int)player.transform.position.y, (int)player.transform.position.z);
        _tilemap.SetTile(getGridPos, null);   
    }

    //public Vector3Int GetGridByPosition()
    //{
    //    return new Vector3Int((int)player.transform.position.x,(int)player.transform.position.y, (int)player.transform.position.z);
    //}
}
