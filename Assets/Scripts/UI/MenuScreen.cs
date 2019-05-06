using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : BasicScreen
{
    [SerializeField]
    protected string m_gameSceneName;
    public Button m_btnStart;
    public Button m_btnQuit;

    private void Awake()
    {
        GameObject tempGo;

        // bind btn

        if (m_btnStart)
            m_btnStart.onClick.AddListener(this.StartGame);
        else
        {
            tempGo = GameObject.Find("BtnStart");
            if (!tempGo)
                Debug.LogError("btn start can not find");
            else
            {
                m_btnStart = tempGo.GetComponent<Button>();
                if (!m_btnStart)
                    Debug.LogError("btn start's component not find");
                else
                    m_btnStart.onClick.AddListener(this.StartGame);
            }
        }

        if (m_btnQuit)
            m_btnQuit.onClick.AddListener(this.QuitGame);
        else
        {
            tempGo = GameObject.Find("BtnStart");
            if (!tempGo)
                Debug.LogError("btn start can not find");
            else
            {
                m_btnQuit = tempGo.GetComponent<Button>();
                if (!m_btnQuit)
                    Debug.LogError("btn start's component not find");
                else
                    m_btnQuit.onClick.AddListener(this.QuitGame);
            }
        }

    }


    public void StartGame()
    {
        StartCoroutine(StartGameLast());

    }
    protected IEnumerator StartGameLast()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(m_gameSceneName);
        ao.allowSceneActivation = true;
        ao.completed += (a) =>
        {
            UIManager.Instance.CloseScreen<MenuScreen>();
            Debug.Log("game scene loaded");
        };
        yield return new WaitForSeconds(0.1f);
        //while (ao.progress < 0.9)
        //{

        //    Debug.Log($"load progress {ao.progress}");
        //    yield return new WaitForSeconds(0.1f);
        //}
        //ao.progress
    }



    public void QuitGame()
    {
#if UNITY_EDITOR
        //Debug.Log("Unity Editor");
#endif

#if UNITY_STANDALONE
        Application.Quit();
#endif

    }

    // class end
}
