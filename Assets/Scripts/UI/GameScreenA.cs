using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenA : BasicScreen
{

    public Text m_scoreText = null;

    [Header("level_start_text")]
    [SerializeField] private Text m_startText = null;
    [SerializeField] private string m_readyStr = "R E A D Y";
    [SerializeField] private string m_goStr = " G O ";

    [Header("level_end_text")]
    [SerializeField] private Text m_levelEndText = null;
    [SerializeField] private string m_niceStr = "N O I C E !";
    [SerializeField] private string m_pityStr = " G O ";

    protected void Awake()
    {
        m_startText.CrossFadeAlpha(0.0f, 0.01f, true);
        m_levelEndText.CrossFadeAlpha(0.0f, 0.01f, true);
    }


    public void ShowLevelStartText(bool _isReady = false)
    {
        m_levelEndText.enabled = false;
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

    public void ShowLevelEndText(bool _isWin = true)
    {
        m_levelEndText.enabled = true;
        m_levelEndText.CrossFadeAlpha(1.0f, 0.02f, true);
        if (_isWin)
        {
            m_levelEndText.text = m_niceStr;
            m_levelEndText.CrossFadeAlpha(0.0f, 1.5f, true);
        }
        else
        {
            m_levelEndText.text = m_pityStr;
            m_levelEndText.CrossFadeAlpha(0.0f, 1.5f, true);
        }
    }


    public void UpdateScoreText(int _score)
    {
        m_scoreText.text = string.Format("Score: {0}", _score);
    }


    // class end
}
