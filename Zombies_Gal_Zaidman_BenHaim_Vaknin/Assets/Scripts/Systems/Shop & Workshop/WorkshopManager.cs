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
    private Collider2D _playerCol;

    [SerializeField]
    private float _inOverlayMapSize = 14f, _defaultMapSize;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsWaveOngoing && collision.collider == _playerCol)
        {
            Time.timeScale = 0;
            _workshopOverlay.SetActive(true);
            _defaultMapSize = _mainCam.orthographicSize;
            _mainCam.orthographicSize = _inOverlayMapSize;
        }
    }
    
    public void CloseWS()
    {
        Time.timeScale = 1;
        _workshopOverlay.SetActive(false);
        _mainCam.orthographicSize = _defaultMapSize;
    }

    //public void FireTrap()
    //{
    //    if (Shop.Instance.GeneralCoins >= Shop.Instance.FireTrapPrice)
    //    {
    //        Shop.Instance.GeneralCoins -= Shop.Instance.FireTrapPrice;
    //        AudioManager.Instance.PlayMusic(Shop.Instance.buySuond);
    //    }
    //}
    //
    //public void IceTrap()
    //{
    //    if (Shop.Instance.GeneralCoins >= Shop.Instance.IceTrapPrice)
    //    {
    //        Shop.Instance.GeneralCoins -= Shop.Instance.IceTrapPrice;
    //        AudioManager.Instance.PlayMusic(Shop.Instance.buySuond);
    //    }
    //}
}
