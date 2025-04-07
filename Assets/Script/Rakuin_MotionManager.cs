using AddClass;
using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XR;

/// <summary>
/// モーション毎の動きをPlayer内関数で登録
/// </summary>
public class Rakuin_MotionManager : MonoBehaviour
{
    private Player player;
    private TsukiOtoshiInput input;
    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField, NonEditable] public ValueChecker<bool> groundingChecker { get; private set; }

    private bool grounding
    {
        get
        {
            return player.grounding;
        }
    }
    private bool guarding
    {
        get
        {
            return input.guardInput.inputting;
        }
    }
    private bool shift
    {
        get
        {
            return input.shiftInput.inputting;
        }
    }
    public MotionAdvancedInput<GeneralMotion> advancedInput { get; private set; } = new MotionAdvancedInput<GeneralMotion>();
    public MotionDictionary motionDictionary = new MotionDictionary();

    public Action durationAction;
    public Action rigorAction;

    public Motion_Guard motion_Guard = new Motion_Guard();
    public Motion_Fall motion_Fall = new Motion_Fall();

    public Motion_Shot motionState_LargeShot = new Motion_Shot();
    public Motion_Shot motionState_LongShot = new Motion_Shot();
    public Motion_Step motionState_Step = new Motion_Step();
    public Motion_Reload motionState_Reload = new Motion_Reload();
    public Motion_ThePassive motionState_ThePassive = new Motion_ThePassive();
    public Motion_GuardKnockBack motionState_GuardKnockBack = new Motion_GuardKnockBack();
    public Motion_Down motionState_Down = new Motion_Down();
    private void Start()
    {
        player = GetComponent<Player>();
        input = GetComponent<TsukiOtoshiInput>();
        InitializeMotion();
        groundingChecker.Initialize(player.grounding);
        groundingChecker.changedAction += Event_FallByGrounding;

        AnimationSet_Rigor();
    }

    private void Update()
    {
        groundingChecker.Update(player.grounding);
        AnimationSet_Duration();
        advancedInput.Update();
        motionDictionary.Update();
        CaseGeneralMotion();
    }


    
    private void InitializeMotion()
    {
        List<Rakuin_MotionState> rakuin_MotioStateList = new List<Rakuin_MotionState>()
        {
            motion_Guard,
            motion_Fall,
            motionState_LargeShot,
            motionState_LongShot,
            motionState_Step,
            motionState_Reload,
            motionState_GuardKnockBack,
            motionState_Down,
        };

        for (int i = 0; i < rakuin_MotioStateList.Count; ++i)
        {
            rakuin_MotioStateList[i].Initialize(player);
            rakuin_MotioStateList[i].AssignProfile();
            if (rakuin_MotioStateList[i].isDuration == true)
            {
                motionDictionary.dicDuration.Add(rakuin_MotioStateList[i].state, rakuin_MotioStateList[i]);
            }
            else
            {

                if (rakuin_MotioStateList[i].inertia.isInertiaDuration == true)
                {
                    // inertia.durationTime.intervalをモーション時間に設定
                    rakuin_MotioStateList[i].inertia.durationTime.interval = rakuin_MotioStateList[i].currentMotionTime.interval;
                }
                motionDictionary.dicRigor.Add(rakuin_MotioStateList[i].state, rakuin_MotioStateList[i]);
            }
        }

        motionDictionary.thinkFunc += StateThink;
        motionDictionary.durationFunc += StateThink;
        motionDictionary.resetMotionAction += () => AddFunction.ResetAnimation(animator);
        motionDictionary.Initialize();
        motionDictionary.setAnimatorAction = AnimationSet_Rigor;

        foreach (var d in input.dicInterval)
        {
            advancedInput.Add(d.Value, () => NextStatePlan(d.Key), d.Key);
            advancedInput.dicAdvanced[d.Key].funcs += () => motionDictionary.Transitional(d.Key);
        }

    }

    /// <summary>
    /// 接地と接地解除が行われる度に実行するイベント
    /// </summary>
    private void Event_FallByGrounding()
    {
        if(motionDictionary.currentState != GeneralMotion.Down)
        {
            if(motionDictionary.currentState != GeneralMotion.Fall)
            {
                NextStatePlan(GeneralMotion.Fall);

            }
        }

    }

    private GeneralMotion StateThink()
    {
        if (grounding == false && motionDictionary.CurrentStateIsRigor == false)
        {
            return GeneralMotion.Fall;
        }
        else if (guarding == true)
        {
            return GeneralMotion.Guard;
        }
        else
        {
            return GeneralMotion.Free;
        }

    }
    private void CaseGeneralMotion()
    {
        if(motionDictionary.CurrentStateIsRigor == false)
        {
            durationAction?.Invoke();
        }
        else
        {
            rigorAction?.Invoke();
        }

    }

    private void NextStatePlan(GeneralMotion newState)
    {
        motionDictionary.NextStatePlan(newState);
    }
    private void AnimationSet_Rigor()
    {
        animator.SetInteger(Animator.StringToHash("MotionState"), (int)motionDictionary.currentState);
        animator.SetTrigger(Animator.StringToHash("CanTransitionToSelfTrigger"));
    }

    private void AnimationSet_Duration()
    {
        animator.SetBool(Animator.StringToHash("MoveInput"), input.moveInput.inputting);
    }

}

