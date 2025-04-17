
using AddUnityClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSceneOperator : SceneOperator
{
    public static GameSceneOperator instance;
    [SerializeField] private Instancer playerInstancer = new Instancer();
    [field: SerializeField] public List<PlayerInfo> playerList { get; private set; } = new List<PlayerInfo>();
    [SerializeField] private Transform respownPosition_GuideObject;
    [SerializeField] private Transform fallLimitRange_GuideObject;
    [SerializeField] private Rule rule;
    protected override void Start()
    {
        playerList = new List<PlayerInfo>();

        for(int i = 0; i  < 2; i++)
        {
            playerInstancer.Instance();
            PlayerInfo _playerInfo = new PlayerInfo();
            _playerInfo.userInfo = playerInstancer.lastObj.GetComponentInParent<UserInfo>();
            _playerInfo.player = playerInstancer.lastObj.GetComponentInChildren<Player>();
            _playerInfo.UI = playerInstancer.lastObj.GetComponentInChildren<Rakuin_UI>();
            List<Camera> _display = playerInstancer.lastObj.transform.root.GetComponentsInChildren<Camera>().ToList();

            playerList.Add(_playerInfo);

            _playerInfo.userInfo.Assign(i, "User" + (i + 1));
            _playerInfo.player.respawnPosition = respownPosition_GuideObject.position;
            _playerInfo.player.score.AssignMax(rule.maxScore);
            _playerInfo.player.instanceID = i;

            for(int j = 0; j < _display.Count; j++)
            {
                _display[j].targetDisplay = i;
            }

            // 2人目
            if (i == 1)
            {
                Vector3 respownVec = respownPosition_GuideObject.position;
                respownVec.x = -respownVec.x;
                respownVec.z = -respownVec.z;

                _playerInfo.player.respawnPosition = respownVec;

            }
            // 1人目
            else if (i == 0)
            {
                _playerInfo.player.playerCamera.tag = Tags.MainCamera;

            }

        }
    }

    private void Singleton()
    {
        if (instance == null)
        {
            instance = (GameSceneOperator)FindObjectOfType(typeof(GameSceneOperator));
            DontDestroyOnLoad(gameObject); // 追加
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
    /// Playerキャラが落下限界高度を下回っているか
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

    public PlayerInfo GetOtherPlayer(int _yourInstanceID)
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            if(i != _yourInstanceID)
            {
                return playerList[i]; 
            }
        }

        return null;
    }

    /// <summary>
    /// Playerが落下した時、Playerから実行
    /// </summary>
    public void Event_PlayerDestroy(int _camp)
    {


        if(isFinish == true)
        {
            // リザルトUIを表示
            for (int i = 0; i < playerList.Count; i++)
            {
                if (_camp == i)
                {
                    playerList[i].UI.Event_ChangeToResult(false);
                }
                else
                {
                    playerList[i].UI.Event_ChangeToResult(true);
                }
            }
        }
    }


    public List<Parameter> playerScoreList
    {
        get
        {
            List<Parameter> returnList = new List<Parameter>();
            for(int i = 0; i < playerList.Count; i++)
            {
                returnList.Add(playerList[i].player.score);
            }

            return returnList;
        }
    }

    /// <summary>
    /// いずれかのscoreが最大なら
    /// </summary>
    public bool isFinish
    {
        get
        {
            for(int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].player.score.entityIsFull)
                {
                    return true;
                }

            }

            return false;
        }
    }
}

[Serializable]
public struct Rule
{
    public int numberOfPlayers;
    public float timeLimit;
    public int maxScore;
}

[Serializable]
public class PlayerInfo
{
    public UserInfo userInfo = new UserInfo();
    public Player player = new Player();
    public Rakuin_UI UI = new Rakuin_UI();
}
