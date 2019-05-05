﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MacManTouchSystem : MonoBehaviour
{

    public MacMan m_macman;

    [SerializeField]
    protected float m_strongDuration = 4.0f;
    private bool m_isStrong;
    private Coroutine m_strongLast;
    private bool m_isSpeedUp;
    private Coroutine m_speedUpLast;

    private void Awake()
    {
        LevelGenerator.Instance.OnLevelGeneratingFinish += SetUp;
    }

    private void SetUp()
    {
        m_macman = FindObjectOfType<MacMan>();
    }

    private void OnEnable()
    {
        MacManTools.Evently.Instance.Subscribe<MacManTouchEvent>(OnMacManTouch);
    }

    private void OnDisable()
    {
        MacManTools.Evently.Instance.UnSubscribe<MacManTouchEvent>(OnMacManTouch);
    }

    private void OnMacManTouch(MacManTouchEvent evt)
    {
        if (m_macman == null)
            return;
        //Debug.Log("get la");
        switch (evt.m_touchType)
        {
            case TouchType.macNghost:
                if (m_isStrong)
                {
                    Debug.Log("mac man crush a ghost");
                    GameManager.Instance.AddScore(20);
                    Destroy(evt.m_otherGo);
                }
                else
                {
                    Debug.Log("mac man be caught by a ghost");
                    Destroy(evt.m_otherGo);
                    GameManager.Instance.ResetStatus();
                    LevelGenerator.ReStartTest();
                }
                break;
            case TouchType.macNpill:
                Pill pill = evt.m_otherGo.GetComponent<Pill>();
                switch (pill.m_pillType)
                {
                    case PillType.Normal:
                        //Debug.Log("mac man eat pill la");
                        GameManager.Instance.AddScore(5);
                        //LevelGenerator
                        break;
                    case PillType.Fast:
                        GameManager.Instance.AddScore(10);
                        //Debug.Log("mac man eat wind pill la");
                        m_macman.StartSpeedUp(4.0f);
                        break;
                    case PillType.Strong:
                        if (m_strongLast != null)
                            StopCoroutine(m_strongLast);
                        GameManager.Instance.AddScore(10);
                        m_strongLast = StartCoroutine(MacManStrongLast());
                        m_macman.StartBlinking(m_strongDuration);
                        //Debug.Log("mac man eat power pill la");
                        break;
                    default:
                        break;
                }
                Destroy(evt.m_otherGo);
                LevelGenerator.Instance.m_pillCounts--;
                if (LevelGenerator.Instance.m_pillCounts <= 0)
                {
                    LevelGenerator.ReStartTest();
                    return;
                }
                break;
            default:
                break;
        }
    }

    protected IEnumerator MacManStrongLast()
    {
        m_isStrong = true;
        yield return new WaitForSeconds(m_strongDuration);
        m_isStrong = false;
        Debug.Log($"macman strong end");
    }



    // class end
}
