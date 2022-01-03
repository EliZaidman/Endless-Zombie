using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    //AI
    private NavMeshAgent agent;
    public Transform target;
    private TilemapCollider2D _trapColider;
    //AI
    public Grid _grid;
    public Tilemap _tilemap;
    public int hp = 10;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _tilemap = FindObjectOfType<Tilemap>();
    }

    void Update()
    {
        agent.SetDestination(target.position);

        if (hp <= 0)
        {
            Shop.Instance.AddCoins();
            FindObjectOfType<SpawnerManager>()._ZombiesInScene.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            hp -= 25;

        if (collision.gameObject.CompareTag("Trap"))
        {
            FunctionToGetRidOfTile();
            hp -= 30;
        }
    }

    void FunctionToGetRidOfTile()
    {
        Vector3Int getGridPos = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
        _tilemap.SetTile(getGridPos, null);
    }
}
