using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    private static CameraManager _Instance = null;

    public static CameraManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new CameraManager();
            }
            return _Instance;
        }
    }
    private Camera m_UICamera=null;
    private float lerpTime = 0;


    /// <summary>
    /// centerPoint指相机缩放的中心点，scale指相机最终拉伸幅度0-1,1为正常大小，rate指每次递归累加的插值时间。
    /// </summary>
    /// <param name="scale"></param>
    /// <param name="rate"></param>
    public void DrawUICameraLens(Vector3 centerPoint,float scale,float rate)
    {
        lerpTime = 0;
        if (m_UICamera == null)
        {
            m_UICamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        }
        float size = m_UICamera.orthographicSize;
        IEnumeratorManager.Instance.StartCoroutine(DrawUICameraLens_IEnumerator(centerPoint,scale,rate, size));

    }

    private IEnumerator DrawUICameraLens_IEnumerator(Vector3 centerPoint, float scale,float rate,float startSize)
    {
        yield return new WaitForSeconds(0.01f);
        m_UICamera.orthographicSize = Mathf.Lerp(startSize, scale, lerpTime);
        m_UICamera.transform.localPosition = Vector3.Lerp(m_UICamera.transform.localPosition, centerPoint, lerpTime);
        lerpTime += rate;

        if (lerpTime >= 1)
        {
            yield return null;
        }
        else
        {
           IEnumeratorManager.Instance.StartCoroutine(DrawUICameraLens_IEnumerator(centerPoint, scale, rate,startSize));
        }
    }



}
