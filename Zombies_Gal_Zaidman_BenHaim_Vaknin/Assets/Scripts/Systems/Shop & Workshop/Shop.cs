using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _coinsText;

    private int _coins = 0, _coinsToPlayer;

    public void AddCoins()
    {
        _coinsToPlayer = Random.Range(1, 10);
        _coins += _coinsToPlayer;
        _coinsText.text = $"{_coins}";
    }
}
