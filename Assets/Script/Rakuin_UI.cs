using AddClass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rakuin_UI : MonoBehaviour
{
    [SerializeField] private Player parent;
    [SerializeField] private TextParameter bulletsTextParameter;

    private void Start()
    {
        bulletsTextParameter.Initialize(numberOfBullets);
    }
    private void Update()
    {
        bulletsTextParameter.Update(numberOfBullets);
    }

    public int numberOfBullets
    {
        get
        {
            return (int)parent.remainingBullets.entity;
        }
    }
}
