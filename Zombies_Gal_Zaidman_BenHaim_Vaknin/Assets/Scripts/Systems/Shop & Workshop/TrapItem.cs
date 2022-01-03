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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Enemy")
        //{
        //    Debug.Log("INSIDEeeeeeeeeeeeeeeeeeeee");
        //    collision.gameObject.GetComponent<EnemyAI>().hp -= 30;
        //    Destroy(collision.gameObject);
        //}

    }

    private void Start()
    {
        
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
    
    public Vector3Int GetGridByPosition()
    {
        return new Vector3Int((int)gameObject.transform.position.x,(int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
    }


}
