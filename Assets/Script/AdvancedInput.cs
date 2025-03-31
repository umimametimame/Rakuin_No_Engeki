using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class AdvancedInput
{
    public BoolFuncs funcs { get; set; } = new BoolFuncs();
    public Action action { get; set; }
    
    public Interval input { get; private set; } = new Interval();
    public bool enable;


    public void Initialize(Interval interval)
    {
        input = interval;
    }
    public void Update()
    {
        enable = (!input.active);
        if(enable == true)
        {
            if (funcs.Invoke() == true)
            {
                action?.Invoke();
                input.Reset();
            }
        }

    }

    /// <summary>
    /// ”­“®‰Â”\‚©‚ð•Ô‚·
    /// </summary>
    public bool Executable
    {
        get
        {
            if(!input.active == true)
            {
                if (funcs.Invoke() == true)
                {
                    return true;
                }

            }

            return false;
        }
    }

    public void FuncReset()
    {
        funcs = null;
    }
}
