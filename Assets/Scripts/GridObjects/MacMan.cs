using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacMan : BaseGridMovement
{
    private Material m_mat = null;
    private Color m_ogColor = Color.green;
    [SerializeField] private float m_originalSpeed = 3.0f;
    [SerializeField] private Transform m_meshRoot = null;

    #region effects
    [SerializeField] private WalkDust m_walkDust = null;
    [SerializeField] private StrongRing m_stringRing = null;
    #endregion
    protected override void Awake()
    {
        // get other component ref
        if (m_walkDust)
        {
            m_walkDust = GetComponentInChildren<WalkDust>();
            if (m_walkDust)
                m_walkDust.PlayIt();
        }
        if (m_stringRing)
        {
            m_stringRing = GetComponentInChildren<StrongRing>();
        }

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

        // Debug.Log(m_inputDirection);
        // change 
        if (null != m_walkDust)
            m_walkDust.ChangDir(new Vector3(m_inputDirection.x, m_inputDirection.y, 0.0f));
        if (null != m_meshRoot)
        {
            m_meshRoot.right = m_acturallyMovingDir.ToVector3();
        }
        // m_walkDust.ChangDir(new Vector3(m_inputDirection.x, m_inputDirection.y, 0.0f));

        // move
        base.Update();
    }


    private void OnTriggerEnter(Collider _other)
    {
        if (GameManager.Instance.IsPaused)
            return;

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
        m_mat.SetColor("_Color", Color.green);
    }

    protected Coroutine m_blinkingCoroutine;
    public void PlayStrongEffects(float _duration)
    {
        if (m_blinkingCoroutine != null)
            StopCoroutine(m_blinkingCoroutine);
        StartCoroutine(BlinkingLast(_duration));
    }
    protected IEnumerator BlinkingLast(float _duration)
    {
        if (m_stringRing)
        {
            m_stringRing.StopIt();
            m_stringRing.gameObject.SetActive(false);
            // yield return new WaitForSeconds(0.1f);
            m_stringRing.SetUp(_duration);
            m_stringRing.gameObject.SetActive(true);
            m_stringRing.PlayIt();
        }
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
        // if (m_stringRing)
        //     m_stringRing.StopIt();
    }

    protected Coroutine m_speedUpCoroutine;
    public void StartSpeedUp(float _duration = 4.0f)
    {
        // m_mat.SetColor("_Color", Color.blue);
        // m_movementSpeed += 0.4f * m_originalSpeed;
        float tmp = (m_movementSpeed - m_originalSpeed) / m_originalSpeed;
        if (tmp > 1.0)
            tmp = 1.0f;
        m_ogColor = Color.blue;
        m_mat.SetColor("_Color", Color.blue);

        if (m_speedUpCoroutine != null)
            StopCoroutine(m_speedUpCoroutine);
        StartCoroutine(SpeedUpLast(_duration));
    }
    protected IEnumerator SpeedUpLast(float _duration)
    {
        // todo : remove dis
        SoundManager.Instance.PitchUpSFX(0.2f);
        m_mat.SetColor("_Color", Color.blue);
        float oldSpeed = m_movementSpeed;
        m_movementSpeed *= 1.3f;
        yield return new WaitForSeconds(_duration);
        m_mat.SetColor("_Color", Color.green);
        m_movementSpeed = oldSpeed * (1.0f + 0.1f);
        m_ogColor = Color.green;
        // todo : remove dis
        SoundManager.Instance.ResetSFCPitch();
    }



    // class end
}
