using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkDust : EffectControllerBase
{


    public void ChangDir(in Vector3 _forward)
    {
        if (_forward == Vector3.zero)
            return;
        transform.forward = -_forward.normalized;
    }


    // class end
}
