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
    //public Grid _grid;
    public GameObject trapGrid;
    public Tilemap _tilemap;
    public int hp = 10;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _tilemap = FindObjectOfType<Tilemap>();
        trapGrid = GameObject.Find("Trap");
    }

    void Update()
    {
        agent.SetDestination(target.position);
        transform.position = agent.nextPosition;

       // 
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


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            Debug.Log("INSIDEeeeeeeeeeeeeeeeeeeee");
            hp -= trapGrid.GetComponent<TrapItem>().trapHP;
            FunctionToGetRidOfTile();
            Destroy(collision.gameObject);
        }

    }
    void FunctionToGetRidOfTile()
    {
        Vector3Int getGridPos = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);

        _tilemap.SetTile(getGridPos, null);


    }

    
}
