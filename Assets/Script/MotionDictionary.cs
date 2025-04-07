using AddClass;
using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable] public class MotionDictionary 
{
    [field: SerializeField] public GeneralMotion currentState {  get; private set; }
    [field: SerializeField, NonEditable] public MotionState currentMotion { get; private set; }
    public ValueChecker<GeneralMotion> stateObserver { get; private set; } = new ValueChecker<GeneralMotion>();

    [field: SerializeField] public SerializedDictionary<GeneralMotion, MotionState> dicDuration { get; set; } = new SerializedDictionary<GeneralMotion, MotionState>();
    [field: SerializeField] public SerializedDictionary<GeneralMotion, MotionState> dicRigor { get; set; } = new SerializedDictionary<GeneralMotion, MotionState>();
    [field: SerializeField] public SerializedDictionary<GeneralMotion, MotionState> dicMotions = new SerializedDictionary<GeneralMotion, MotionState>();
    public Func<GeneralMotion> thinkFunc { get; set; }
    public Func<GeneralMotion> durationFunc { get; set; }
    public Action resetMotionAction { get; set; }
    public Action setAnimatorAction { get; set; }
    public void Initialize()
    {
        currentState = GeneralMotion.Free;
        stateObserver.Initialize(currentState);
        stateObserver.changedAction += Event_StateChanged;
        stateObserver.changedAction += Event_StateCutIn;

        InitializeDicMotion();
        Think();
    }

    public void InitializeDicMotion()
    {
        foreach(var d in dicDuration)
        {
            d.Value.Initialize();
            dicMotions.Add(d.Value.state, d.Value);
        }
        foreach(var r in dicRigor)
        {
            r.Value.Initialize();
            r.Value.finishAction += Think;
            dicMotions.Add(r.Value.state, r.Value);
        }
        foreach(var m in dicMotions)
        {
            m.Value.cutIn += m.Value.Reset;
        }

    }

    private void Think()
    {
        NextStatePlan(thinkFunc.Invoke());
    }

    /// <summary>
    /// ���[�V�����؂�ւ�
    /// </summary>
    /// <param name="nextState"></param>
    public void NextStatePlan(GeneralMotion nextState)
    {
        currentState = nextState;
        currentMotion = dicMotions[currentState];
        currentMotion.Start();
        currentMotion.Reset();
        setAnimatorAction?.Invoke();
    }

    public void Update()
    {
        stateObserver.Update(currentState);
        DurationMotionUpdate();
        DicMotionUpdate();

    }

    private void Event_StateChanged()
    {
        currentMotion = dicMotions[currentState];
        currentMotion.Start();
        currentMotion.Reset();
        setAnimatorAction?.Invoke();
    }
    private void Event_StateCutIn()
    {
        if (StateIsRigor(stateObserver.beforeValue) == true)
        {
            dicMotions[stateObserver.beforeValue].cutIn?.Invoke();
        }
    }

    /// <summary>
    /// currentMotion�������n�̏ꍇ
    /// <br/>���Ԃ�l���قȂ�ꍇ��ChangeState
    /// </summary>
    private void DurationMotionUpdate()
    {
        if (CurrentStateIsRigor == false)
        {
            if(durationFunc.Invoke() != currentState)
            {
                NextStatePlan(durationFunc.Invoke());

            }
        }
    }

    private void DicMotionUpdate()
    {

        foreach (var d in dicMotions)
        {
            // ���s���̃��[�V�����X�V����
            if (currentState == d.Value.state)
            {

            }
            // ����s���̃��[�V�����X�V����
            else
            {
                d.Value.Disable();
                // State�ύX����̂ݕύX����C�x���g�`���ɂ�����
            }
            // ���ʍX�V����
            d.Value.Update();

            List<TransitionalMotionThreshold> transitionalList = d.Value.transitionalPeriod;
            for(int i = 0; i < transitionalList.Count; ++i)
            {
                if (transitionalList[i].beforeMotion == currentState)
                {
                    transitionalList[i].thresholdRatio.Update(currentMotion.currentMotionTime.ratio);
                }
            }

        }
    }

    public bool StateIsRigor(GeneralMotion state)
    {
        foreach(var d in dicRigor)
        {
            if(state == d.Value.state)
            {
                return true;
            }
        }

        return false;
    }
    public bool CurrentStateIsRigor
    {
        get
        {
            foreach(var d in dicRigor)
            {
                if(currentState == d.Value.state)
                {
                    return true;
                }

            }

            return false;
        }
    }

    /// <summary>
    /// �h���\���[�V�����Ȃ�true
    /// </summary>
    /// <param name="nextMotionState"></param>
    /// <returns></returns>
    public bool Transitional(GeneralMotion nextMotionState)
    {
        //if (dicMotions[nextMotionState].)
        if (currentState == GeneralMotion.Free)
        {
            return true;
        }

        List<TransitionalMotionThreshold> list = dicMotions[nextMotionState].transitionalPeriod;
        for (int i = 0; i < list.Count; ++i)
        {
            if(currentState == list[i].beforeMotion)
            {
                if (list[i].IsReaching == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

}
