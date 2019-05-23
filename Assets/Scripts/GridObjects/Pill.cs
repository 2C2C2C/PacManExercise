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

    public PillType m_pillType = PillType.Normal;
    public Material m_mat;

    private void Awake()
    {
        if (GetComponentInChildren<MeshRenderer>())
            m_mat = GetComponentInChildren<MeshRenderer>().material;
        InitPill(m_pillType);
    }

    public void InitPill(PillType _pillType = PillType.Normal)
    {
        m_pillType = _pillType;
        if (m_mat)
            m_mat.SetColor("_Color", Pill.PillColors[(int)_pillType]);
    }

    // class end
}
