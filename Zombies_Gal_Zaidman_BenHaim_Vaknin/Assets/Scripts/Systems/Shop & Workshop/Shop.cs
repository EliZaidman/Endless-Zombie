using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop : MonoBehaviour
{
    private TextMeshPro thisText;
    private int coins;
    // Start is called before the first frame update
    void Start()
    {
        thisText = GetComponent<TextMeshPro>();
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {

        thisText.text = "coins is" + coins;
    }
    public void AddCoins()
    {
        coins += Random.Range(1, 10);


    }
}
