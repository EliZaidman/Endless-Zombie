using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playWarningEffect : MonoBehaviour
{
    public GameObject Top;
    public GameObject Left;
    public GameObject Right;
    public GameObject Bottom;
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Top"))
        {
            Instantiate(GameManager.Instance.warningEffect, Top.transform);           
            Debug.Log("Top Warning");
        }

        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Left"))
        {
            Instantiate(GameManager.Instance.warningEffect, Left.transform);
            Debug.Log("Left Warning");
        }

        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Right"))
        {
            Instantiate(GameManager.Instance.warningEffect, Right.transform);
            Debug.Log("Right Warning");
        }

        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Bottom"))
        {
            Instantiate(GameManager.Instance.warningEffect, Bottom.transform);
            Debug.Log("Bottom Warning");
        }
    }


}
