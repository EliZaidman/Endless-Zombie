using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public GameObject Joystick;
    public GameObject JoystickBG;
    public Vector2 joystickVec;
    public Vector2 joystickTouchpos;
    private Vector2 joystickOriginalpos;
    private float joystickRadius;

    // Start is called before the first frame update
    void Start()
    {
        joystickOriginalpos = JoystickBG.transform.position;
        joystickRadius = JoystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
    }
    public void Pointdown()
    {
        Joystick.transform.position = Input.mousePosition;
        JoystickBG.transform.position = Input.mousePosition;
        joystickTouchpos = Input.mousePosition;
    }
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchpos).normalized;
        float joystickDist = Vector2.Distance(dragPos, joystickTouchpos);
        if (joystickDist<joystickRadius)
        {
            Joystick.transform.position = joystickTouchpos + joystickVec * joystickDist;
        }
        else
        {
            Joystick.transform.position = joystickTouchpos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        Joystick.transform.position = joystickOriginalpos;
        JoystickBG.transform.position = joystickOriginalpos;
    }

}
