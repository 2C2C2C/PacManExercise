using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HentaiTools.PoolWa;

public class GhostCracked : EffectControllerBase
{
    protected override void Awake()
    {
        base.Awake();
        OnEnd += () =>
        {
            MuyPoolManager.Instance.TakeOneBack<GhostCracked>(this);
        };
    }

    // private void Start()
    // {
    //     // bind



    // }

    // class end
}
