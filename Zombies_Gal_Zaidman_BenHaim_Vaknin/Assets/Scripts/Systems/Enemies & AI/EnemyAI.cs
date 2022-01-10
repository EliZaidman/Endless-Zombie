using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private Transform _target;
    #endregion

    #region Fields
    private NavMeshAgent _agent;
    private GameObject _trapItem;
    private Tilemap _tilemap;
    private int _hp;
    #endregion

    #region Public Fields
    public int EnemyId;
    public ParticleSystem hitEffect;
    public AudioClip hitMark;
    
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _trapItem = GameObject.Find("Trap");
        _tilemap = FindObjectOfType<Tilemap>();

        switch (EnemyId)
        {
            case 0:
                _hp = 10;
                break;

            case 1:
                _hp = 25;
                break;

            case 2:
                _hp = 50;
                break;

            default:
                _hp = 20;
                break;
        }
    }

    void Update()
    {
        _agent.SetDestination(_target.position);
        transform.position = _agent.nextPosition;

        if (_hp <= 0)
        {
            Shop.Instance.AddCoins();
            Instantiate(GameManager.Instance.deathEffect, gameObject.transform);
            FindObjectOfType<SpawnerManager>()._ZombiesInScene.Remove(gameObject);
            Destroy(gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


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

            _hp -= _trapItem.GetComponent<TrapItem>().FireDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {   
            _hp -= PlayerWeapon.Instance.BulletDmg;
            
            Destroy(collision.gameObject);
            Instantiate(GameManager.Instance.hitEffect, gameObject.transform);
            AudioManager.Instance.PlayMusic(hitMark);
        }

        #endregion
    }
    #region Methods
    void FunctionToGetRidOfTile()
    {
        Vector3Int getGridPos = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
        _tilemap.SetTile(getGridPos, null);
    }
    #endregion
}
