using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Game Manager not loaded properly");

            return _instance;
        }
    }

    private GameManager()
    {

    }
    #endregion

    #region Serialized Fields
    [SerializeField]
    private TextMeshProUGUI _countDown, _levelText, _targetsLeftText;

    [SerializeField]
    private int _level, _targetsRemaining;

    [SerializeField]
    private float _timer;

    [SerializeField]
    private bool _isWaveOngoing = false;
    #endregion

    #region Properties
    public bool IsWaveOngoing { get => _isWaveOngoing; set => _isWaveOngoing = value; }
    #endregion

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    void Update()
    {
        _levelText.text = _level.ToString();

        if (!IsWaveOngoing)
        {
            _timer -= Time.deltaTime;
            _countDown.text = _timer.ToString("0");
            if (_timer <= 0)
                IsWaveOngoing = true;
        }
    }
}
