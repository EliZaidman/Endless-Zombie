using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _playerRb;

    [SerializeField]
    private JoyStick _moveStick;

    [SerializeField]
    private float _speed;
    
    private Vector2 _moveVelocity;

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (_moveStick.InputDir != Vector3.zero)
            moveInput = _moveStick.InputDir;

        _moveVelocity = moveInput.normalized * _speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _playerRb.MovePosition(_playerRb.position + _moveVelocity);
    }
}
