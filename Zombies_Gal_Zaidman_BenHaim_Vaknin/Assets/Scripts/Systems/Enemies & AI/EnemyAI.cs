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

    private GameObject _trapFireItem;
    private GameObject _trapIceItem;


    private GameObject _trapFireGrid;
    private GameObject _trapIceGrid;

    public Tilemap _tilemap;
    private int _hp;
    public GameObject coreTarget;
    public bool nearTarget = false;
    #endregion

    #region Public Fields
    public int EnemyId;
    public ParticleSystem hitEffect;
    public AudioClip hitMark;
    public AudioClip fireSFX;
    public AudioClip iceSFX;

    #endregion

    #region Unity Callbacks
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _trapFireGrid = GameObject.Find("FireTrapGrid");
        _trapFireItem = GameObject.Find("FireTrapGrid");

        _trapIceGrid = GameObject.Find("IceTrapGrid");
        _trapIceItem = GameObject.Find("IceTrapGrid");
        coreTarget = GameObject.Find("Core");
        //_tilemap = FindObjectOfType<Tilemap>();

        switch (EnemyId)
        {
            case 0:
                _hp = 25;
                break;

            case 1:
                _hp = 30;
                break;

            case 2:
                _hp = 35;
                break;

            default:
                _hp = 20;
                break;
        }
    }

    void Update()
    {
        //AI
        _agent.SetDestination(_target.position);
        Vector3 agentNextPos = transform.position = _agent.nextPosition;
        //AI
        if (_agent.speed <= 0)
        {
            _agent.speed = 1;
        }

        //Quaternion rotation = Quaternion.LookRotation(coreTarget.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        if (nearTarget == false)
        {
            transform.up = coreTarget.transform.position + agentNextPos;
        }



        if (_hp <= 0)
        {
            Shop.Instance.AddCoins();
            Instantiate(GameManager.Instance.deathEffect, gameObject.transform);
            FindObjectOfType<SpawnerManager>()._ZombiesInScene.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            _hp -= PlayerWeapon.Instance.BulletDmg;
            Instantiate(GameManager.Instance.hitEffect, gameObject.transform);
            AudioManager.Instance.PlayMusic(hitMark);
        }

        if (collision.gameObject.tag == "nearTarget")
        {
            nearTarget = true;
            Debug.Log("Enterd");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FireTrap")
        {
            Instantiate(GameManager.Instance.fireEffect, gameObject.transform);
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                Debug.Log(hit.point);
                hitPosition.x = hit.point.x - 0.1f;
                hitPosition.y = hit.point.y - 0.1f;
                _trapFireGrid.GetComponent<Tilemap>().SetTile(_trapFireGrid.GetComponent<Tilemap>().WorldToCell(hitPosition), null);
            }
          
            _hp -= _trapFireItem.GetComponent<TrapItem>().FireDamage;
            Debug.Log("TargetOnFire!");
            AudioManager.Instance.PlayMusic(fireSFX);
        }

        if (collision.gameObject.tag == "IceTrap")
        {
            Instantiate(GameManager.Instance.iceEffect, gameObject.transform);
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                Debug.Log(hit.point);
                hitPosition.x = hit.point.x - 0.1f;
                hitPosition.y = hit.point.y - 0.1f;
                _trapIceGrid.GetComponent<Tilemap>().SetTile(_trapIceGrid.GetComponent<Tilemap>().WorldToCell(hitPosition), null);
            }
            _agent.speed -= _trapIceItem.GetComponent<TrapItem>().slowAmount;
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log("Target Slowed!");
            AudioManager.Instance.PlayMusic(iceSFX);
        }

        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Animator>().Play("Slash");
            gameObject.GetComponent<Animator>().Play("New Animation");
            Debug.Log("ATTACK PLAYER");
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {

    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Animator>().Play("New Animation");
            Debug.Log("Resume Walking");
        }
    }

    #endregion

    #region Methods
    //void FunctionToGetRidOfTile()
    //{
    //    Vector3Int getGridPos = new Vector3Int((int)gameObject.transform.position.x, (int)gameObject.transform.position.y, (int)gameObject.transform.position.z);
    //    _tilemap.SetTile(getGridPos, null);
    //}
    #endregion
}
