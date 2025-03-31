using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TsukiOtoshiInput : InputOperator
{
    [field: SerializeField, NonEditable] public InputVecOrFloat<float> guardInput { get; private set; } = new InputVecOrFloat<float>();
    [field: SerializeField, NonEditable] public InputVecOrFloat<float> shiftInput { get; private set; } = new InputVecOrFloat<float>();
    /// <summary>
    /// PlayerInput‚ÌButton“ü—Í‚ÌList
    /// </summary>
    [field: SerializeField] public SerializedDictionary<GeneralMotion, Interval> dicInterval { get; private set; } = new SerializedDictionary<GeneralMotion, Interval>();

    private void Awake()
    {
        SetInputsList();
        Initialize();

        SetButtonInputsList();
    }
    private void Start()
    {
        shiftInput.Initialize();
    }

    private void SetButtonInputsList()
    {

        Dictionary<GeneralMotion, Interval> newDic = AddClass.ConvertEnums<GeneralMotion, Interval>.GetDic();
        foreach (var n in newDic)
        {
            GeneralMotion addMotion = n.Key;
            if(GeneralMotionState.IsReserveState(addMotion) == false)
            {
                dicInterval.Add(addMotion, new Interval());
                dicInterval[addMotion].Initialize(true, false, 0.3f);
            }
        }

    }

    protected override void Update()
    {
        base.Update();
        foreach(var d in dicInterval)
        {
            d.Value.Update();
        }


        if (Guard == true)
        {
            dicInterval[GeneralMotion.Guard].Reset(dicInterval[GeneralMotion.Guard].interval / 2);
        }
    }


    #region InputEvent
    public void OnGuard(InputValue value)
    {
        guardInput.entity = value.Get<float>();
    }

    public void OnShift(InputValue value)
    {
        shiftInput.entity = value.Get<float>();
    }

    public void OnLargeShot(InputValue value)
    {
        if(Shift == false)
        {
            dicInterval[GeneralMotion.LargeShot].Reset();
        }
        //else
        //{
        //    dicInterval[GeneralMotion.ShiftLargeShot].Reset();
        //}
    }
    public void OnLongShot(InputValue value)
    {
        if (Shift == false)
        {
            dicInterval[GeneralMotion.LongShot].Reset();
        }
        //else
        //{
        //    dicInterval[GeneralMotion.ShiftLongShot].Reset();
        //}
    }

    public void OnStep(InputValue value)
    {
        if(Shift == false)
        {
            dicInterval[GeneralMotion.Step].Reset();
        }
        else
        {
            dicInterval[GeneralMotion.ThePassive].Reset();
        }
    }
    public void OnReload(InputValue value)
    {
        if(Shift == false)
        {
            dicInterval[GeneralMotion.Reload].Reset();
        }
        else
        {
            dicInterval[GeneralMotion.Down].Reset();
        }
    }

    #endregion

    public bool Shift
    {
        get
        {
            return shiftInput.inputting;
        }
    }

    public bool Guard
    {
        get
        {
            return guardInput.inputting;
        }
    }
}

public static class RigorStr
{
    public static string LargeShot = nameof(LargeShot);
    public static string ShiftLargeShot = nameof(ShiftLargeShot);
    public static string LongShot = nameof(LargeShot);
    public static string ShiftLongShot = nameof(ShiftLongShot);

    public static List<string> shots = new List<string>() { LargeShot,  ShiftLargeShot, LongShot, ShiftLongShot };

    public static string Step = nameof(Step);
}


[Serializable] public class TsukiOtoshiInterval : Interval
{
    [field: SerializeField] public GeneralMotion motion { get; set; }
}