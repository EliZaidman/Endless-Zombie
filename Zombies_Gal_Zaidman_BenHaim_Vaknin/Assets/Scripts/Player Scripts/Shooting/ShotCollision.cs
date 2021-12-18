using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotCollision : MonoBehaviour
{
    [SerializeField]
    GameObject _hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 3)
        {
            GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.1f);
            Destroy(gameObject);
        }
    }
}
