using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    //Object you want to follow
    [SerializeField]
    private Transform _playerTr;

    //[SerializeField]
    //private RectTransform _gUI;

    //Set to z -10
    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, -10f);

    //How much delay on the follow
    [Range(1, 10)]
    public float smoothing;

    private void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPosition = _playerTr.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
