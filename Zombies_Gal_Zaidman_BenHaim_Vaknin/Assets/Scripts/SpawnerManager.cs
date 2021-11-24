using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //public GameObject[] spawnerList = new GameObject[5];

    private List<GameObject> _enemies;

    [SerializeField]
    private GameObject _zombie;

    [SerializeField]
    private float _timeBetweenSpawns = 0;

    [SerializeField]
    private int _maxSpawns;

    private float _currentTimeBetweenSpawns;

    private void Start()
    {
        _currentTimeBetweenSpawns = _timeBetweenSpawns;
        _enemies = new List<GameObject>(_maxSpawns);
    }
    private void Update()
    {
        if (_currentTimeBetweenSpawns <= 0)
        {
            Instantiate(_zombie, _enemies[Random.Range(0, _maxSpawns)].transform.position, Quaternion.identity);
            _currentTimeBetweenSpawns = _timeBetweenSpawns;
        }

        else
            _currentTimeBetweenSpawns -= Time.deltaTime;
    }
}
