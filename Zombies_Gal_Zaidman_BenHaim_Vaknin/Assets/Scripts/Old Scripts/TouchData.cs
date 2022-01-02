using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchData
{
    public Vector2 StartTouchPos;
    public int TouchId;

    public TouchData(int newTouchId, Vector2 newTouchPos)
    {
        StartTouchPos = newTouchPos;
        TouchId = newTouchId;
    }
}
