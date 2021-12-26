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

    }
    private void FixedUpdate()
    {
        FunctionToGetRidOfTile();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 25;

            if (hp <= 0)
            {
                
                FindObjectOfType<SpawnerManager>()._ZombiesInScene.Remove(gameObject);
                Destroy(gameObject);
                

            }
            Shop.Instance.AddCoins();
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            hp -= 30;
            Destroy(collision.gameObject);
        }
    }

    void FunctionToGetRidOfTile()
    {

        Vector3Int getGridPos = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
        _tilemap.SetTile(getGridPos, null);
        
    }


}
