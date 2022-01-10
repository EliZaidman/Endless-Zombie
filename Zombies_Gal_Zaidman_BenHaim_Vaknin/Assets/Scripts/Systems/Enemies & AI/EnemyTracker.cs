using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public GameObject TrackerPos;

    private void Start()
    {
        TrackerPos = GameObject.Find("TrackerPos");
    }
    void Update()
    {
        //var dir = transform.parent.transform.position - transform.position;
        //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.position = TrackerPos.transform.position;
    }
}