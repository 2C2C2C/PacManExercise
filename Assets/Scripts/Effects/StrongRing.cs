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
        // StopIt();
        m_effectDuration = _duration;
        var ptsMain = m_mainParticle.main;
        ptsMain.duration = _duration - m_mainParticle.main.startLifetime.Evaluate(0.0f);
        // m_mainParticle.main.duration=
    }



    // class end
}
