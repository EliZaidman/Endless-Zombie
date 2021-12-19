using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private GameObject _deafultWeapon, _machineGun, _canonWeapon, _bullet;

    [SerializeField]
    private Transform _bulletLocation;

    [SerializeField]
    private SpriteRenderer _currentWeaponSprite;

    [SerializeField]
    private List<SpriteRenderer> _allWeaponSprites;

    [SerializeField]
    private float _readyToShoot = 0.3f, _bulletForce = 20;

    private bool _holdingDefaultWeapon = true;
    private bool _holdingMachineGunWeapon, _holdingCanonWeapon, _isShooting = false;

    public bool IsShooting { get => _isShooting; set => _isShooting = value; }

    void Update()
    {
        _readyToShoot -= Time.deltaTime;
        if (_readyToShoot <= 0 && _isShooting)
        {
            Shoot();
            if (_holdingDefaultWeapon)
            {
                _readyToShoot = 0.3f;
                _bulletForce = 20f;

            }
            if (_holdingMachineGunWeapon)
            {
                _readyToShoot = 0.1f;
                _bulletForce = 10f;
            }
            if (_holdingCanonWeapon)
            {
                _readyToShoot = 0.5f;
                _bulletForce = 50f;
            }
        }

        //if (Input.GetButton("Fire1"))
        //    Shoot();
    }

    private void Shoot()
    {
        GameObject _shotClone = Instantiate(_bullet, _bulletLocation.position, _bulletLocation.rotation);
        Rigidbody2D rb = _shotClone.GetComponent<Rigidbody2D>();
        rb.AddForce(_bulletLocation.up * _bulletForce, ForceMode2D.Impulse);
    }

    public void ShootCycle()
    {
        if (_holdingDefaultWeapon)
        {
            _deafultWeapon.SetActive(false);
            _machineGun.SetActive(true);
            _holdingMachineGunWeapon = true;
            _holdingDefaultWeapon = false;
            _currentWeaponSprite.sprite = _allWeaponSprites[0].sprite;
            Debug.Log("machineGun");
        }
        else if (_holdingMachineGunWeapon)
        {
            _machineGun.SetActive(false);
            _canonWeapon.SetActive(true);
            _holdingMachineGunWeapon = false;
            _holdingCanonWeapon = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[1].sprite;
            Debug.Log("canonGun");
        }
        else if (_holdingCanonWeapon)
        {
            _canonWeapon.SetActive(false);
            _deafultWeapon.SetActive(true);
            _holdingMachineGunWeapon = false;
            _holdingCanonWeapon = false;
            _holdingDefaultWeapon = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[2].sprite;
            Debug.Log("defaultGun");
        }
    }
}
