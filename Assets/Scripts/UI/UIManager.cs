﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;
    public UnityEngine.EventSystems.EventSystem ues = null;
    private Dictionary<System.Type, BasicScreen> m_uiScreens;

    private void Awake()
    {
        if (UIManager.Instance == null)
            UIManager.Instance = this;
        else
            Destroy(gameObject);


        m_uiScreens = new Dictionary<System.Type, BasicScreen>();
        BasicScreen[] screens = GetComponentsInChildren<BasicScreen>(true);

        for (int i = 0; i < screens.Length; i++)
        {
            if (m_uiScreens.ContainsKey(screens[i].GetType()))
            {
                Destroy(m_uiScreens[screens[i].GetType()].gameObject);
                m_uiScreens[screens[i].GetType()] = screens[i];
            }
            else
            {
                m_uiScreens[screens[i].GetType()] = screens[i];
                //m_uiScreens(screens[i].GetType()],screens[i]);
            }
        }


    }



    public void ShowScreen<T>()
    {
        if (!m_uiScreens.ContainsKey(typeof(T)))
            Debug.LogError("this screen does not exist");
        else
            m_uiScreens[typeof(T)].OpenScreen();
    }

    public void CloseScreen<T>()
    {
        if (!m_uiScreens.ContainsKey(typeof(T)))
            Debug.LogError("this screen does not exist");
        else
            m_uiScreens[typeof(T)].CloseScreen();
    }

    public BasicScreen GetScreen<T>()
    {
        if (!m_uiScreens.ContainsKey(typeof(T)))
        {
            Debug.LogError("this screen does not exist");
            return null;
        }
        else
        {
            return m_uiScreens[typeof(T)];
        }
    }


    // class end
}
