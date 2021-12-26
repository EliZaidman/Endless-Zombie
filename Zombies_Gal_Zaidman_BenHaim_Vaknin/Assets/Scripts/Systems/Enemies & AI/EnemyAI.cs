using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    
    public Transform target;
    public int hp = 10;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.SetDestination(target.position);
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
        }
    }
}
