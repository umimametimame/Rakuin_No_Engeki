using AddClass;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum CutInType
{
    /// <summary>
    /// 特定のモーションを指定し、そこに割り込みを追加する場合
    /// </summary>
    Handy,
    /// <summary>
    /// 特定のモーションを指定し、そこからのみ割り込める場合
    /// </summary>
    ReverseHandy,
    /// <summary>
    /// 自身を含め全てから割り込める場合
    /// </summary>
    IsAll,
    /// <summary>
    /// 自身以外の全てから割り込める場合
    /// </summary>
    OtherMyself,
}

public enum GeneralMotion
{
    // 持続系モーションは1ケタ
    Free,
    Guard,
    Fall,

    // 単発系モーションは2ケタ
    LargeShot = 10,
    ShiftLargeShot,
    LongShot,
    ShiftLongShot,
    Step,
    ThePassive,
    Reload,
    GuardKnockBack,

    // 受けモーション（自身から発動できない）
    Down = 100,

    // その他モーションは負
    None = -1,
}
public static class GeneralMotionState
{
    /// <summary>
    /// Stateが受動的か
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static bool IsReserveState(GeneralMotion state)
    {
        switch (state)
        {
            case GeneralMotion.Free:
                return true;
            case GeneralMotion.Fall:
                return true;
            case GeneralMotion.ShiftLargeShot:
                return true;
            case GeneralMotion.ShiftLongShot:
                return true;
            //case GeneralMotion.Down:
            //    return true;
            case GeneralMotion.None:
                return true;
        }

        return false;

    }

}


[Serializable]
public class TransitionalMotionThreshold
{
    /// <summary>
    /// beforeMotionのどこから派生できるか
    /// </summary>
    [field: SerializeField] public GeneralMotion beforeMotion { get; set; }
    [field: SerializeField] public Range thresholdRatio { get; set; } = new Range();

    public bool IsReaching
    {
        get
        {
            return thresholdRatio.IsReaching;
        }
    }
}

[Serializable] public class MotionState
{
    [field: SerializeField, NonEditable] public GeneralMotion state { get; protected set; }
    [field: SerializeField, NonEditable] public List<TransitionalMotionThreshold> transitionalPeriod { get; set; }
    [field: SerializeField, NonEditable] public List<Range> actionByTimeRange { get; set; } = new List<Range>();
    [field: SerializeField, NonEditable] public float motionTime { get; protected set; }
    [field: SerializeField, NonEditable] public Interval currentMotionTime { get; set; }
    [field: SerializeField, NonEditable] public Exist exist { get; set; } = new Exist();
    public Action cutIn { get; set; }

    [SerializeField] protected List<Instancer> effectInstancer = new List<Instancer>();
    public virtual void Initialize()
    {
        exist.enable += AddExistAction;
        void AddExistAction()
        {

            for (int i = 0; i < actionByTimeRange.Count; i++)
            {
                actionByTimeRange[i].Update(currentMotionTime.ratio);
            }
        }

        startAction += ResetAction;
        finishAction += ResetAction;
        cutIn += ResetAction;

        void ResetAction()
        {
            for (int i = 0; i < actionByTimeRange.Count; i++)
            {
                actionByTimeRange[i].Reset();
            }
        }
    }
    public virtual void Update()
    { 
        currentMotionTime.Update();
        exist.Update();
    }
    public void Start()
    {
        exist.Start();
    }
    public void Enable()
    {

    }
    public void Disable()
    {
        exist.Disable();
    }

    public void Reset()
    {
        currentMotionTime.Reset();
    }

    /// <summary>
    /// 現在のモーションが遷移ルートにあり、入力受付可能な場合
    /// </summary>
    /// <param name="currentMotion"></param>
    /// <returns></returns>
    public bool NextTransitional(MotionState currentMotion)
    {
        for (int i = 0; i < transitionalPeriod.Count; ++i)
        {
            if (currentMotion.state == transitionalPeriod[i].beforeMotion)
            {
                if (transitionalPeriod[i].IsReaching)
                {
                    return true;
                }
            }
        }

        return false;
    }
    public bool active
    {
        get
        {
            return !currentMotionTime.active;
        }
    }
    #region Existプロパティ
    public Action startAction
    {
        get { return exist.start; }
        set { exist.start = value; }
    }

    public Action enableAction
    {
        get { return exist.enable; }
        set { exist.enable = value; }
    }
    public Action finishAction
    {
        get { return currentMotionTime.reachAction; }
        set { currentMotionTime.reachAction = value; }
    }
    public Action disableAction
    {
        get { return exist.disable; }
        set { exist.disable = value; }
    }

    #endregion

    public void ThresholdReset()
    {
        for(int i = 0; i < transitionalPeriod.Count; ++i)
        {
            transitionalPeriod[i].thresholdRatio.Reset();
        }
    }

    public void ThresholdUpdate(float _ratio)
    {
        for (int i = 0; i < transitionalPeriod.Count; ++i)
        {
            transitionalPeriod[i].thresholdRatio.Update(_ratio);
        }

    }


}

