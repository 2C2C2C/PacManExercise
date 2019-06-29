using UnityEngine;

public class BaseGridMovement : BaseGridObject
{
    public float m_movementSpeed;

    protected IntVector2 m_inputDirection;
    protected IntVector2 m_targetGridPos;
    protected Vector3 m_position;
    protected Vector3 m_targetPosition;

    public float m_movementLerpPercentage = 1.0f;

    protected virtual void Awake()
    {
        // GetComponent<Collider>().enabled = false;
    }

    protected virtual void Start()
    {
        m_position = transform.position;
        m_targetPosition = m_position;
        m_targetGridPos = m_gridPos;
        //m_targetGridPos = m_gridPos;
        //m_position = transform.position;
        //m_targetPosition = new Vector3(m_position.x - 1.0f, m_position.y, m_position.z);
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;

        // for move
        //Debug.Log("inputDir " + m_inputDirection.x.ToString() + "," + m_inputDirection.y.ToString());
        if (transform.position == m_targetPosition)
        {
            //Debug.Log("arrive la " + gameObject.name);
            // reset, when arrive
            m_movementLerpPercentage = 0.0f;
            m_gridPos = m_targetGridPos;
            m_targetGridPos = m_gridPos + m_inputDirection;
            //Debug.Log(m_gridPos);
            if (LevelGenerator.Grids[LevelGenerator.m_levelSizeY - m_targetGridPos.y - 1, m_targetGridPos.x] == 1)
                m_targetGridPos -= m_inputDirection;
            //m_targetGridPos -= m_inputDirection;

            m_position = new Vector3(m_gridPos.x, m_gridPos.y);
            m_targetPosition = new Vector3(m_targetGridPos.x, m_targetGridPos.y);
        }
        else
        {
            m_movementLerpPercentage += Time.deltaTime * m_movementSpeed;
            transform.position = Vector3.Lerp(m_position, m_targetPosition, m_movementLerpPercentage);
        }
        //if (transform.position == m_targetPosition) return;

        // if (!GetComponent<Collider>().enabled)
        // {
        //     GetComponent<Collider>().enabled = true;
        // }
    }


    public override void InitSelfPostition()
    {
        m_position = transform.position;
        m_targetPosition = m_position;
        m_targetGridPos = m_gridPos;
    }


    // class end
}