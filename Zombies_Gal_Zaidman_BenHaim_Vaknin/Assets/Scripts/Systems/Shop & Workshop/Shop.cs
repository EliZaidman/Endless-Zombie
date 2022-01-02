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

    [SerializeField]
    private TextMeshProUGUI _coinsText;

    [SerializeField]
    private GameObject _shopOverlay;
    
    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    private Collider2D _shopCol, _playerCol;

    [SerializeField]
    private float _inOverlayMapSize = 14f, _defaultMapSize;

    public int PotPrice = 5;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Touching");
        if (!GameManager.Instance.IsWaveOngoing && collision.collider == _playerCol)
        {
            Debug.Log("Inside IF");
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
    public int _coins = 0, _coinsToPlayer;

    public void AddCoins()
    {
        _coinsToPlayer = Random.Range(2, 8);
        _coins += _coinsToPlayer;
        _coinsText.text = $"{_coins}";
    }

    public void CheckCoins()
    {
        Debug.Log("C more");
        if (_coins >= PotPrice)
        {
            Debug.Log("Butts");
            CoreManager.Instance.CoreHp += 5;
            _coins -= PotPrice;
        }
    }
}
