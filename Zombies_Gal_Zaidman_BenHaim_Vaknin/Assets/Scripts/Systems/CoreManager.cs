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
    public TextMeshProUGUI CoreHpTxt;

    public GameObject GameOver;
    public int CoreHp = 20;
    public int CoreMaxHp = 20;

    public AudioClip hitCoreSound;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CoreHp -= 5;
            AudioManager.Instance.Play(hitCoreSound);
            _sM._ZombiesInScene.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
