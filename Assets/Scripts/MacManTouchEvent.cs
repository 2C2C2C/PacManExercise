//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public enum TouchType
{
    macNghost, macNpill
}

public class MacManTouchEvent
{
    public TouchType m_touchType;
    public GameObject m_otherGo;
    public MacManTouchEvent(TouchType _touchType, GameObject _otherGo)
    {
        m_otherGo = _otherGo;
        m_touchType = _touchType;
    }

    // class end
}
