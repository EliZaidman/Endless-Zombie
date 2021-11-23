using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public GameObject zombie;

    //public GameObject[] spawnerList = new GameObject[5];
    public float timeBetweenSpawns;
    public float maxSpawns;


    public GameObject[] obj = new GameObject[4];

    // Use this for initialization
    void Start()
    {
        
    }

    private void Update()
    {
            if (timeBetweenSpawns <= 0)
            {
                Instantiate(zombie, obj[Random.Range(0,4)].transform.position,Quaternion.identity);
            timeBetweenSpawns = 1;
            }


            else
            {
                timeBetweenSpawns -= Time.deltaTime;
            }
    }


}
