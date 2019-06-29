using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MacManTools;
using HentaiTools.PoolWa;



public class MacManTouchSystem : MonoBehaviour
{

    public MacMan m_macman = null;

    [SerializeField]
    protected float m_strongDuration = 5.0f;
    private bool m_isStrong = false;
    private Coroutine m_strongLast = null;
    private bool m_isSpeedUp = false;
    private Coroutine m_speedUpLast = null;

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
                    // Debug.Log("mac man crush a ghost");
                    if (MuyPoolManager.Instance)
                    {
                        CameraFollow.Instance.CameraShake4BreakGhost(0.4f);
                        EffectControllerBase fx = MuyPoolManager.Instance.GetOne<GhostCracked>(evt.m_otherGo.transform.position) as EffectControllerBase;
                        Debug.Log($"effect pos {evt.m_otherGo.transform.position}");
                        fx.PlayIt();
                        MuyPoolManager.Instance.TakeOneBack<Ghost>(evt.m_otherGo.GetComponent<Ghost>());
                    }
                    // Destroy(evt.m_otherGo);
                    GameManager.Instance.AddScore(20);
                    SoundManager.Instance.PlaySfxTemp("eat_ghost_01");
                }
                else
                {
                    // Debug.Log("mac man be caught by a ghost");
                    SoundManager.Instance.PlaySfxTemp("death_01");
                    m_macman.ResetMacMan();
                    if (HentaiTools.PoolWa.MuyPoolManager.Instance)
                        HentaiTools.PoolWa.MuyPoolManager.Instance.TakeOneBack<MacMan>(m_macman);
                    // Destroy(evt.m_otherGo);
                    LevelGenerator.Instance.FinishLevel(false);
                    // LevelGenerator.ReStartTest();
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
                        m_macman.PlayStrongEffects(m_strongDuration);

                        //Debug.Log("mac man eat power pill la");
                        break;
                    default:
                        break;
                }
                SoundManager.Instance.PlaySfxTemp("eat_pill_01");
                if (HentaiTools.PoolWa.MuyPoolManager.Instance)
                    HentaiTools.PoolWa.MuyPoolManager.Instance.TakeOneBack<Pill>(pill);
                // Destroy(pill.gameObject);
                LevelGenerator.Instance.m_pillCounts--;
                if (LevelGenerator.Instance.m_pillCounts <= 0)
                {
                    // LevelGenerator.ReStartTest();
                    LevelGenerator.Instance.FinishLevel();
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
        // Debug.Log($"macman strong end");
    }



    // class end
}
