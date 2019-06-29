using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HentaiTools.PoolWa;

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
            // HentaiTools.PoolWa.MuyPoolManager.CallInitPool4GridObjs();

            MuyPoolManager.Instance.InitSomePool<BaseGridObject>(1);
            MuyPoolManager.Instance.InitSomePool<BaseGridMovement>(1);
            MuyPoolManager.Instance.ResizePool<Ghost>(5);
            MuyPoolManager.Instance.ResizePool<Wall>(20);
            MuyPoolManager.Instance.ResizePool<Pill>(10);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            MuyPoolManager.Instance.InitSomePool<EffectControllerBase>(5);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < 6; i++)
                MuyPoolManager.Instance.GetOne<Wall>();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            MuyPoolManager.Instance.TakeAllBack();
        }

    }

    public Transform m_Test01;

    // class end
}
