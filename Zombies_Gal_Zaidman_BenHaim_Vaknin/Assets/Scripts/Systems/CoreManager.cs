using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreManager : MonoBehaviour

{
    public int CoreHp = 20;
    public int CoreMaxHp = 20;
    EnemyAI enemyAI;

    [SerializeField]
    private SpawnerManager _sM;

    [SerializeField]
    TextMeshProUGUI tmpCoreHp;

    // Update is called once per frame
    void Update()
    {
        tmpCoreHp.text = CoreHp.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CoreHp -= 5;
            _sM._ZombiesInScene.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
