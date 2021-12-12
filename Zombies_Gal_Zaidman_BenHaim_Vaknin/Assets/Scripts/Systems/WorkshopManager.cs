using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkshopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _inventoryLayout;

    [SerializeField]
    private Collider2D _wsCol, _playerCol;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Touching");
        if (!GameManager.Instance.IsWaveOngoing && collision.collider == _playerCol)
        {
            Debug.Log("Inside IF");
            _inventoryLayout.SetActive(true);
        }
    }
    
    public void CloseWS()
    {
        _inventoryLayout.SetActive(false);
    }
}
