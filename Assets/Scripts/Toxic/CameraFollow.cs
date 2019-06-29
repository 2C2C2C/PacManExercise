using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/// <summary>
/// camera follow pacman on XOY plane,for main camera?
/// </summary>
public class CameraFollow : MonoBehaviour
{
    private static CameraFollow instance = null;
    public static CameraFollow Instance => instance;

    public Transform m_targetToFollow = null;
    public Transform m_cameraTransform = null;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void Awake()
    {
        instance = this;
        if (null == m_cameraTransform)
            m_cameraTransform = GetComponent<Camera>().transform;
    }


    private Vector3 m_newPos;
    private void LateUpdate()
    {
        if (m_targetToFollow != null)
        {
            m_newPos = m_targetToFollow.position;
            m_newPos.z = transform.position.z;
            transform.position = m_newPos;
        }
    }

    public void CameraShake4BreakGhost(float _duration = 0.2f)
    {
        Vector3 ogLocalPos = m_cameraTransform.localPosition;
        // Debug.Log("break ghost shake");
        m_cameraTransform.DOShakePosition(_duration, 1.0f, 8, 60, false, true).OnComplete(() =>
       {
           m_cameraTransform.localPosition = ogLocalPos;
       });
    }

    // class end
}
