using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuScreen : BasicScreen
{
    [SerializeField]
    protected string m_gameSceneName = null;
    public Button m_btnStart = null;
    public Button m_btnQuit = null;

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

        OnScreenOpen += () =>
          {
              SetInitState();
              UIManager.Instance.ues.SetSelectedGameObject(m_btnStart.gameObject);
          };

    }


    public void StartGame()
    {
        m_btnQuit.interactable = false;
        m_btnStart.interactable = false;
        StartCoroutine(StartGameLast());
    }
    protected IEnumerator StartGameLast()
    {
        m_btnStart.transform.DOPunchScale(new Vector3(1.4f, 1.4f, 1.4f), 1.0f, 3);
        yield return new WaitForSeconds(2.0f);
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

    private void SetInitState()
    {
        m_btnStart.interactable = true;
        m_btnQuit.interactable = true;
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
