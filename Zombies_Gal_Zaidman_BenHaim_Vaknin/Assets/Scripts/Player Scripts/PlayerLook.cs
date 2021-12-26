using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private JoyStick _gunStick;

    [SerializeField]
    private Transform _gunPos;
    
    [SerializeField]
    private float _rotationSpeed = 100;

    private void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _gunPos.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

        if (_gunStick.InputDir != Vector3.zero)
            angle = Mathf.Atan2(_gunStick.InputDir.y, _gunStick.InputDir.x) * Mathf.Rad2Deg - 90;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _gunPos.rotation = Quaternion.Slerp(_gunPos.rotation, rotation, _rotationSpeed * Time.deltaTime); //fixedDeltaTime - Original
    }
}
