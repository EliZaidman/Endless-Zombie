using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public  class CoreManager : MonoBehaviour
{
    private static CoreManager _instance;
    public static CoreManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Core Manager not loaded properly");

            return _instance;
        }
    }

    [SerializeField]
    private SpawnerManager _sM;

    [SerializeField]
    TextMeshProUGUI tmpCoreHp;
    
    EnemyAI enemyAI;
    
    public int CoreHp = 20;
    public int CoreMaxHp = 20;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    void Update()
    {
        tmpCoreHp.text = CoreHp.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CoreHp -= 5;
            _sM._ZombiesInScene.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
