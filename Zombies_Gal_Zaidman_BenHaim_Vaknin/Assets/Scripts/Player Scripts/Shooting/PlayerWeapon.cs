using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _allWeapons;

    [SerializeField]
    private List<SpriteRenderer> _allWeaponSprites;

    [SerializeField]
    private GameObject _defaultWeapon, _machineGun, _canonWeapon, _bullet;

    [SerializeField]
    private Transform _bulletLocation;

    [SerializeField]
    private Image _currentWeaponSprite;

    [SerializeField]
    private float _fireRate = 0.3f, _bulletForce = 20;

    private bool _holdingDefaultWeapon = true;
    private bool _holdingMachineGun, _holdingCanon, _isShooting = false;
    
    public bool canShoot;
    
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

            if (_holdingMachineGun)
            {
                _fireRate = 0.1f;
                _bulletForce = 10f;
            }

            if (_holdingCanon)
            {
                _fireRate = 0.5f;
                _bulletForce = 50f;
            }
        }
    }

    public void ShootCycle()
    {
        if (_holdingDefaultWeapon)
        {
            _defaultWeapon.SetActive(false);
            _machineGun.SetActive(true);
            _holdingMachineGun = true;
            _holdingDefaultWeapon = false;
            _currentWeaponSprite.sprite = _allWeaponSprites[1].sprite;
            Debug.Log("machineGun");
        }
        else if (_holdingMachineGun)
        {
            _machineGun.SetActive(false);
            _canonWeapon.SetActive(true);
            _holdingMachineGun = false;
            _holdingCanon = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[2].sprite;
            Debug.Log("canonGun");
        }
        else if (_holdingCanon)
        {
            _canonWeapon.SetActive(false);
            _defaultWeapon.SetActive(true);
            _holdingMachineGun = false;
            _holdingCanon = false;
            _holdingDefaultWeapon = true;
            _currentWeaponSprite.sprite = _allWeaponSprites[0].sprite;
            Debug.Log("defaultGun");
        }
    }
}
