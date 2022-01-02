using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

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
    private TextMeshProUGUI _countDown, _levelText, _targetsLeftText;

    [SerializeField]
    public int _level, _targetsRemaining;

    [SerializeField]
    public float _timer;

    [SerializeField]
    private bool _isWaveOngoing = false;
    #endregion

    #region Properties
        public bool IsWaveOngoing { get => _isWaveOngoing; set => _isWaveOngoing = value; }
        #endregion

    [SerializeField] 
    NavMeshSurface2d _navmesh2D;

    [SerializeField]
    private Tilemap _ground;
    [SerializeField]
    private Tilemap _walls;

    [SerializeField]
    private TileBase _wallTiles;

    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    SpawnerManager _spawnerManager;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    //private void Start()
    //{
    //    //_navmesh2D.BuildNavMesh();
    //}

    void Update()
    {
        _levelText.text = _level.ToString("0");

        if (!IsWaveOngoing)
        {
            _timer -= Time.deltaTime;
            _countDown.text = _timer.ToString("0");

            if (_timer <= 0)
                IsWaveOngoing = true;
        }
    }

    public void NextLevel()
    {
        _level++;
        _timer = 5;
        _spawnerManager._timeBetweenSpawns = 0;
        _spawnerManager._maxSpawns += _level;
        _spawnerManager._currentTimeBetweenSpawns = _spawnerManager._timeBetweenSpawns;
        IsWaveOngoing = false;
    }
}
