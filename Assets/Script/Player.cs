using UnityEngine;
using AYellowpaper.SerializedCollections;
using AddClass;
using Photon.Realtime;
using NUnit.Framework;
using System.Collections.Generic;

public class Player : GenericChara.Chara
{
    [NonEditable] public Vector3 respawnPosition;
    [NonEditable] public Parameter score = new Parameter();
    [field: SerializeField] public Camp camp { get; set; } = new Camp();
    [field: SerializeField, NonEditable] public TsukiOtoshiInput input { get; private set; }
    public Vector3 beforeMoveInput { get; private set; }
    [field: SerializeField] public Parameter remainingBullets { get; private set; } = new Parameter();
    [field: SerializeField, NonEditable] public TPSViewPoint viewPoint { get; private set; }
    [field: SerializeField] public MomentaryBarAndDelayedBar rotateBar { get; private set; }
    public Rakuin_MotionManager motionManager { get; private set; }
    [field: SerializeField, NonEditable] public bool grounding { get; private set; }
    public GeneralMotion currentMotionState
    {
        get
        {
            return motionManager.motionDictionary.currentState;
        }
    }

    /// <summary>
    /// Guard�X�e�[�g/GuardKnockBack�X�e�[�g�Ȃ�
    /// </summary>
    public bool guard
    {
        get
        {

            if (currentMotionState == GeneralMotion.Guard)
            {
                return true;
            }
            else if (currentMotionState == GeneralMotion.GuardKnockBack)
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Step�܂���Down��ԂŖ��G���ԓ��Ȃ�
    /// </summary>
    public bool invinsible   
    {
        get
        {
            switch(motionManager.motionDictionary.currentState)
            {
                case GeneralMotion.Step:
                    if(stepInvisible.IsInRange(motionManager.motionDictionary.currentMotion.currentMotionTime.ratio))
                    {
                        return true;
                    }
                    break;
                case GeneralMotion.Down:
                    return true;

            }

            return false;
        }
    }
    public ValueInRange stepInvisible = new ValueInRange();
    [field: SerializeField, NonEditable] public Camera playerCamera { get; private set; }

    private void Awake()
    {
        remainingBullets.AssingEntityByMax();
        aliveAction += AliveAction;

    }
    protected override void Start()
    {
        base.Start();

        motionManager = GetComponent<Rakuin_MotionManager>();
        motionManager.durationAction += MoveVelocityNormalize;

        input = GetComponent<TsukiOtoshiInput>();
        viewPoint = GetComponent<TPSViewPoint>();
        playerCamera = transform.parent.GetComponentInChildren<Camera>();


        InitializePosition();
        BodyRotate();
    }

    private void AliveAction()
    {
        OverFallingLimit();
        assignSpeed = speed.entity;
        stepInvisible.Update(motionManager.motionDictionary.dicMotions[GeneralMotion.Step].currentMotionTime.ratio);
        viewPoint.ChangeSeeSawAngle(input.viewPointInput.entity);
        BodyRotate();
    }

    private void OverFallingLimit()
    {
        if(GameSceneOperator.instance.isOverFallLimit(this) == true)
        {
            if(lastAttacker == null)
            {
                DestroyOnSelfPenalty();
            }
            else
            {
                if(lastAttacker.TryGetComponent(out Player _lastAttacker) == true)
                {
                    _lastAttacker.Defeat();
                }
            }

            InitializePosition();
        }
    }

    /// <summary>
    /// ���Ă���Player��������s�����
    /// </summary>
    public void Defeat()
    {
        score.Update(1);
    }

    private void DestroyOnSelfPenalty()
    {
        score.Update(-1);
    }

    private void InitializePosition()
    {
        transform.position = respawnPosition;
    }

    /// <summary>
    /// �J�����̌����Ă�������Ɉړ������𐳋K������
    /// </summary>
    public void MoveVelocityNormalize()
    {

        if (input.moveInput.entity != Vector3.zero && beforeMoveInput != Vector3.zero)
        {
            Transform camTra = viewPoint.cam.transform;

            // ������`�����x�N�g���ɕϊ�
            Vector3 nor;
            nor = (camTra.right * input.moveInput.entity.x) + (camTra.forward * input.moveInput.entity.z);
            nor.y = 0;
            nor.Normalize();

            Vector2 modelRotateVec = new Vector2(nor.x, nor.z);
            rotateBar.RotateByVec2(modelRotateVec);

            AddAssignedMoveVelocity(nor);
        }

        beforeMoveInput = input.moveInput.entity;

    }

    /// <summary>
    /// Model�̕�����rotateBar�̒��_�Ɍ�����
    /// </summary>
    private void BodyRotate()
    {
        transform.LookAt(rotateBar.delayedBar.apex);
    }

    public void FootColliderEnter()
    {
        engine.gravityActive = false;
        grounding = true;
    }
    public void FootColliderExit()
    {
        engine.gravityActive = true;
        grounding = false;
    }


    /// <summary>
    /// �����蔻�肩����s
    /// </summary>
    /// <param name="_enemy"></param>
    public void Damage(Player _enemy)
    {
        lastAttacker = _enemy.GetComponent<GenericChara.Chara>();
        if (guard == true)
        {
            motionManager.motionDictionary.NextStatePlan(GeneralMotion.GuardKnockBack);
        }
        else
        {
            motionManager.motionDictionary.NextStatePlan(GeneralMotion.Down);
        }

        // �H���������������(����Ɠ��������������A����]����)
        Vector3 newEuler = _enemy.transform.eulerAngles;
        newEuler.y += 180;

        rotateBar.RotateByEuler(newEuler);
    }


    public Transform AnglePlan
    {
        get
        {
            return rotateBar.momentaryBar.apex;
        }
    }
}


public enum Camp
{
    Player1,
    Player2,
}
