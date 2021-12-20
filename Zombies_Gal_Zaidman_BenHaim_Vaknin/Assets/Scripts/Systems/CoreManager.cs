using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreManager : MonoBehaviour

{
    public int CoreHp = 20;
    EnemyAI enemyAI;
    [SerializeField]
    TextMeshProUGUI tmpCoreHp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmpCoreHp.text = CoreHp.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            CoreHp -= 5;
        }

    }
}
