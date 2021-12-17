using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLocation
{
    public GameObject Touchable;
    public int TouchId;

    public TouchLocation(int newTouchId, GameObject newGO)
    {
        TouchId = newTouchId;
        Touchable = newGO;
    }
}
