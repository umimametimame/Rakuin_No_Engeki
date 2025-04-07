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
    /// ����̃��[�V�������w�肵�A�����Ɋ��荞�݂�ǉ�����ꍇ
    /// </summary>
    Handy,
    /// <summary>
    /// ����̃��[�V�������w�肵�A��������̂݊��荞�߂�ꍇ
    /// </summary>
    ReverseHandy,
    /// <summary>
    /// ���g���܂ߑS�Ă��犄�荞�߂�ꍇ
    /// </summary>
    IsAll,
    /// <summary>
    /// ���g�ȊO�̑S�Ă��犄�荞�߂�ꍇ
    /// </summary>
    OtherMyself,
}

public enum GeneralMotion
{
    // �����n���[�V������1�P�^
    Free,
    Guard,
    Fall,

    // �P���n���[�V������2�P�^
    LargeShot = 10,
    ShiftLargeShot,
    LongShot,
    ShiftLongShot,
    Step,
    ThePassive,
    Reload,
    GuardKnockBack,

    // �󂯃��[�V�����i���g���甭���ł��Ȃ��j
    Down = 100,

    // ���̑����[�V�����͕�
    None = -1,
}
public static class GeneralMotionState
{
    /// <summary>
    /// State���󓮓I��
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
    /// beforeMotion�̂ǂ�����h���ł��邩
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
    /// <summary>
    /// interval�����̏ꍇ�����n���[�V����
    /// </summary>
    [field: SerializeField, NonEditable] public Interval currentMotionTime { get; set; }
    [field: SerializeField, NonEditable] public Exist exist { get; set; } = new Exist();
    public Action cutIn { get; set; }
    [field: SerializeField] public MotionStateProfile profile { get; set; }
    public CutInType cutInType;

    [SerializeField] protected List<Instancer> effectInstancer = new List<Instancer>();
    public virtual void Initialize()
    {
        exist.enable += Enable_Exist;
        startAction += ResetAction;
        finishAction += ResetAction;
        cutIn += ResetAction;

        currentMotionTime.Initialize(false, true, 0.0f);
        AssignProfile();

        if(isDuration == true)
        {

        }
        else
        {
            currentMotionTime.activeAction += exist.Disable;
        }
        exist.disable += Reset;
    }

    public void Update()
    { 
        currentMotionTime.Update();
        exist.Update();
    }
    public void Start()
    {
        exist.Start();
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
    /// ���[�V�����ɐݒ肳�ꂽ�^�C�~���O�Ŋ֐������s
    /// </summary>
    void Enable_Exist()
    {

        for (int i = 0; i < actionByTimeRange.Count; i++)
        {
            actionByTimeRange[i].Update(currentMotionTime.ratio);
        }
    }
    void ResetAction()
    {
        for (int i = 0; i < actionByTimeRange.Count; i++)
        {
            actionByTimeRange[i].Reset();
        }
    }

    public void AssignProfile(MotionStateProfile newProfile = null)
    {
        if (newProfile != null)
        {
            profile = newProfile;
        }

        if (profile != null)
        {
            state = profile.state;
            cutInType = profile.cutInType;

            // �蓮�Ŏw�肵���J�b�g�C���ȊO�Ȃ�
            if (cutInType != CutInType.Handy)
            {
                ConvertTransitionalList();
            }
            else
            {
                TransitionalMotionThreshold add = new TransitionalMotionThreshold();
                
                
                transitionalPeriod = profile.GetTransitionalList;
                effectInstancer = profile.effectInstancers;
                actionByTimeRange = new List<Range>();
                for (int i = 0; i < profile.rangeBool.Count; ++i)
                {
                    Range newRange = new Range();
                    newRange.Initialize(profile.rangeBool[i]);
                    actionByTimeRange.Add(newRange);
                }
            }
            currentMotionTime.interval = profile.motionTime;
        }
    }
    private void ConvertTransitionalList()
    {
        List<GeneralMotion> states = ConvertEnums<GeneralMotion>.GetList();
        transitionalPeriod = new List<TransitionalMotionThreshold>();
        
        TransitionalMotionThreshold newTransitional = new TransitionalMotionThreshold();
        switch (cutInType)
        {
            case CutInType.ReverseHandy:

                for (int i = 0; i < states.Count; ++i)
                {
                    newTransitional = new TransitionalMotionThreshold();
                    newTransitional.thresholdRatio.Initialize(0, 1);
                    newTransitional.beforeMotion = states[i];
                    transitionalPeriod.Add(newTransitional);
                    for (int j = 0; j < profile.motionStateValueList.Count; j++)
                    {
                        if (states[i] == profile.motionStateValueList[j].beforeMotion)
                        {
                            transitionalPeriod.RemoveAt(transitionalPeriod.Count - 1);
                        }
                    }
                }
                break;
            case CutInType.IsAll:
                for (int i = 0; i < states.Count; ++i)
                {
                    newTransitional = new TransitionalMotionThreshold();
                    newTransitional.beforeMotion = states[i];
                    newTransitional.thresholdRatio.Initialize(0, 1);
                    transitionalPeriod.Add(newTransitional);
                }
                break;

            case CutInType.OtherMyself:
                for (int i = 0; i < states.Count; ++i)
                {
                    newTransitional = new TransitionalMotionThreshold();
                    if (states[i] != state)
                    {
                        newTransitional.beforeMotion = states[i];
                        newTransitional.thresholdRatio.Initialize(0, 1);
                        transitionalPeriod.Add(newTransitional);
                    }
                }
                break;
        }
        
    }
    #region Exist�v���p�e�B
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

    /// <summary>
    /// ���[�V�������Ԃ����Ȃ�true
    /// </summary>
    public bool isDuration
    {
        get
        {
            if(currentMotionTime.interval <= 0)
            {
                return true;
            }

            return false;
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