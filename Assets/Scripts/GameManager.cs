using System.Collections;
using System.Collections.Generic;
using System;
using HentaiTools.PoolWa;


public class GameManager
{
    private static readonly System.Object m_lock = new Object();
    private static GameManager instance;
    // public static GameManager Instance => instance ?? (instance = new GameManager());
    public static GameManager Instance
    {
        get
        {
            lock (m_lock)
            {
                if (GameManager.instance == null)
                {
                    GameManager.instance = new GameManager();
                }
                return GameManager.instance;
            }
        }
    }

    private int m_scores = 0;
    private int m_levelFinished = 0;

    private readonly int m_ogLevelSizeWidth = 16;
    private readonly int m_ogLlevelSizeHeight = 8;
    private int m_levelSizeWidth = 12;
    private int m_levelSizeHeight = 12;

    /// <summary>
    /// todo : do not do this way,
    /// read it from other file may be better
    /// </summary>
    private int m_tempCount = 2;

    #region actions

    public Action OnScoreChanged;

    #endregion

    public GameManager()
    {
        m_scores = 0;
        m_levelFinished = 0;
    }

    public void ResetStatus()
    {
        m_levelFinished = 0;
        m_scores = 0;
        m_levelSizeHeight = m_ogLlevelSizeHeight;
        m_levelSizeWidth = m_ogLevelSizeWidth;
        OnScoreChanged?.Invoke();
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

    public void AddLevelCount(int _add = 1)
    {
        m_levelFinished = m_levelFinished + _add;
        if (_add != 0 && m_levelFinished % m_tempCount == 0)
        {
            // gain difficulty
            m_levelSizeHeight += 4;
            m_levelSizeWidth += 4;
        }
        LevelGenerator.ExtendGrids(m_levelSizeHeight, m_levelSizeWidth);
    }

    // class end
}
