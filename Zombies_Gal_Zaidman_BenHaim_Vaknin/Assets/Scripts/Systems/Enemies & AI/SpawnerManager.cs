using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerManager : MonoBehaviour
{
    #region Singleton
    private static SpawnerManager _instance;
    public static SpawnerManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Spawn Manager not loaded properly");

            return _instance;
        }
    }

    private SpawnerManager()
    {

    }
    #endregion

    [SerializeField]
    private List<GameObject> _spawners = new List<GameObject>(4);

    [SerializeField]
   
    private GameObject _zombie;

    [SerializeField]
    public List<GameObject> _ZombiesInScene = new List<GameObject>();

    [SerializeField]
    private RectTransform _rTr;

    [SerializeField]
    private Button NextLevelButton;
    
    [SerializeField]
    public float _timeBetweenSpawns = 0;

    [SerializeField]
    public int _maxSpawns;

    public float _currentTimeBetweenSpawns;


    private void Start()
    {
        _currentTimeBetweenSpawns = _timeBetweenSpawns;
    }

    private void Update()
    {
        Debug.Log(_ZombiesInScene.Count);
       
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
                GameObject newEnemy = Instantiate(_zombie, (Vector2)_spawners[Random.Range(0, 4)].transform.position + randomPosInsideSpawner, Quaternion.identity);

                _ZombiesInScene.Add(newEnemy);
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
        if (_ZombiesInScene.Count == 0 && GameManager.Instance.IsWaveOngoing)
        {
            NextLevelButton.gameObject.SetActive(true);
        }
        else
        {
            NextLevelButton.gameObject.SetActive(false);
        }
    }

}
