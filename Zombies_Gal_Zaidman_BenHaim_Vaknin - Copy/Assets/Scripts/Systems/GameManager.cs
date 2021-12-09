using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int Level;
    public int TargetsRemaining;
    public float timer;

    [SerializeField] TextMeshProUGUI countDown;
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] TextMeshProUGUI TargetsLeftText;

    public bool isLevelRunning = false;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Update()
    {
        LevelText.text = Level.ToString();

        if (!isLevelRunning)
        {
            timer -= Time.deltaTime;
            countDown.text = timer.ToString("0");
            if (timer <= 0)
            {
                isLevelRunning = true;
            }
        }


        
    }
}
