using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
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
    #endregion

    #region Serialized Fields
    [SerializeField]
    private SpawnerManager _spawnerManager;
    
    [SerializeField]
    private TextMeshProUGUI _countDown, _levelText, _nextLevelTxt;

    [SerializeField]
    private bool _isWaveOngoing = false;
    #endregion

    #region Properties
    public bool IsWaveOngoing { get => _isWaveOngoing; set => _isWaveOngoing = value; }
    #endregion

    #region Public Fields
    //public GameObject rest;
    public ParticleSystem hitEffect;
    public ParticleSystem deathEffect;
    public ParticleSystem warningEffect;
    public ParticleSystem fireEffect;
    public ParticleSystem iceEffect;
    public int Level, TargetsRemaining;
    public float Timer, TimeSinceLevelStart;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    void Update()
    {
        _levelText.text = Level.ToString("0");
        Shop.Instance.CoinsText.text = $"{Shop.Instance.GeneralCoins}";
        CoreManager.Instance.CoreHpTxt.text = $"{CoreManager.Instance.CoreHp} / {CoreManager.Instance.CoreMaxHp}";

        if (!IsWaveOngoing)
        {
            Timer -= Time.deltaTime;
            _countDown.text = Timer.ToString("0");

            if (Timer <= 0)
                IsWaveOngoing = true;
        }

        if (CoreManager.Instance.CoreHp <= 0)
        {
            Time.timeScale = 0;
            CoreManager.Instance.GameOver.SetActive(true);
        }

        if (Shop.Instance.GeneralCoins >= 999)
            Shop.Instance.GeneralCoins = 999;

        TimeSinceLevelStart += Time.deltaTime;
        if (IsWaveOngoing == false)
            TimeSinceLevelStart = 0;
    }
    #endregion

    #region Methods
    public void NextLevel()
    {
        if (Level < 1)
        {
            _nextLevelTxt.text = "Start Game!";
            Timer = 5;
            _spawnerManager._timeBetweenSpawns = 1;
            _spawnerManager._maxSpawns += Level;
            _spawnerManager._currentTimeBetweenSpawns = _spawnerManager._timeBetweenSpawns;
            IsWaveOngoing = false;
        }

        else
        {
            _nextLevelTxt.text = "Next Level!";
            Level++;
            Timer = 5;
            _spawnerManager._timeBetweenSpawns = 1;
            _spawnerManager._maxSpawns += Level;
            _spawnerManager._currentTimeBetweenSpawns = _spawnerManager._timeBetweenSpawns;
            IsWaveOngoing = false;
        }
    }

    [System.Obsolete]
    public void ResetGame()
    {
        Level = 1;
        _spawnerManager._timeBetweenSpawns = 0;

        foreach (GameObject enemy in FindObjectOfType<SpawnerManager>()._ZombiesInScene)
            Destroy(enemy);

        FindObjectOfType<SpawnerManager>()._ZombiesInScene.Clear();

        CoreManager.Instance.CoreMaxHp = 20;
        CoreManager.Instance.CoreHp = CoreManager.Instance.CoreMaxHp;

        CoreManager.Instance.GameOver.SetActive(false);

        if (CoreManager.Instance.GameOver == true)
        {
            Time.timeScale = 1;
            Application.LoadLevel(1);
        }
    }

    public void EndGame() => Application.LoadLevel(0);
    #endregion
}
