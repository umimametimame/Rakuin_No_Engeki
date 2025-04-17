using AddUnityClass;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rakuin_UI : MonoBehaviour
{
    private Player parentPlayer;
    private PlayerInfo parentPlayerInfo;
    private PlayerInfo otherPlayerInfo;

    [SerializeField] private TextParameter bulletsTextParameter = new TextParameter();
    [SerializeField] private TextParameter myScoreTextParameter = new TextParameter();
    [SerializeField] private TextParameter otherScoreTextParameter = new TextParameter();

    [SerializeField] private TextMeshProUGUI myUserName;
    [SerializeField] private TextMeshProUGUI otherUserName;
    [field: SerializeField] public List<GameObject> destroyByResult { get; private set; }
    [field: SerializeField] public Instancer winnerUI { get; private set; } = new Instancer();
    [field: SerializeField] public Instancer loserUI { get; private set; } = new Instancer();
    private void Start()
    {
        parentPlayer = transform.root.GetComponentInChildren<Player>();
        parentPlayerInfo = gameInfo.playerList[ID];
        otherPlayerInfo = gameInfo.GetOtherPlayer(ID);
        
        bulletsTextParameter.Initialize(parentPlayerInfo.player.remainingBullets);
        myScoreTextParameter.Initialize(parentPlayerInfo.player.score);
        otherScoreTextParameter.Initialize(otherPlayerInfo.player.score);

        myUserName.SetText(parentPlayerInfo.userInfo.userName);
        otherUserName.SetText(otherPlayerInfo.userInfo.userName);

    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        bulletsTextParameter.Update((int)parentPlayerInfo.player.remainingBullets.entity);
        myScoreTextParameter.Update((int)parentPlayerInfo.player.score.entity);
        otherScoreTextParameter.Update((int)otherPlayerInfo.player.score.entity);
    }

    public void Event_ChangeToResult(bool _isWinner)
    {
        if(_isWinner == true)
        {
            winnerUI.Instance(gameObject);
        }
        else
        {
            loserUI.Instance(gameObject);
        }

        for(int i = 0; i < destroyByResult.Count; i++)
        {
            destroyByResult[i].SetActive(false);
        }
    }

    private GameSceneOperator gameInfo
    {
        get
        {
            return GameSceneOperator.instance;
        }
    }

    private int ID
    { 
        get
        {
            return parentPlayer.instanceID;
        }
    }
}