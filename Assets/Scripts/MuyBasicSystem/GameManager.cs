using System.Collections;
using System.Collections.Generic;
using System;


public class GameManager
{
    private static GameManager instance;
    public static GameManager Instance => instance ?? (instance = new GameManager());

    private int m_scores = 0;
    private int m_levelFinished = 0;

    #region actions

    public Action OnScoreChanged;

    #endregion

    public GameManager()
    {
        m_scores = 0;
    }

    public void ResetStatus()
    {
        m_levelFinished = 0;
        m_scores = 0;
    }

    public void AddScore(int _add)
    {
        m_scores += _add;

        // do not do dis
        if (UIManager.Instance)
        {
            GameScreenA gsa;
            gsa = (UIManager.Instance.GetScreen<GameScreenA>() as GameScreenA);
            if (gsa != null)
                gsa.UpdateScoreText(m_scores);
        }
        OnScoreChanged?.Invoke();
    }

    public int GetNowScore()
    {
        return m_scores;
    }

    // class end
}
