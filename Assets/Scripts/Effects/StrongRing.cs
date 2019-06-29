using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HentaiTools.PoolWa;

public class StrongRing : EffectControllerBase
{

    protected override void Awake()
    {
        base.Awake();
        // no need to take dis back at dis time
        // OnEnd += () =>
        // {
        //     MuyPoolManager.Instance.TakeOneBack<GhostCracked>(this);
        // };
    }


    public void SetUp(float _duration)
    {
        m_effectDuration = _duration;
    }



    // class end
}
