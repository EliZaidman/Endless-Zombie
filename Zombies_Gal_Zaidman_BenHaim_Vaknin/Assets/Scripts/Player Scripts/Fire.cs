using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private GameObject _deafultWeapon, _machineGun, _canonWeapon, _shot;

    [SerializeField]
    private Transform _shotLocation;

    [SerializeField]
    private float _readyToShoot = 0.3f, _shotForce = 20;

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
                _shotForce = 20f;
                
            }
            if (_holdingMachineGunWeapon)
            {
                _readyToShoot = 0.1f;
                _shotForce = 10f;
            }
            if (_holdingCanonWeapon)
            {
                _readyToShoot = 0.5f;
                _shotForce = 50f;
            }
        }


        //if (Input.GetButton("Fire1"))
        //    Shoot();

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (_holdingDefaultWeapon)
            {
                _deafultWeapon.SetActive(false);
                _machineGun.SetActive(true);
                _holdingMachineGunWeapon = true;
                _holdingDefaultWeapon = false;     
                Debug.Log("default");
                

            }
             else if (_holdingMachineGunWeapon)
            {
                _machineGun.SetActive(false);
                _canonWeapon.SetActive(true);
                _holdingMachineGunWeapon = false;
                _holdingCanonWeapon = true;

                Debug.Log("slow");


            }
            else if (_holdingCanonWeapon)
            {
                _canonWeapon.SetActive(false);
                _deafultWeapon.SetActive(true);
                _holdingMachineGunWeapon = false;
                _holdingCanonWeapon = false;
                _holdingDefaultWeapon = true;

                Debug.Log("default");

            }

        }
    }

    private void Shoot()
    {

            GameObject _shotClone = Instantiate(_shot, _shotLocation.position, _shotLocation.rotation);
            Rigidbody2D rb = _shotClone.GetComponent<Rigidbody2D>();
            rb.AddForce(_shotLocation.up * _shotForce, ForceMode2D.Impulse);
        
    }
}
