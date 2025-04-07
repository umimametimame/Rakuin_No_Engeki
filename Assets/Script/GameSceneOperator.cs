
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneOperator : SceneOperator
{
    public static GameSceneOperator instance;
    [SerializeField] private Transform respownPosition_GuideObject;
    [SerializeField] private Instancer player;
    [SerializeField] private Transform fallLimitRange_GuideObject;
    [SerializeField] private Rule rule;
    protected override void Start()
    {
        for(int i = 0; i  < 2; i++)
        {

            player.Instance();
            Player _player = player.lastObj.GetComponentInChildren<Player>();
            DisplayByPlayer _display = player.lastObj.GetComponent<DisplayByPlayer>();

            _player.respawnPosition = respownPosition_GuideObject.position;
            _player.score.AssignMax(rule.maxScore);
            _player.score.AssingEntityByMax();

            _display.TargetDisplay = i;

            // 2�l��
            if (i == 1)
            {
                Vector3 respownVec = respownPosition_GuideObject.position;
                respownVec.x = -respownVec.x;
                respownVec.z = -respownVec.z;

                _player.respawnPosition = respownVec;

            }
            // 1�l��
            else if (i == 0)
            {
                _player.playerCamera.tag = Tags.MainCamera;

            }

            _player.camp = (Camp)i;
        }
    }

    private void Singleton()
    {
        if (instance == null)
        {
            instance = (GameSceneOperator)FindObjectOfType(typeof(GameSceneOperator));
            DontDestroyOnLoad(gameObject); // �ǉ�
        }
        else
        {
            Destroy(gameObject);

        }
    }

    protected override void Awake()
    {
        base.Awake();
        Singleton();
    }

    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Player�L�������������E���x��������Ă��邩
    /// </summary>
    /// <param name="_player"></param>
    /// <returns></returns>
    public bool isOverFallLimit(Player _player)
    {
        if(_player.transform.position.y <= fallLimitRange_GuideObject.position.y)
        {
            return true;
        }

        return false;
    }
}

[Serializable]
public struct Rule
{
    public float timeLimit;
    public int maxScore;
}