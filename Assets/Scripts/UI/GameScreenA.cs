using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreenA : BasicScreen
{

    public Text m_scoreText;

    protected void Awake()
    {

    }

    public void UpdateScoreText(int _score)
    {
        m_scoreText.text = string.Format("Score: {0}", _score);
    }


    // class end
}
