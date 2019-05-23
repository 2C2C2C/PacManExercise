using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// camera follow pacman on XOY plane
/// </summary>
public class CameraFollow : MonoBehaviour
{

    public Transform m_targetToFollow;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


    Vector3 m_newPos;
    private void LateUpdate()
    {
        if (m_targetToFollow != null)
        {
            m_newPos = m_targetToFollow.position;
            m_newPos.z = transform.position.z;
            transform.position = m_newPos;
        }
    }

    // class end
}
