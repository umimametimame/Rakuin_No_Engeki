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
    /// モーション切り替え
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
    /// currentMotionが持続系の場合
    /// <br/>且つ返り値が異なる場合にChangeState
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
            // 実行中のモーション更新処理
            if (currentState == d.Value.state)
            {

            }
            // 非実行中のモーション更新処理
            else
            {
                d.Value.Disable();
                // State変更直後のみ変更するイベント形式にしたい
            }
            // 共通更新処理
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
    /// 派生可能モーションならtrue
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
