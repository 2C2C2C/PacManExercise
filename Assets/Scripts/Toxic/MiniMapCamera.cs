using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{

    public Camera m_camera;

    private void Awake()
    {
        m_camera = GetComponent<Camera>();
    }

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


    public void SetCameraRatio(float _w, float _h)
    {
        if (!m_camera)
        {
            Debug.LogError("there is no camera");
            return;
        }
        // set camera viewport width and height, than shoud set the minimap image
        float ratio = _w / _h;
        Rect newRt = m_camera.rect;
        newRt.width = ratio;
        newRt.height = 1.0f;
        m_camera.rect = newRt;
        //m_camera.rect.height
        //m_camera.rect.width
        //m_camera.rect.height
    }
    public void SetCameraRatio(float _ratio)
    {
        if (!m_camera)
        {
            Debug.LogError("there is no camera");
            return;
        }
        Rect newRt = m_camera.rect;
        newRt.width = _ratio;
        newRt.height = 1.0f;
        m_camera.rect = newRt;
    }


    // class end
}