[Serializable] public class DurationMotionState : MotionState
{

}


[Serializable] public class RigorMotionState : MotionState
{
    [field: SerializeField] public RigorMotionStateProfile profile { get; set; }
    public CutInType cutInType;
    
    public void AssignProfile(RigorMotionStateProfile newProfile = null)
    {
        if (newProfile != null)
        {
            profile = newProfile;
        }

        if (profile != null)
        {
            state = profile.state;
            cutInType = profile.cutInType;

            // 手動で指定したカットイン以外なら
            if(cutInType != CutInType.Handy)
            {
                ConvertTransitionalList();
            }
            else
            {
                transitionalPeriod = profile.transitionalPeriod;
                effectInstancer = profile.effectInstancers;
                actionByTimeRange = new List<Range>();
                for(int i = 0; i < profile.rangeBool.Count; ++i)
                {
                    Range newRange = new Range();
                    newRange.Initialize(profile.rangeBool[i]);
                    actionByTimeRange.Add(newRange);
                }
            }
            motionTime = profile.motionTime;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        AssignProfile(); 
        currentMotionTime.Initialize(false, true, motionTime);

        exist.disable += Reset;

        currentMotionTime.activeAction += exist.Disable;

    }

    private void ConvertTransitionalList()
    {
        List<GeneralMotion> states = ConvertEnums<GeneralMotion>.GetList();
        for (int i = 0; i < states.Count; ++i)
        {
            TransitionalMotionThreshold newTransitional = new TransitionalMotionThreshold();
            switch (cutInType)
            {
                case CutInType.ReverseHandy:
                    for(int j = 0; j < profile.transitionalPeriod.Count; j++)
                    {
                        if (states[i] != profile.transitionalPeriod[j].beforeMotion)
                        {
                            newTransitional.beforeMotion = states[i];
                            newTransitional.thresholdRatio.Initialize(-1, -1);
                            transitionalPeriod.Add(newTransitional);
                        }
                        else
                        {
                            newTransitional = profile.transitionalPeriod[j];
                            transitionalPeriod.Add(newTransitional);
                        }
                    }
                    break;
                case CutInType.IsAll:
                    newTransitional.beforeMotion = states[i];
                    newTransitional.thresholdRatio.Initialize(0, 1);
                    transitionalPeriod.Add(newTransitional);
                    break;

                case CutInType.OtherMyself:
                    if (states[i] != state)
                    {
                        newTransitional.beforeMotion = states[i];
                        newTransitional.thresholdRatio.Initialize(0, 1);
                        transitionalPeriod.Add(newTransitional);
                    }
                    break;
            }
        }
    }
}
public static class ConvertEnums<T1> where T1 : Enum
{
    public static Dictionary<T1, MotionState> GetDic()
    {
        Dictionary<T1, MotionState> newDic = new Dictionary<T1, MotionState>();
        foreach (T1 s in Enum.GetValues(typeof(T1)))
        {
            newDic.Add(s, new MotionState());
        }

        return newDic;
    }

    public static List<T1> GetList()
    {
        List<T1> newList = new List<T1>();
        foreach (T1 t in Enum.GetValues(typeof(T1)))
        {
            newList.Add(t);
        }

        return newList;

    }
}