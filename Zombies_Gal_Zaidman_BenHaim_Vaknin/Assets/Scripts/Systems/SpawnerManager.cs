using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _spawners = new List<GameObject>(4);

    [SerializeField]
    private GameObject _zombie;

    [SerializeField]
    private RectTransform _rTr;

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
        GameObject selectedSpawner = _spawners[Random.Range(0, 4)];

        if (_rTr == null)
            _rTr = selectedSpawner.GetComponent<RectTransform>();
        else
            Debug.Log("There is an issue with selecting a spawner.");

        float rectX = Random.Range(_rTr.rect.xMin, _rTr.rect.xMax);
        float rectY = Random.Range(_rTr.rect.yMin, _rTr.rect.yMax);

        Vector2 randomPosInsideSpawner = new Vector2(rectX, rectY);

        if (GameManager.Instance.IsWaveOngoing)
        {
            if (_currentTimeBetweenSpawns <= 0 && _maxSpawns > 0)
            {
                Instantiate(_zombie, (Vector2)_spawners[Random.Range(0, 4)].transform.position + randomPosInsideSpawner, Quaternion.identity);

                _currentTimeBetweenSpawns = _timeBetweenSpawns;
                _maxSpawns--;
                selectedSpawner = null;
            }

            else
            {
                _currentTimeBetweenSpawns -= Time.deltaTime;
                _rTr = null;
            }
        }
    }
}
