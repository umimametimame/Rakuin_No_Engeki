using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddClass;

public class MomentaryBarAndDelayedBar : MonoBehaviour
{
    [field: SerializeField, NonEditable] public BarObject momentaryBar { get; private set; } = new BarObject();
    [field: SerializeField, NonEditable] public BarObject delayedBar { get; private set; } = new BarObject();
    private List<BarObject> bars = new List<BarObject>();

    [field: SerializeField] public Vector3 rotateDirection { get; set; }
    [field: SerializeField, NonEditable] public Vector3 norDirection { get; private set; }
    [field: SerializeField] public float speed { get; set; }
    [SerializeField, NonEditable] private float different;

    private void Start()
    {
        bars = new List<BarObject> { momentaryBar, delayedBar };
        for(int i = 0; i < bars.Count; ++i)
        {
            bars[i].root = transform.GetChild(i).transform;
            bars[i].bar = bars[i].root.GetChild(0).transform;
            bars[i].apex = bars[i].bar.GetChild(0).transform;
        }
    }

    private void Update()
    {
        norDirection = rotateDirection.normalized;



        Vector3 directionToApex = momentaryBar.apex.position - delayedBar.root.position;
        Quaternion quaternion = Quaternion.LookRotation(directionToApex);
        delayedBar.root.rotation = Quaternion.Slerp(delayedBar.root.rotation, quaternion, speed);
    }

    /// <summary>
    /// ˆø”Vector2‚©‚çŠp“x‚ğZo‚·‚é
    /// </summary>
    /// <param name="vec2"></param>
    public void RotateByVec2(Vector2 vec2)
    {
        float direction = AddFunction.Vec2ToAngle(vec2);
        momentaryBar.root.eulerAngles = norDirection * direction;
    }

    /// <summary>
    /// ˆø”Vector3(EulerŠp)‚©‚çŠp“x‚ğ•ÏX‚·‚é
    /// </summary>
    /// <param name="_euler"></param>
    public void RotateByEuler(Vector3 _euler)
    {
        momentaryBar.root.eulerAngles = _euler;
    }
}

[Serializable] public class BarObject
{
    [field: SerializeField] public Transform root { get; set; }
    [field: SerializeField] public Transform bar { get; set; }
    [field: SerializeField] public Transform apex { get; set; }
}