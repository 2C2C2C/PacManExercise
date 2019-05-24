using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacMan : BaseGridMovement
{
    private Material m_mat;
    private Color m_ogColor;
    [SerializeField] private float m_originalSpeed = 3.0f;

    protected override void Awake()
    {
        base.Awake();
        m_originalSpeed = m_movementSpeed;
        if (GetComponentInChildren<MeshRenderer>())
        {
            m_mat = GetComponentInChildren<MeshRenderer>().material;
            if (m_mat)
                m_ogColor = m_mat.color;
        }
    }

    protected override void Update()
    {
        //base.Update();
        if (Input.GetKeyDown(KeyCode.DownArrow))
            m_inputDirection = IntVector2.DownVector2Int;
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            m_inputDirection = IntVector2.UpVector2Int;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            m_inputDirection = IntVector2.LeftVector2Int;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            m_inputDirection = IntVector2.RightVector2Int;

        base.Update();
    }


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Ghost"))
        {
            //Debug.Log("col ghost");
            MacManTools.Evently.Instance.Publish<MacManTouchEvent>(new MacManTouchEvent(TouchType.macNghost, _other.gameObject));
            return;
        }
        if (_other.CompareTag("Pill"))
        {
            //Debug.Log("col pill");
            MacManTools.Evently.Instance.Publish<MacManTouchEvent>(new MacManTouchEvent(TouchType.macNpill, _other.gameObject));
            return;
        }

    }

    public override void InitSelfPostition()
    {
        m_position = transform.position;
        m_targetPosition = m_position;
        m_targetGridPos = m_gridPos;
        m_inputDirection = IntVector2.IntVectorZero;
    }

    public void ResetMacMan()
    {
        m_movementSpeed = m_originalSpeed;
        m_mat.SetColor("_Color", Color.white);
    }

    protected Coroutine m_blinkingCoroutine;
    public void StartBlinking(float _duration)
    {
        if (m_blinkingCoroutine != null)
            StopCoroutine(m_blinkingCoroutine);
        StartCoroutine(BlinkingLast(_duration));
    }
    protected IEnumerator BlinkingLast(float _duration)
    {
        float temp = 0.0f;
        //Color tmpColor = m_mat.color;
        while (temp < _duration)
        {
            yield return new WaitForSeconds(0.2f);
            temp += 0.2f;
            m_mat.SetColor("_Color", Color.red);
            yield return new WaitForSeconds(0.2f);
            m_mat.SetColor("_Color", m_ogColor);
            temp += 0.2f;
        }
        m_mat.SetColor("_Color", m_ogColor);
    }

    protected Coroutine m_speedUpCoroutine;
    public void StartSpeedUp(float _duration = 4.0f)
    {

        // m_mat.SetColor("_Color", Color.blue);
        m_movementSpeed += 0.2f * m_originalSpeed;
        float tmp = (m_movementSpeed - m_originalSpeed) / m_originalSpeed;
        tmp %= 1.0f;
        m_ogColor = Color.Lerp(Color.white, Color.blue, tmp);
        m_mat.SetColor("_Color", m_ogColor);

        //if (m_speedUpCoroutine != null)
        //    StopCoroutine(m_speedUpCoroutine);
        //StartCoroutine(SpeedUpLast(_duration));
    }
    protected IEnumerator SpeedUpLast(float _duration)
    {
        m_mat.SetColor("_Color", Color.blue);
        m_movementSpeed *= 1.3f;
        yield return new WaitForSeconds(_duration);
        m_mat.SetColor("_Color", Color.white);
        m_movementSpeed = m_originalSpeed;
    }

    // class end
}
