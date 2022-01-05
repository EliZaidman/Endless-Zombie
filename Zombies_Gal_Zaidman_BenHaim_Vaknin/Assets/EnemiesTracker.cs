using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesTracker : MonoBehaviour
{
    GameObject start;

    private void Start()
    {
        start = GameObject.Find("LOCK ON");
    }
    void Update()
    {
        var dir = transform.parent.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.position = start.transform.position;
    }




}
