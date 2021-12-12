using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    [SerializeField] TextMeshProUGUI countDown;
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] TextMeshProUGUI TargetsLeftText;

    public int Level;
    public int TargetsRemaining;
    public float timer;
    public bool isLevelRunning = false;

    private GameManager()
    {

    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        else
            Destroy(this);
    }

    void Update()
    {
        LevelText.text = Level.ToString();

        if (!isLevelRunning)
        {
            timer -= Time.deltaTime;
            countDown.text = timer.ToString("0");
            if (timer <= 0)
                isLevelRunning = true;
        }
    }
}
