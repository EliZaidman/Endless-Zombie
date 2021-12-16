using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private Transform _shotLocation;
    [SerializeField] private GameObject deafultWeapon;
    [SerializeField] private GameObject MachineGun;
    [SerializeField] private GameObject CanonWeapon;
    public float readyToShoot;
    bool HoldingDefaultWeapon = true;
    bool HoldingMachineGunWeapon = false;
    bool HoldingCanonWeapon = false;
   

    [SerializeField]
    private GameObject _shot;

    [SerializeField]
    public float _shotForce;

    void Update()
    {
        readyToShoot -= Time.deltaTime;
        if (readyToShoot <= 0 && Input.GetButton("Fire1"))
        {
            Shoot();
            if (HoldingDefaultWeapon)
            {
                readyToShoot = 0.3f;
                _shotForce = 20f;
                
            }
            if (HoldingMachineGunWeapon)
            {
                readyToShoot = 0.1f;
                _shotForce = 10f;
            }
            if (HoldingCanonWeapon)
            {
                readyToShoot = 0.5f;
                _shotForce = 50f;
            }
        }


        //if (Input.GetButton("Fire1"))
        //    Shoot();

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (HoldingDefaultWeapon)
            {
                deafultWeapon.SetActive(false);
                MachineGun.SetActive(true);
                HoldingMachineGunWeapon = true;
                HoldingDefaultWeapon = false;     
                Debug.Log("default");
                

            }
             else if (HoldingMachineGunWeapon)
            {
                MachineGun.SetActive(false);
                CanonWeapon.SetActive(true);
                HoldingMachineGunWeapon = false;
                HoldingCanonWeapon = true;

                Debug.Log("slow");


            }
            else if (HoldingCanonWeapon)
            {
                CanonWeapon.SetActive(false);
                deafultWeapon.SetActive(true);
                HoldingMachineGunWeapon = false;
                HoldingCanonWeapon = false;
                HoldingDefaultWeapon = true;

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
