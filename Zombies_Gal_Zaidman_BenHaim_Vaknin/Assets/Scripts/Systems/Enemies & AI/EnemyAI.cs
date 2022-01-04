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
    public GameObject _trapItem;
    public GameObject trapGrid;
    public Tilemap _tilemap;
    public int hp = 10;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _tilemap = FindObjectOfType<Tilemap>();
        _trapItem = GameObject.Find("Trap");
    }

    void Update()
    {
        agent.SetDestination(target.position);
        transform.position = agent.nextPosition;
        if (hp <= 0)
        {
            Shop.Instance.AddCoins();
            FindObjectOfType<SpawnerManager>()._ZombiesInScene.Remove(gameObject);
            Destroy(gameObject);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
            hp -= 25;

        if (collision.gameObject.tag == "Trap")
        {
            
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                Debug.Log(hit.point);
                hitPosition.x = hit.point.x - 0.1f;
                hitPosition.y = hit.point.y - 0.1f;
                _tilemap.SetTile(_tilemap.WorldToCell(hitPosition), null);
            }
            hp -= _trapItem.GetComponent<TrapItem>().trapHP;
        }

    }

    void FunctionToGetRidOfTile()
    {
        Vector3Int getGridPos = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);

        _tilemap.SetTile(getGridPos, null);


    }
}
