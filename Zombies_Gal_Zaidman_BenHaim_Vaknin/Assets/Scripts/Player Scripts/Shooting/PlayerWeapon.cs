using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField]
    private List<GameObject> _allWeapons;

    [SerializeField]
    private List<Sprite> _allWeaponsSprites;

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private Transform _bulletTr;

    [SerializeField]
    private Image _currentWeaponImage;

    [SerializeField]
    private float _defaultGunFireRate = 0.3f, _cannonGunFireRate = 0.5f, _machinegunFireRate = 0.1f;
    #endregion

    #region Fields
    private float _cooldown, _currentFireRate;

    private int _currentBulletForce = 20, _defaultGunBulletForce = 20, _cannonGunBulletForce = 50, _machinegunBulletForce = 10;
    private int _currentCycleIndex = 0;
    #endregion

    #region Public Fields
    public bool CanShoot;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _allWeapons[0].SetActive(true);
        _currentWeaponImage.sprite = _allWeaponsSprites[0];
        _currentFireRate = _defaultGunFireRate;
    }

    private void FixedUpdate()
    {
        Shoot();
    }
    #endregion

    #region WeaponCycle
    public void CycleNextWeapon(int numToAddToCycle)
    {
        _currentCycleIndex += numToAddToCycle;
        _currentCycleIndex = Mathf.Clamp(_currentCycleIndex, 0, _allWeapons.Count - 1);
        Debug.Log(_currentCycleIndex);
        NextWeapon();
        RefreshSprite();
    }

    public void CyclePreviousWeapon(int numToAddToCycle)
    {
        _currentCycleIndex -= numToAddToCycle;
        _currentCycleIndex = Mathf.Clamp(_currentCycleIndex, 0, _allWeapons.Count - 1);
        Debug.Log(_currentCycleIndex);
        PreviousWeapon();
        RefreshSprite();
    }

    private void NextWeapon()
    {
        Debug.Log("Refresh Weapon");
        foreach (GameObject weapon in _allWeapons)
            weapon.SetActive(false);

        if (_currentCycleIndex == 1)
        {
            if (Shop.Instance.IsCannonGunAquired)
            {
                _currentCycleIndex = 1;
                _currentFireRate = _cannonGunFireRate;
                _currentBulletForce = _cannonGunBulletForce;
            }

            else if (Shop.Instance.IsMachinegunAquired)
            {
                _currentCycleIndex = 2;
                _currentFireRate = _machinegunFireRate;
                _currentBulletForce = _machinegunBulletForce;
            }

            else
            {
                _currentCycleIndex = 0;
                _currentFireRate = _defaultGunFireRate;
                _currentBulletForce = _defaultGunBulletForce;
            }
        }

        else if (_currentCycleIndex == 2)
        {
            if (Shop.Instance.IsMachinegunAquired)
            {
                _currentCycleIndex = 2;
                _currentFireRate = _machinegunFireRate;
                _currentBulletForce = _machinegunBulletForce;
            }

            else if (Shop.Instance.IsCannonGunAquired)
            {
                _currentCycleIndex = 1;
                _currentFireRate = _cannonGunFireRate;
                _currentBulletForce = _cannonGunBulletForce;
            }

            else
            {
                _currentCycleIndex = 0;
                _currentFireRate = _defaultGunFireRate;
                _currentBulletForce = _defaultGunBulletForce;
            }
        }

        else
            return;
        
        _cooldown = _currentFireRate;

        _allWeapons[_currentCycleIndex].SetActive(true);
    }

    private void PreviousWeapon()
    {
        Debug.Log("Refresh Weapon");
        foreach (GameObject weapon in _allWeapons)
            weapon.SetActive(false);

        if (_currentCycleIndex == 1)
        {
            if (Shop.Instance.IsCannonGunAquired)
            {
                _currentCycleIndex = 1;
                _currentFireRate = _cannonGunFireRate;
                _currentBulletForce = _cannonGunBulletForce;
            }

            else
            {
                _currentCycleIndex = 0;
                _currentFireRate = _defaultGunFireRate;
                _currentBulletForce = _defaultGunBulletForce;
            }
        }

        else if (_currentCycleIndex == 0)
        {
            _currentCycleIndex = 0;
            _currentFireRate = _defaultGunFireRate;
            _currentBulletForce = _defaultGunBulletForce;
        }

        else
            return;

        _allWeapons[_currentCycleIndex].SetActive(true);
    }

    private void RefreshSprite()
    {
        Debug.Log("Refresh Sprite");
        _currentWeaponImage.sprite = _allWeaponsSprites[_currentCycleIndex];
    }
    #endregion

    #region WeaponShoot
    private void InstansiateBullet()
    {
        GameObject _shotClone = Instantiate(_bullet, _bulletTr.position, _bulletTr.rotation);
        Rigidbody2D rb = _shotClone.GetComponent<Rigidbody2D>();
        rb.AddForce(_bulletTr.up * _currentBulletForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        _isShooting = false;
        _cooldown -= Time.deltaTime;
        Debug.Log("Yehou222");

        if (Input.GetMouseButton(0) && CanShoot && _cooldown <= 0)
        {
            Debug.Log("Yehou");
            InstansiateBullet();
            _cooldown = _currentFireRate;
        }
    }
    #endregion
}
