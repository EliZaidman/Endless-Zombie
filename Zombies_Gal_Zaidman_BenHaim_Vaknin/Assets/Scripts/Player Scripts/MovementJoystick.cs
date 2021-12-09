using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    [SerializeField]
    private RectTransform _joystick, _joystickBGTr;

    private Vector2 _joystickVec, _joystickTouchpos, _joystickOriginalpos;
    private float _joystickRadius;

    public Vector2 JoystickVec { get => _joystickVec; set => _joystickVec = value; }

    void Start()
    {
        _joystickOriginalpos = _joystickBGTr.position;
        _joystickRadius = _joystickBGTr.sizeDelta.y / 4;
    }

    public void Pointdown()
    {
        _joystickBGTr.position = Input.mousePosition;
        _joystickBGTr.position = Input.mousePosition;
        _joystickTouchpos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        JoystickVec = (dragPos - _joystickTouchpos).normalized;

        float joystickDist = Vector2.Distance(dragPos, _joystickTouchpos);

        if (joystickDist<_joystickRadius)
            _joystick.position = _joystickTouchpos + JoystickVec * joystickDist;

        else
            _joystick.position = _joystickTouchpos + JoystickVec * _joystickRadius;

    }

    public void PointerUp()
    {
        JoystickVec = Vector2.zero;
        _joystick.position = _joystickOriginalpos;
        _joystickBGTr.position = _joystickOriginalpos;
    }

}
