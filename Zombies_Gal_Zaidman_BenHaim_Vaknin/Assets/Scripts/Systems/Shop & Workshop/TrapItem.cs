using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapItem : MonoBehaviour
{
    [SerializeField]
    private CoreManager _cM;

    [SerializeField]
    private int _trapDamage = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            Destroy(collision.gameObject);
        else if (collision.gameObject.tag == "Player")
            _cM.CoreHp -= _trapDamage;

        Destroy(gameObject);
    }
}
