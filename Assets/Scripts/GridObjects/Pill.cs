using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum PillType
{
    Normal, Fast, Strong
}

public class Pill : BaseGridObject
{
    public static Color[] PillColors = { Color.green, Color.blue, Color.red };
    [SerializeField] private Transform m_meshRoot = null;
    [SerializeField] private MeshRenderer m_mesh = null;
    private Material m_mat = null;
    public PillType m_pillType = PillType.Normal;

    private static Vector3 normalPillMeshSize = new Vector3(0.3f, 0.3f, 0.3f);
    private Tween m_punchScaleTween = null;

    private void Awake()
    {
        if (null == m_mesh)
        {
            m_mesh = GetComponentInChildren<MeshRenderer>();
        }
        m_mat = GetComponentInChildren<MeshRenderer>().material;
        InitPill(m_pillType);
    }

    [ContextMenu("init pill")]
    public void InitPill(PillType _pillType = PillType.Normal)
    {
        m_pillType = _pillType;
        if (m_mat)
            m_mat.SetColor("_Color", Pill.PillColors[(int)_pillType]);
        if (null != m_punchScaleTween)
            m_punchScaleTween.Kill();
        if (m_pillType != PillType.Normal)
        {
            // special pill are bigger
            m_meshRoot.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            // m_meshRoot.localScale = Pill.normalPillMeshSize;
            // m_punchScaleTween = m_meshRoot.DOPunchScale(new Vector3(0.6f, 0.6f, 0.6f), 2.0f, 5, 1.0f).SetLoops(-1);
            m_punchScaleTween = m_meshRoot.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            m_meshRoot.localScale = Pill.normalPillMeshSize;
        }

        //
        if (null != m_pillBlinkAnim)
            StopCoroutine(m_pillBlinkAnim);
        switch (m_pillType)
        {
            case PillType.Strong:
                StrongPillBlinking();
                break;
            default:
                break;
        }
    }

    private Coroutine m_pillBlinkAnim = null;
    public void StrongPillBlinking()
    {
        m_pillBlinkAnim = StartCoroutine(StrongPillBlinkingLast());
    }
    private IEnumerator StrongPillBlinkingLast()
    {
        yield return null;
        float tempTime = 0.0f;
        float blinkDuration = 1.5f;
        if (m_mat != null)
        {
            do
            {
                while (tempTime < blinkDuration)
                {
                    yield return null;
                    tempTime += Time.deltaTime;
                    m_mat.SetColor("_Color", Color.Lerp(Color.white, Color.red, tempTime / blinkDuration));
                }
                tempTime = 0;
                while (tempTime < blinkDuration)
                {
                    yield return null;
                    tempTime += Time.deltaTime;
                    m_mat.SetColor("_Color", Color.Lerp(Color.white, Color.red, tempTime / blinkDuration));
                }
            } while (true);
        }
    }

    // class end
}
