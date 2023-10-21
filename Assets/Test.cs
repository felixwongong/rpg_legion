using System;
using System.Collections.Generic;
using CofyEngine;
using UnityEngine;
using UnityEngine.Events;

public class Test: MonoBehaviour
{
    SmartEvent<int> _event = new SmartEvent<int>();
    
    private IRegistration _reg1;
    private IRegistration _reg2;
    private void Start()
    {
        RegFirst();
        GC.Collect();
        _event.Invoke(2);

        _reg1 = null;
        GC.Collect();
        _event.Invoke(3);
    }

    public void RegFirst()
    {
        _reg1 = _event.AddListener(x => Debug.Log($"reg1: {x}"));
        _reg2 = _event.AddListener(x => Debug.Log($"reg2: {x}"));
        var localReg = _event.AddListener(x => Debug.Log($"localReg: {x}"));
        _event.Invoke(1);
    }
}


