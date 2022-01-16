using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TrapItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private Tilemap _tilemap;

    public int TrapId, FireDamage = 30, slowAmount = 1, BearTrapDamage = 15, FlameTrapDamage = 30;

    
    void FunctionToGetRidOfTile()
    {
        Vector3Int getGridPos = new Vector3Int((int)_player.transform.position.x, (int)_player.transform.position.y, (int)_player.transform.position.z);
        _tilemap.SetTile(getGridPos, null);   
    }
    
    public Vector3Int GetGridByPosition()
    {
        return new Vector3Int((int)gameObject.transform.position.x,(int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
    }
}
