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
/// ÉÇÅ[ÉVÉáÉìñàÇÃìÆÇ´ÇPlayerì‡ä÷êîÇ≈ìoò^
/// </summary>
public class Rakuin_MotionManager : MonoBehaviour
{
    private Player player;
    private TsukiOtoshiInput input;
    [field: SerializeField] public Animator animator { get; private set; }
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

        AnimationSet_Rigor();
    }

    private void Update()
    {
        AnimationSet_Duration();
        advancedInput.Update();
        motionDictionary.Update();
        CaseGeneralMotion();
    }


    
    private void InitializeMotion()
    {
        List<Rakuin_RigorMotionState> rigorMotionStateList = new List<Rakuin_RigorMotionState>()
        {
            motionState_LargeShot,
            motionState_LongShot,
            motionState_Step,
            motionState_Reload,
            motionState_GuardKnockBack,
            motionState_Down,
        };

        for (int i = 0; i < rigorMotionStateList.Count; ++i)
        {
            rigorMotionStateList[i].AssignChara(player);
            rigorMotionStateList[i].AssignProfile();
            motionDictionary.dicRigor.Add(rigorMotionStateList[i].state, rigorMotionStateList[i]);
        }

        motionDictionary.thinkFunc += StateThink;
        motionDictionary.durationFunc += StateThink;
        motionDictionary.resetMotionAction += () => AddFunction.ResetAnimation(animator);
        motionDictionary.Initialize();
        motionDictionary.setAnimatorAction = AnimationSet_Rigor;

        foreach (var d in input.dicInterval)
        {
            advancedInput.Add(d.Value, () => ChangeState(d.Key), d.Key);
            advancedInput.dicAdvanced[d.Key].funcs += () => motionDictionary.Transitional(d.Key);
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

    private void ChangeState(GeneralMotion newState)
    {
        motionDictionary.ChangeState(newState);
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

