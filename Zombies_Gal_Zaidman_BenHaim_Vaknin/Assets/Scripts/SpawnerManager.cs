using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    //public GameObject[] spawnerList = new GameObject[5];
    [SerializeField]
    private List<GameObject> _spawners = new List<GameObject>(4);

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
    }
    private void Update()
    {
        if (_currentTimeBetweenSpawns <= 0 && _maxSpawns > 0)
        {
            Instantiate(_zombie, _spawners[Random.Range(0, 4)].transform.position, Quaternion.identity);
            _currentTimeBetweenSpawns = _timeBetweenSpawns;
            _maxSpawns --;
        }

        else
            _currentTimeBetweenSpawns -= Time.deltaTime;
    }
}
