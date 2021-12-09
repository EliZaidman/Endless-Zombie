using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBetweenWeapaons : MonoBehaviour
{
    WeapaonStateMachine states = new WeapaonStateMachine(new DeafultWeapon());

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            states.Request();
        }

    }
}
