using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// do not use it in game
/// </summary>
public class NonUseTestWa : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            HentaiTools.PoolWa.MuyPoolManager.Instance.EmptyFunc();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HentaiTools.PoolWa.MuyPoolManager.CallInitPool4GridObjs();
            HentaiTools.PoolWa.MuyPoolManager.Instance.InitSomePool<BaseGridMovement>();
        }
    }

    // class end
}
