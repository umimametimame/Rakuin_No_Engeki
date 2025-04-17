using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericChara.Chara
{
    public Player parent;
    [field: SerializeField] public int camp { get; set; }
    protected override void Start()
    {
        base.Start();
        camp = parent.instanceID;
    }
    protected override void Update()
    {
        base.Update();
    }
    private void EnterPlayer(Player _player)
    {
        // ヒットしたPlayerクラスが親でなければ
        if(_player.instanceID != camp)
        {
            if(_player.invinsible == false)
            {

                _player.Damage(parent);
            }
            else if(_player.invinsible == true)
            {
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Player>(out Player _player) == true)
        {
            EnterPlayer(_player);
        }
    }

}
