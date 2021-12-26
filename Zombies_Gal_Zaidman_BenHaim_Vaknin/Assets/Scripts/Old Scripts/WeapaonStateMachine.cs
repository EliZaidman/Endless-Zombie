using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeapaonStateMachine : MonoBehaviour
{
    
    [SerializeField] private StateBase _state;
    
    //[SerializeField] private Sprite deafultWeapon;
    //[SerializeField] private Sprite FastWeapon;
    //[SerializeField] private Sprite SlowWeapon;
    [SerializeField] private Fire _fire;

    public WeapaonStateMachine(StateBase state)
    {

        _state = state;
    }

    public void Request()
    {
        _state.Handle(this);
    }

    public StateBase State
        
    {

        get { return _state; }
        set { _state = value; }
    }

    private void Update()
    {
       
    }
}


public abstract class StateBase : MonoBehaviour
{
    
    public abstract void Handle(WeapaonStateMachine context);
    public Fire fire;
}


public class DeafultWeapon : StateBase
{
   
    public override void Handle(WeapaonStateMachine context)
    {
        Debug.Log("DeafultWeapon");
        context.State = new FastWeapon();
    }
}


public class FastWeapon : StateBase
{
    
    public override void Handle(WeapaonStateMachine context)
    {
        Debug.Log("FastWeapon");
        context.State = new SlowWeapon();  
    }
}

public class SlowWeapon : StateBase
{
    public override void Handle(WeapaonStateMachine context)
    {
        Debug.Log("SlowWeapon");
        context.State = new DeafultWeapon();
    }
}

