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

    [SerializeField] NavMeshSurface2d _navmesh2D;
    [SerializeField]
    private Tilemap ground;
    [SerializeField]
    private Tilemap walls;

    [SerializeField]
    private TileBase Walls;

    [SerializeField]
    private Camera cam;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    private void Start()
    {

        //_navmesh2D.BuildNavMesh();

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





        //if (Input.GetMouseButtonDown(0))
        //{
        //    ground.SetTile(walls.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition)), Walls);
        //    Debug.Log("BUILD");
        //    //_navmesh2D.UpdateNavMesh(_navmesh2D.navMeshData);
        //    _navmesh2D.BuildNavMesh();
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    walls.SetTile(walls.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition)), Walls);
        //    ground.SetTile(ground.WorldToCell(cam.ScreenToWorldPoint(Input.mousePosition)), Walls);
        //    Debug.Log("BUILD");
        //    _navmesh2D.BuildNavMesh();
        //}
    }


}
