using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField]
    private Transform _shotLocation;

    [SerializeField]
    private GameObject _shot;

    [SerializeField]
    private float _shotForce = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void Shoot()
    {
        GameObject _shotClone = Instantiate(_shot, _shotLocation.position, _shotLocation.rotation);
        Rigidbody2D rb = _shotClone.GetComponent<Rigidbody2D>();
        rb.AddForce(_shotLocation.up * _shotForce, ForceMode2D.Impulse);
    }
}
