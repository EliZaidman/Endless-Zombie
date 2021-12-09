using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb;

    [SerializeField]
    private MovementJoystick _movementJoystick;

    [SerializeField]
    private float _playerSpeed;

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_movementJoystick.JoystickVec.y !=0)
            _rb.velocity = new Vector2(_movementJoystick.JoystickVec.x * _playerSpeed, _movementJoystick.JoystickVec.y * _playerSpeed);

        else
            _rb.velocity = Vector2.zero;

    }
}
