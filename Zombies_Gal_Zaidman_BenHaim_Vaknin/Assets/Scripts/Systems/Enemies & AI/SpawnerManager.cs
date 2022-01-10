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

    #region Serialized Fields
    [SerializeField]
    private List<GameObject> _spawners = new List<GameObject>(4);

    [SerializeField]
    private List<GameObject> _allEnemies;

    [SerializeField]
    public GameObject _enemy;

    [SerializeField]
    private RectTransform _rTr;

    [SerializeField]
    private Button _nextLevelButton;

    [SerializeField]
    public List<GameObject> _ZombiesInScene = new List<GameObject>();

    [SerializeField]
    public float _timeBetweenSpawns = 0;

    [SerializeField]
    public int _maxSpawns;

    public AudioClip[] randomSpawnSounds;
    #endregion

    #region Public Fields
    public float _currentTimeBetweenSpawns;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _currentTimeBetweenSpawns = _timeBetweenSpawns;
        _maxSpawns = GameManager.Instance.Level;
        //_idCycler = 0;
    }

    private void Update()
    {
        GameObject selectedSpawner = _spawners[Random.Range(0, 4)];

        if (_rTr == null)
            _rTr = selectedSpawner.GetComponent<RectTransform>();
        else
            Debug.Log("Between Levels");

        float rectX = Random.Range(_rTr.rect.xMin, _rTr.rect.xMax);
        float rectY = Random.Range(_rTr.rect.yMin, _rTr.rect.yMax);

        Vector2 randomPosInsideSpawner = new Vector2(rectX, rectY);

        if (GameManager.Instance.IsWaveOngoing)
        {
            int enemyIndex = Mathf.FloorToInt(GameManager.Instance.Level / 10);

            if (enemyIndex > 0 && enemyIndex < 2)
                _enemy = _allEnemies[1];

            else if (enemyIndex >= 2)
                _enemy = _allEnemies[2];

            if (_currentTimeBetweenSpawns <= 0 && _maxSpawns > 0)
            {
                GameObject newEnemy = Instantiate(_enemy, (Vector2)_spawners[Random.Range(0, 4)].transform.position + randomPosInsideSpawner, Quaternion.identity);
                AudioManager.Instance.RandomSoundEffect(randomSpawnSounds);
                _ZombiesInScene.Add(newEnemy);
                _currentTimeBetweenSpawns = _timeBetweenSpawns;
                _maxSpawns--;
            }

            else
            {
                _currentTimeBetweenSpawns -= Time.deltaTime;
                _rTr = null;
            }
        }
        
        if (!(CoreManager.Instance.CoreHp <= 0) && GameManager.Instance.Level > 0 && _ZombiesInScene.Count == 0 && GameManager.Instance.IsWaveOngoing)
        {
            if (GameManager.Instance.TimeSinceLevelStart > 5)
                _nextLevelButton.gameObject.SetActive(true);
        }

        else
            _nextLevelButton.gameObject.SetActive(false);
    }
    #endregion
}
