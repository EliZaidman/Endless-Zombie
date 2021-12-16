using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkshopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventoryOverlay;

    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    private Collider2D _wsCol, _playerCol;

    [SerializeField]
    private float _mapSize = 14f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Touching");
        if (!GameManager.Instance.IsWaveOngoing && collision.collider == _playerCol)
        {
            Debug.Log("Inside IF");
            _inventoryOverlay.SetActive(true);
            _mainCam.orthographicSize = 14;
        }
    }
    
    public void CloseWS()
    {
        _inventoryOverlay.SetActive(false);
    }
}
