using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _coinsText;
    [SerializeField]
    private GameObject _inventoryOverlay;

    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    private Collider2D _wsCol, _playerCol;

    [SerializeField]
    private float _inOverlayMapSize = 14f, _defaultMapSize;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Touching");
        if (!GameManager.Instance.IsWaveOngoing && collision.collider == _playerCol)
        {
            Debug.Log("Inside IF");
            _inventoryOverlay.SetActive(true);
            _defaultMapSize = _mainCam.orthographicSize;
            _mainCam.orthographicSize = _inOverlayMapSize;
        }
    }

    public void CloseWS()
    {
        _inventoryOverlay.SetActive(false);
        _mainCam.orthographicSize = _defaultMapSize;
    }
    private int _coins = 0, _coinsToPlayer;

    public void AddCoins()
    {
        _coinsToPlayer = Random.Range(2, 8);
        _coins += _coinsToPlayer;
        _coinsText.text = $"{_coins}";
    }
}
