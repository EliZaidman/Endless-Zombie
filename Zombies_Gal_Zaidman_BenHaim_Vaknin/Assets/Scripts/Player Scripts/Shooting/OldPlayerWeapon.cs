using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OldPlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> _allWeaponSprites;

    [SerializeField]
    private GameObject _defaultWeapon, _machineGun, _canonWeapon, _bullet;

    [SerializeField]
    private Transform _bulletLocation;

    [SerializeField]
    private Image _currentWeaponSprite;

    private float _fireRate = 0.3f, _bulletForce = 20;

    private bool _holdingDefaultWeapon = true;
    private bool _holdingMachineGun, _holdingCanon, _isShooting = false;
    
    public bool canShoot, IsCannonAquired, IsMachineGunAquired;
    
    public bool IsShooting { get => _isShooting; set => _isShooting = value; }

    private void Update()
    {
        WeaponsStats();
    }

    private void Shoot()
    {
        GameObject _shotClone = Instantiate(_bullet, _bulletLocation.position, _bulletLocation.rotation);
        Rigidbody2D rb = _shotClone.GetComponent<Rigidbody2D>();
        rb.AddForce(_bulletLocation.up * _bulletForce, ForceMode2D.Impulse);
    }

    private void WeaponsStats()
    {
        _fireRate -= Time.deltaTime;

        if (Input.GetMouseButton(0) && canShoot && _fireRate <= 0)
        {
            Shoot();

            if (_holdingDefaultWeapon)
            {
                _fireRate = 0.3f;
                _bulletForce = 20f;
            }

            if (_holdingCanon)
            {
                _fireRate = 0.5f;
                _bulletForce = 50f;
            }

            if (_holdingMachineGun)
            {
                _fireRate = 0.1f;
                _bulletForce = 10f;
            }            
        }
    }

    public void ShootCycle()
    {
        // switch to cannon from gun
        if (_holdingDefaultWeapon /*&& IsCannonAquired*/)
        {
            _defaultWeapon.SetActive(false);
            _holdingDefaultWeapon = false;
            _canonWeapon.SetActive(true);
            _holdingCanon = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[2].sprite;
            Debug.Log("canonGun");
        }
        // switch to machinegun from cannon
        else if (_holdingCanon /*&& IsMachineGunAquired*/)
        {
            _canonWeapon.SetActive(false);
            _holdingCanon = false;
            _machineGun.SetActive(true);
            _holdingMachineGun = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[1].sprite;
            Debug.Log("machineGun");
        }
        // switch to gun from machinegun
        else if (_holdingMachineGun)
        {
            _machineGun.SetActive(false);
            _holdingMachineGun = false;
            _defaultWeapon.SetActive(true);
            _holdingDefaultWeapon = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[0].sprite;
            Debug.Log("defaultGun");
        }
    }

    public void AquireCannon()
    {
        if (Shop.Instance.GeneralCoins >= Shop.Instance.CannonPrice)
        {
            IsCannonAquired = true;
            Shop.Instance.GeneralCoins -= Shop.Instance.CannonPrice;
            Shop.Instance.CoinsText.text = $"{Shop.Instance.GeneralCoins}";
        }
    }

    public void AquireMachineGun()
    {
        if (Shop.Instance.GeneralCoins >= Shop.Instance.MachineGunPrice)
        {
            IsMachineGunAquired = true;
            Shop.Instance.GeneralCoins -= Shop.Instance.MachineGunPrice;
            Shop.Instance.CoinsText.text = $"{Shop.Instance.GeneralCoins}";
        }
    }
}


