using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorkshopManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _workshopOverlay;

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
            _workshopOverlay.SetActive(true);
            _defaultMapSize = _mainCam.orthographicSize;
            _mainCam.orthographicSize = _inOverlayMapSize;
        }
    }
    
    public void CloseWS()
    {
        _workshopOverlay.SetActive(false);
        _mainCam.orthographicSize = _defaultMapSize;
    }
}
