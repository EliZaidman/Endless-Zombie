using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    #region Singleton
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
    #endregion

    #region Serialized Fields
    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    private GameObject _shopOverlay;
    
    [SerializeField]
    private TextMeshProUGUI ShopCoinsText, WSCoinsText;

    [SerializeField]
    private Collider2D _playerCol;

    [SerializeField]
    private float _inOverlayMapSize = 14f, _defaultMapSize;

    public AudioClip buySuond;
    
    #endregion

    #region Fields
    private int _coinsToPlayer, _shopCoins, _wSCoins;
    public int FireTrapPrice = 5, IceTrapPrice = 25, GelTrapPrice = 450, BlackFireTrapPrice = 900;
    #endregion

    #region Public Fields
    public TextMeshProUGUI CoinsText;
    public bool IsCannonGunAquired, IsMachinegunAquired;
    public int MachineGunPrice = 200, CannonPrice = 50, MaxHealthPotionPrice = 100, HealthPotionPrice = 5, GeneralCoins = 0, FireRatePotion = 10, PowerUpPotion = 10;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    private void Update()
    {
        if (GeneralCoins < 0)
        {
            GeneralCoins = 0;
        }
        _shopCoins = GeneralCoins;
        _wSCoins = GeneralCoins;

        ShopCoinsText.text = _shopCoins.ToString();
        WSCoinsText.text = _wSCoins.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsWaveOngoing && collision.collider == _playerCol)
        {
            Time.timeScale = 0;
            _shopOverlay.SetActive(true);
            _defaultMapSize = _mainCam.orthographicSize;
            _mainCam.orthographicSize = _inOverlayMapSize;
        }
    }
    #endregion

    #region Methods
    public void AddCoins()
    {
        _coinsToPlayer = Random.Range(2, 8);
        GeneralCoins += _coinsToPlayer; 
    }
    #endregion

    #region Events
    public void CloseShop()
    {
        Time.timeScale = 1;
        _shopOverlay.SetActive(false);
        _mainCam.orthographicSize = _defaultMapSize;
    }

    public void BuyMaxHealthPotion()
    {
        if (GeneralCoins >= MaxHealthPotionPrice)
        {
            CoreManager.Instance.CoreMaxHp += 5;
            GeneralCoins -= MaxHealthPotionPrice;
            AudioManager.Instance.PlayMusic(buySuond);
        }
    }

    public void BuyHealthPotion()
    {
        if (GeneralCoins >= HealthPotionPrice)
        {
            if (CoreManager.Instance.CoreHp == CoreManager.Instance.CoreMaxHp)
                return;

            else
            {
                CoreManager.Instance.CoreHp += 5;

                if (CoreManager.Instance.CoreHp >= CoreManager.Instance.CoreMaxHp)
                    CoreManager.Instance.CoreHp = CoreManager.Instance.CoreMaxHp;

                GeneralCoins -= HealthPotionPrice;
                AudioManager.Instance.PlayMusic(buySuond);
            }
        }
    }

    public void BuyPowerUpPotion()
    {
        if (GeneralCoins >= PowerUpPotion)
        {
            PlayerWeapon.Instance.BulletDmg += 5;
            GeneralCoins -= PowerUpPotion;
            AudioManager.Instance.PlayMusic(buySuond);
        }
    }

    public void BuyBulletSpeedUpPotion()
    {
        if (GeneralCoins >= FireRatePotion)
        {
            PlayerWeapon.Instance.CurrentFireRate -= 0.02f;
            GeneralCoins -= FireRatePotion;
            AudioManager.Instance.PlayMusic(buySuond);

            return;
        }
    }
    
    public void AquireMachinegun()
    {
        if (GeneralCoins >= MachineGunPrice)
        {
            IsMachinegunAquired = true;
            GeneralCoins -= MachineGunPrice;
            AudioManager.Instance.PlayMusic(buySuond);
        }
    }

    public void AquireCannonGun()
    {
        if (GeneralCoins >= CannonPrice)
        {
            IsCannonGunAquired = true;
            GeneralCoins -= CannonPrice;
            AudioManager.Instance.PlayMusic(buySuond);
        }
    }
    #endregion
}
