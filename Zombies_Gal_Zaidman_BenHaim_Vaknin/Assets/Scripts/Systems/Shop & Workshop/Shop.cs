using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    private static Shop _instance;
    public static Shop Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Shop Manager not loaded properly");

            return _instance;
        }
    }

    //[SerializeField]

    [SerializeField]
    private GameObject _shopOverlay;
    
    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    private Collider2D _shopCol, _playerCol;

    [SerializeField]
    private float _inOverlayMapSize = 14f, _defaultMapSize;

    private int _coinsToPlayer;

    public TextMeshProUGUI CoinsText;
    public int MachineGunPrice = 200, CannonPrice = 50, PotionPrice = 5, Coins = 0;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsWaveOngoing && collision.collider == _playerCol)
        {
            _shopOverlay.SetActive(true);
            _defaultMapSize = _mainCam.orthographicSize;
            _mainCam.orthographicSize = _inOverlayMapSize;
        }
    }

    public void CloseShop()
    {
        _shopOverlay.SetActive(false);
        _mainCam.orthographicSize = _defaultMapSize;
    }

    public void AddCoins()
    {
        _coinsToPlayer = Random.Range(2, 8);
        Coins += _coinsToPlayer;
        CoinsText.text = $"{Coins}";
    }

    public void CheckCoins()
    {
        if (Coins >= PotionPrice)
        {
            CoreManager.Instance.CoreHp += 5;
            Coins -= PotionPrice;
            CoinsText.text = $"{Coins}";
        }
    }
}
