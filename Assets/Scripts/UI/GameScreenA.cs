using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenA : BasicScreen
{

    public Text m_scoreText = null;

    [SerializeField] private Text m_startText = null;
    [SerializeField] private string m_readyStr = "R E A D Y";
    [SerializeField] private string m_goStr = " G O ";

    protected void Awake()
    {
        m_startText.CrossFadeAlpha(0.0f, 0.01f, true);
    }


    public void SetReadyText(bool _isReady = false)
    {
        if (!_isReady)
        {
            m_startText.text = m_goStr;
            m_startText.CrossFadeAlpha(0.0f, 1.0f, true);
        }
        else
        {
            m_startText.text = m_readyStr;
            m_startText.CrossFadeAlpha(1.0f, 0.2f, true);
        }
    }


    public void UpdateScoreText(int _score)
    {
        m_scoreText.text = string.Format("Score: {0}", _score);
    }


    // class end
}
