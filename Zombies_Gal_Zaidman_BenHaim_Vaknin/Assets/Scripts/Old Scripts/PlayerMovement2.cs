using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Change everything to touch!

public class PlayerMovement2 : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private Camera _mainCam;

    [SerializeField]
    private float _moveSpeed = 0f;

    private Vector2 _playerPos = Vector2.zero;
    private Vector2 _mousePos = Vector2.zero;

    private void Update()
    {
        _playerPos.x = Input.GetAxisRaw("Horizontal");
        _playerPos.y = Input.GetAxisRaw("Vertical");

        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _mainCam.transform.position = new Vector3(_rb.position.x, _rb.position.y, _mainCam.transform.position.z);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _playerPos * _moveSpeed * Time.deltaTime);

        Vector2 lookDirection = _mousePos - _rb.position;
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = lookAngle;
    }
}
