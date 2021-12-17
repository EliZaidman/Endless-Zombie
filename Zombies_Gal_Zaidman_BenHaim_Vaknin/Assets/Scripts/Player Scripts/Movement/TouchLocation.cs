using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocation
{
    public Vector2 StartTouchPos;
    public int TouchId;

    public TouchLocation(int newTouchId, Vector2 newTouchPos)
    {
        StartTouchPos = newTouchPos;
        TouchId = newTouchId;
    }
}
