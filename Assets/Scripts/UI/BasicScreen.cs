using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicScreen : MonoBehaviour
{

    protected bool m_screenActived = false;

    #region actions

    public System.Action OnScreenOpen;
    public System.Action OnScreenClosed;

    #endregion


    private void Awake()
    {
        m_screenActived = gameObject.activeSelf;
    }

    public void OpenScreen()
    {
        m_screenActived = true;
        gameObject.SetActive(true);
        OnScreenOpen?.Invoke();
    }

    public void CloseScreen()
    {
        m_screenActived = false;
        gameObject.SetActive(false);
        OnScreenClosed?.Invoke();
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

// class end
}
