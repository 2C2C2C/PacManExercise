using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ghost : BaseGridMovement
{
    [SerializeField] private Animator m_bodyAC = null;
    [SerializeField] private Animator m_faceAC = null;



    [SerializeField]
    private IntVector2[] dirCanGo = new IntVector2[4]
    {
        new IntVector2(IntVector2.RightVector2Int),
        new IntVector2(IntVector2.LeftVector2Int),
        new IntVector2(IntVector2.UpVector2Int),
        new IntVector2(IntVector2.DownVector2Int)
    };

    private readonly IntVector2[] movementDir = new IntVector2[]
    {
        new IntVector2(IntVector2.RightVector2Int),
        new IntVector2(IntVector2.LeftVector2Int),
        new IntVector2(IntVector2.UpVector2Int),
        new IntVector2(IntVector2.DownVector2Int)
    };

    int j = 0;
    protected override void Update()
    {

        if (transform.position == m_targetPosition)
        {
            //int j = 0;
            j = 0;
            for (int i = 0; i < movementDir.Length; i++)
            {

                if (movementDir[i] != -m_inputDirection) // will not move back
                    if (LevelGenerator.Grids[LevelGenerator.m_levelSizeY - (m_targetGridPos.y + movementDir[i].y) - 1, m_targetGridPos.x + movementDir[i].x] != 1)
                    {
                        //m_gridPos
                        dirCanGo[j] = movementDir[i];
                        j++;
                    }
            }
            //Debug.Log(j);
            //Debug.Log("last dir" + m_inputDirection);
            //Debug.Log("last dirReverse" + -m_inputDirection);
            if (j > 1)
                m_inputDirection = dirCanGo[Random.Range(0, j)];
            else if (j == 1)
                m_inputDirection = dirCanGo[0];
            else
            {
                m_inputDirection *= -1;
                MacManTools.Evently.Instance.Publish(new TuringAroundEvent(gameObject));
            }
        }

        //update anim
        if (m_acturallyMovingDir.x != 0)
        {
            // look hori
            if (m_acturallyMovingDir.x < 0)
                m_faceAC.SetInteger("LookDir", 2);
            else
                m_faceAC.SetInteger("LookDir", 3);
        }
        else
        {
            if (m_acturallyMovingDir.y < 0)
                m_faceAC.SetInteger("LookDir", 1);
            else
                m_faceAC.SetInteger("LookDir", 0);
        }

        // move
        base.Update();

        //if (transform.position == m_targetPosition) return;

        //m_movementLerpPercentage += Time.deltaTime * movementSpeed;
        //transform.position = Vector3.Lerp(transform.position, m_targetPosition, m_movementLerpPercentage);
    }


    // class end
}
