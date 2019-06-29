using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PillType
{
    Normal, Fast, Strong
}

public class Pill : BaseGridObject
{
    public static Color[] PillColors = { Color.green, Color.blue, Color.red };
    public Transform m_meshRoot = null;
    public PillType m_pillType = PillType.Normal;
    public Material m_mat = null;

    private void Awake()
    {
        if (GetComponentInChildren<MeshRenderer>())
            m_mat = GetComponentInChildren<MeshRenderer>().material;
        InitPill(m_pillType);
    }

    [ContextMenu("init pill")]
    public void InitPill(PillType _pillType = PillType.Normal)
    {
        m_pillType = _pillType;
        if (m_mat)
            m_mat.SetColor("_Color", Pill.PillColors[(int)_pillType]);
        if (m_pillType != PillType.Normal)
        {
            // special pill are bigger
            m_meshRoot.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    // class end
}
