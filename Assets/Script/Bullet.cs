using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GenericChara.Chara
{
    public Player parent;
    [field: SerializeField] public Camp camp { get; set; }
    protected override void Start()
    {
        base.Start();
        camp = parent.camp;
    }
    protected override void Update()
    {
        base.Update();
        //Debug.Log(parent.transform.eulerAngles);
    }
    private void EnterPlayer(Player _player)
    {
        // �q�b�g����Player�N���X���e�łȂ����
        if(_player.camp != camp)
        {
            if(_player.invinsible == false)
            {

                _player.Damage(parent);
                Debug.Log("�q�b�g!");
            }
            else if(_player.invinsible == true)
            {
                Debug.Log("���!");
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
