using AddClass;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Rakuin_RigorMotionState : RigorMotionState
{
    public void AssignChara(Player _player)
    {
        player = _player;
    }

    public Player player { get; private set; }

    /// <summary>
    /// player.motionManager
    /// </summary>
    public Rakuin_MotionManager motionManager
    { 
        get
        {
            return player.motionManager;
        }
    }
}
[Serializable]
public class Motion_Shot : Rakuin_RigorMotionState
{
    public override void Initialize()
    {
        base.Initialize();
        startAction += StartAction;
        for (int i = 0; i < actionByTimeRange.Count; i++)
        {
            actionByTimeRange[i].Initialize();
        }
    }
    public void StartAction()
    {

        if (player.remainingBullets.CostJudge() == true)
        {
            for(int i = 0; i < effectInstancer.Count; i++)
            {

                GameObject clone = effectInstancer[i].Instance(player.transform);
                clone.transform.eulerAngles = player.transform.eulerAngles;

                Bullet _bullet = clone.GetComponent<Bullet>();
                _bullet.parent = player;
            }

            player.remainingBullets.Update(-1);
        }
        else
        {

        }
    }
}

[Serializable]
public class Motion_Reload : Rakuin_RigorMotionState
{
    public override void Initialize()
    {
        base.Initialize();
        startAction += Start_Reload;


        for(int i = 0; i < actionByTimeRange.Count; i++)
        {
            actionByTimeRange[i].Initialize();
            actionByTimeRange[i].withinRangeAction.Add(effectInstancer[i].Instance);
        }
    }

    private void Start_Reload()
    {
        player.remainingBullets.Initialize();
    }
}
[Serializable]
public class Motion_Step : Rakuin_RigorMotionState
{
    public RatioCurve stepSpeed = new RatioCurve();
    public override void Initialize()
    {
        stepSpeed.AssignProfile();
        base.Initialize(); 
        startAction += player.MoveVelocityNormalize;
        exist.enable += Start_Step;
        for (int i = 0; i < actionByTimeRange.Count; i++)
        {
            actionByTimeRange[i].Initialize();
            actionByTimeRange[i].withinRangeAction.Add(effectInstancer[i].Instance);
        }
    }

    private void Start_Step()
    {
        Vector3 newNor = (-player.transform.forward) * stepSpeed.Evalute(motionManager.motionDictionary.dicMotions[GeneralMotion.Step].currentMotionTime.ratio);

        player.AddMoveVelocity(newNor);
    }
}
[Serializable]
public class Motion_ThePassive : Rakuin_RigorMotionState
{
    public RatioCurve thePassiveSpeed = new RatioCurve();
    public override void Initialize()
    {
        thePassiveSpeed.AssignProfile();
        base.Initialize();
        exist.enable += Enable_ThePassive;
        for (int i = 0; i < actionByTimeRange.Count; i++)
        {
            actionByTimeRange[i].Initialize();
        }
    }

    private void Enable_ThePassive()
    {
        Vector3 newNor = (-player.transform.forward) * thePassiveSpeed.Evalute(motionManager.motionDictionary.dicMotions[GeneralMotion.ThePassive].currentMotionTime.ratio);

        player.AddMoveVelocity(newNor);
    }
}
[Serializable]
public class Motion_GuardKnockBack : Rakuin_RigorMotionState
{
    public RatioCurve knockBackSpeed = new RatioCurve();
    public override void Initialize()
    {
        knockBackSpeed.AssignProfile();
        base.Initialize();
        exist.enable += Enable_Down;
        for (int i = 0; i < actionByTimeRange.Count; i++)
        {
            actionByTimeRange[i].Initialize();
        }

    }

    private void Enable_Down()
    {
        Vector3 newNor = (-player.transform.forward) * knockBackSpeed.Evalute(motionManager.motionDictionary.dicMotions[GeneralMotion.GuardKnockBack].currentMotionTime.ratio);
        
        player.AddMoveVelocity(newNor);
    }
}


[Serializable]
public class Motion_Down : Rakuin_RigorMotionState
{
    public RatioCurve downSpeed_x = new RatioCurve();
    public RatioCurve downSpeed_y = new RatioCurve();
    public override void Initialize()
    {
        downSpeed_x.AssignProfile();
        downSpeed_y.AssignProfile();
        base.Initialize();
        exist.enable += Enable_Down;
        for (int i = 0; i < actionByTimeRange.Count; i++)
        {
            actionByTimeRange[i].Initialize();
        }

    }

    private void Enable_Down()
    {
        Vector3 newNor = (-player.transform.forward) * downSpeed_x.Evalute(motionManager.motionDictionary.dicMotions[GeneralMotion.Down].currentMotionTime.ratio);
        newNor += player.transform.up * downSpeed_y.Evalute(motionManager.motionDictionary.dicMotions[GeneralMotion.Down].currentMotionTime.ratio);

        player.AddMoveVelocity(newNor);
    }
}

