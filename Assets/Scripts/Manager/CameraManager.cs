using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager
{
    public enum CameraEnum
    {
        UICmera,
        MainCmera,
    }

    /// <summary>
    /// 摄像机向哪个方向倾斜，player动画特殊处理
    /// </summary>
    public enum FeatureMode
    {
        player,
        left,
        right,
    }

    private static CameraManager _Instance = null;

    public static CameraManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new CameraManager();
                main_Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            }
            return _Instance;
        }
    }
    private Camera m_UICamera=null;
    private static  Camera main_Camera = null;
    private float lerpTime = 0;


    /// <summary>
    /// centerPoint指相机缩放的中心点，scale指相机最终拉伸幅度0-1,1为正常大小，如果是场景相机5是正常大小，rate指每次递归累加的插值时间。
    /// </summary>
    /// <param name="scale"></param>
    /// <param name="rate"></param>
    public void DrawCameraLens(CameraEnum m_Enmu,Vector3 centerPoint,float scale,float rate)
    {
        float size = 0;
        lerpTime = 0;
        if(m_Enmu == CameraEnum.UICmera)
        {
            if (m_UICamera == null)
            {
                m_UICamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
            }
            size = m_UICamera.orthographicSize;
        
        }

        else if(m_Enmu == CameraEnum.MainCmera)
        {
            size = main_Camera.orthographicSize;
        }

        IEnumeratorManager.Instance.StartCoroutine(DrawUICameraLens_IEnumerator(m_Enmu,centerPoint, scale, rate, size));

    }

    private IEnumerator DrawUICameraLens_IEnumerator(CameraEnum m_Enmu,Vector3 centerPoint, float scale,float rate,float startSize)
    {
        yield return new WaitForSeconds(0.01f);
        if(m_Enmu== CameraEnum.UICmera)
        {
            m_UICamera.orthographicSize = Mathf.Lerp(startSize, scale, lerpTime);
            m_UICamera.transform.localPosition = Vector3.Lerp(m_UICamera.transform.localPosition, centerPoint, lerpTime);
        }

        else if(m_Enmu== CameraEnum.MainCmera)
        {
            centerPoint = new Vector3(centerPoint.x, centerPoint.y, main_Camera.transform.position.z);
            main_Camera.orthographicSize = Mathf.Lerp(startSize, scale, lerpTime);
            main_Camera.transform.localPosition = Vector3.Lerp(main_Camera.transform.position, centerPoint, lerpTime);
        }
      
        lerpTime += rate;

        if (lerpTime >= 1)
        {
            yield return null;
        }
        else
        {
           IEnumeratorManager.Instance.StartCoroutine(DrawUICameraLens_IEnumerator(m_Enmu,centerPoint, scale, rate,startSize));
        }
    }


    /// <summary>
    /// 单帧特写
    /// </summary>
    public void Feature(FeatureMode mode,GameObject character,string spriteName)
    {
        BattleCamera.Instance.needMoveWithPlayer = false;
        main_Camera.orthographicSize = 2.0f;
        TweenPosition characterTP = character.GetComponent<TweenPosition>();
        TweenRotation cameraTR = main_Camera.GetComponent<TweenRotation>();
        if(mode== FeatureMode.player)
        {
            Rigidbody rgb = character.GetComponent<Rigidbody>();
            rgb.isKinematic = true;
            Vector3 screenVec = new Vector3(Screen.width / 4, 80, Mathf.Abs(main_Camera.transform.position.z));
            PlayerControl.Instance.PauseAnimator();
            SpriteRenderer spriteRenderer = character.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = ResourcesManager.Instance.LoadSprite(spriteName);
            Vector3 targetVec = Camera.main.ScreenToWorldPoint(screenVec);
            character.transform.position = targetVec;

            cameraTR.enabled = true;
            cameraTR.from = new Vector3(0, 0, 0);
            cameraTR.to = new Vector3(0, 0, 3);
            cameraTR.ResetToBeginning();

            characterTP.enabled = true;
            characterTP.from = character.transform.position;
            characterTP.to = new Vector3(character.transform.position.x + 1, character.transform.position.y, 0);
            characterTP.ResetToBeginning();
        }
       
    }


}
