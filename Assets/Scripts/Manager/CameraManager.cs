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
    /// 特写角色在屏幕哪个位置
    /// </summary>
    public enum FeatureMode
    {
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

    #region 摄像机插值
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

    private float orthSizeLerpTime=0;


    private void CameraorthographicSizeLerp(float start,float end,float rate,EventDelegate OnLerpished=null)
    {
        orthSizeLerpTime = 0;
        IEnumeratorManager.Instance.StartCoroutine(CameraorthographicSizeLerp_IEnumerator(start, end, rate, OnLerpished));
    }
    private IEnumerator CameraorthographicSizeLerp_IEnumerator(float start,float end,float rate,EventDelegate OnLerpished)
    {
        yield return new WaitForSeconds(0.01f);
        main_Camera.orthographicSize = Mathf.Lerp(start, end, orthSizeLerpTime);

        if (orthSizeLerpTime >1)
        {
            if (OnLerpished != null)
            {
                List<EventDelegate> m_List = new List<EventDelegate>();
                m_List.Add(OnLerpished);
                EventDelegate.Execute(m_List);
            }
            yield break;
        }
        orthSizeLerpTime += rate;
        IEnumeratorManager.Instance.StartCoroutine(CameraorthographicSizeLerp_IEnumerator(start, end, rate, OnLerpished));
    }
    #endregion

    private Transform curBG;
    private void SetBlurNull()
    {
        BattleCamera.Instance.needMoveWithPlayer = true;
        if (leftCharacter != null)
        {
            Rigidbody leftRgb = leftCharacter.GetComponent<Rigidbody>();

            leftRgb.isKinematic = false;
            leftCharacter = null;
        }

        if (rightCharacter != null)
        {
            Rigidbody rightRgb = rightCharacter.GetComponent<Rigidbody>();

            rightRgb.isKinematic = false;
            rightCharacter = null;
        }

      

        for (int i = 0; i < curBG.transform.childCount; i++)
        {
            GameObject go = curBG.transform.GetChild(i).gameObject;
            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            if (render != null)
            {
                Material material = render.material;
                if (material != null)
                {

                    Material[] m_Materials = new Material[1];
                    m_Materials[0] = ResourcesManager.Instance.LoadMaterial("Normal");
                    render.materials = m_Materials;
                }
            }
        }
    }
   
    public void FeatureOver(string characterName,EventDelegate OnFeatureOver=null)
    {
        GameObject character = null;
        if (characterName == "Player")
        {
            character = GameObject.FindWithTag("Player");
        }
        else
        {
            character = curBG.transform.FindRecursively(characterName).gameObject;
        }
 
   
        TweenPosition tp = character.GetComponent<TweenPosition>();
        tp.enabled = true;
        tp.onFinished.Clear();
       
       
        tp.duration = 0.5f;
        if(characterName == "Player")
        {
            tp.onFinished.Add(new EventDelegate(PlayerControl.Instance.PlayIdle));
            tp.from = character.transform.position;
            tp.to = savaPos_1;
        }
        else
        {
            tp.from = character.transform.localPosition;
            tp.to = savaPos_2;
            tp.onFinished.Add(new EventDelegate(SetNPCCharacterIdle));
        }
        
        tp.onFinished.Add(new EventDelegate(SetBlurNull));
        tp.onFinished.Add(OnFeatureOver);
        tp.ResetToBeginning();

        TweenRotation cameraTR = main_Camera.GetComponent<TweenRotation>();
        cameraTR.enabled = true;
        float z = main_Camera.transform.rotation.z;
        if (z > 180)
        {
            z = z - 360;
        }
        cameraTR.from = new Vector3 (main_Camera.transform.rotation.eulerAngles.x, main_Camera.transform.rotation.eulerAngles.y,z);
        cameraTR.to = new Vector3(0, 0, 0);
        cameraTR.duration = 0.5F;
        cameraTR.ResetToBeginning();

        float start = main_Camera.orthographicSize;


        CameraorthographicSizeLerp(start, 3.5f, 0.05f);
    }


    private void SetNPCCharacterIdle()
    {
        NPCAnimatorManager.Instance.PlayCharacterAnimator(rightCharacter, "idle");
    }

    private Vector3 savaPos_1 = Vector3.zero;
    private GameObject leftCharacter;
    private GameObject rightCharacter;
    /// <summary>
    /// 玩家单帧特写（无拉镜
    /// </summary>
    public void Feature(NPCAnimatorManager.BGEnmu bgEnum, string animatorName)
    {
        Transform bg = null;

        if (bgEnum == NPCAnimatorManager.BGEnmu.ShenShe)
        {
            bg = GameObject.FindWithTag("ShenSheBG").transform;
        }

        else if (bgEnum == NPCAnimatorManager.BGEnmu.Village)
        {
            bg = GameObject.FindWithTag("VillageBG").transform;
        }
        curBG = bg;
        GameObject player = GameObject.FindWithTag("Player");
        leftCharacter = player;
        savaPos_1 = player.transform.position;

        for (int i = 0; i < bg.transform.childCount; i++)
        {
            GameObject go = bg.transform.GetChild(i).gameObject;
            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            if (render != null)
            {

                if (go != player)
                {
                    Material[] m_Materials = new Material[1];
                    m_Materials[0] = ResourcesManager.Instance.LoadMaterial("Blur");
                    render.materials = m_Materials;
                    render.material.SetFloat("_BlurSize", 2);
                }

                else
                {
                    Material[] m_Materials = new Material[1];
                    m_Materials[0] = ResourcesManager.Instance.LoadMaterial("Normal");
                    render.materials = m_Materials;

                }


            }
        }

        Material m_Material = player.GetComponent<SpriteRenderer>().material;
        m_Material = ResourcesManager.Instance.LoadMaterial("Normal");


        BattleCamera.Instance.needMoveWithPlayer = false;
        main_Camera.orthographicSize = 2.0f;

        OnOnlyOneCharacterFeatureFished();
  
        Rigidbody rgb = player.GetComponent<Rigidbody>();
        rgb.isKinematic = true;
        PlayerControl.Instance.PlayPlayerSkill(animatorName);
    }

    /// <summary>
    /// 玩家单个特写(带拉镜
    /// </summary>
    /// <param name="character"></param>
    /// <param name="animatorName"></param>
    /// <param name="rate"></param>
    public void Feature( NPCAnimatorManager.BGEnmu bgEnum,string animatorName,float rate)
    {
        Transform bg = null;

        if (bgEnum == NPCAnimatorManager.BGEnmu.ShenShe)
        {
            bg = GameObject.FindWithTag("ShenSheBG").transform;
        }

        else if (bgEnum == NPCAnimatorManager.BGEnmu.Village)
        {
            bg = GameObject.FindWithTag("VillageBG").transform;
        }
        curBG = bg;
        GameObject player = GameObject.FindWithTag("Player");
        leftCharacter = player;
        for (int i = 0; i < bg.transform.childCount; i++)
        {
            GameObject go = bg.transform.GetChild(i).gameObject;
            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            if (render != null)
            {

                if (go != player)
                {
                    Material[] m_Materials = new Material[1];
                    m_Materials[0] = ResourcesManager.Instance.LoadMaterial("Blur");
                    render.materials = m_Materials;
                    render.material.SetFloat("_BlurSize", 2);
                }

                else
                {
                    Material[] m_Materials = new Material[1];
                    m_Materials[0] = ResourcesManager.Instance.LoadMaterial("Normal");
                    render.materials = m_Materials;

                }
            }
        }

        Material m_Material = player.GetComponent<SpriteRenderer>().material;
        m_Material = ResourcesManager.Instance.LoadMaterial("Normal");


        BattleCamera.Instance.needMoveWithPlayer = false;
        CameraorthographicSizeLerp(3.5F, 2.0f, rate, new EventDelegate(OnOnlyOneCharacterFeatureFished));


        savaPos_1 = player.transform.position;
        Rigidbody rgb = player.GetComponent<Rigidbody>();
        rgb.isKinematic = true;
        PlayerControl.Instance.PlayPlayerSkill(animatorName);
    }


    private void OnOnlyOneCharacterFeatureFished()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 screenVec = new Vector3(Screen.width / 4, 80, Mathf.Abs(main_Camera.transform.position.z));
        TweenPosition characterTP = player.GetComponent<TweenPosition>();
        TweenRotation cameraTR = main_Camera.GetComponent<TweenRotation>();

        Vector3 targetVec = Camera.main.ScreenToWorldPoint(screenVec);
        player.transform.position = targetVec;

        cameraTR.enabled = true;
        cameraTR.from = new Vector3(0, 0, 0);
        cameraTR.to = new Vector3(0, 0, 3);
        cameraTR.duration = 3;
        cameraTR.onFinished.Clear();
        cameraTR.ResetToBeginning();

        characterTP.enabled = true;
        characterTP.from = player.transform.position;
        characterTP.duration = 3.0f;
        characterTP.onFinished.Clear();
        characterTP.to = new Vector3(player.transform.position.x + 1, player.transform.position.y, 0);
        characterTP.ResetToBeginning();
    }


 /// <summary>
 /// 特写单个角色摄像机以及角色移动细节操作
 /// </summary>
 /// <param name="mode"></param>
 /// <param name="featureCharacter"></param>
    private void OnOnlyOneCharacterFeatureFished(FeatureMode mode,GameObject featureCharacter)
    {
        Vector3 screenVec = Vector3.zero;
        if (mode == FeatureMode.left)
        {
           screenVec = new Vector3((Screen.width / 4) , 80, Mathf.Abs(main_Camera.transform.position.z));
        }

        else if(mode== FeatureMode.right)
        {
            screenVec = new Vector3((Screen.width / 4) * 3, 80, Mathf.Abs(main_Camera.transform.position.z));
        }
        TweenPosition characterTP = featureCharacter.GetComponent<TweenPosition>();
        TweenRotation cameraTR = main_Camera.GetComponent<TweenRotation>();

        Vector3 targetVec = Camera.main.ScreenToWorldPoint(screenVec);
        targetVec = targetVec - curBG.transform.position;
        featureCharacter.transform.position = targetVec;

        cameraTR.enabled = true;
        cameraTR.from = new Vector3(0, 0, 0);
      
        cameraTR.duration = 3;
        cameraTR.onFinished.Clear();
        

        characterTP.enabled = true;
        characterTP.from = featureCharacter.transform.position;
        characterTP.duration = 3.0f;
        characterTP.onFinished.Clear();

      
        if (mode== FeatureMode.left)
        {
            cameraTR.to = new Vector3(0, 0, 3);
            characterTP.to = new Vector3(featureCharacter.transform.position.x + 1, featureCharacter.transform.position.y, 0);
            cameraTR.ResetToBeginning();
            characterTP.ResetToBeginning();
        }

        else if(mode == FeatureMode.right)
        {
            cameraTR.to = new Vector3(0, 0, -3);
            characterTP.to = new Vector3(featureCharacter.transform.position.x - 1, featureCharacter.transform.position.y, 0);
            cameraTR.ResetToBeginning();
            characterTP.ResetToBeginning();
        }
     
    }

    /// <summary>
    /// 双人特写
    /// </summary>
    /// <param name="bgEnum"></param>
    /// <param name="player"></param>
    /// <param name="player_AnimatorName"></param>
    /// <param name="npc"></param>
    /// <param name="Npc_AnimatorName"></param>
    public void Feature(NPCAnimatorManager.BGEnmu bgEnum,string player_AnimatorName,GameObject npc,string Npc_AnimatorName)
    {
        Transform bg = null;

        if (bgEnum == NPCAnimatorManager.BGEnmu.ShenShe)
        {
            bg = GameObject.FindWithTag("ShenSheBG").transform;
        }

        else if (bgEnum == NPCAnimatorManager.BGEnmu.Village)
        {
            bg = GameObject.FindWithTag("VillageBG").transform;
        }
        curBG = bg;
      
    }

    /// <summary>
    /// 一般NPC使用的pos保存
    /// </summary>
    private Vector3 savaPos_2;
    /// <summary>
    /// NPC单个特写
    /// </summary>
    /// <param name="bgEnum"></param>
    /// <param name="characterName"></param>
    public void Feature(NPCAnimatorManager.BGEnmu bgEnum,string characterName,string aniamtorName)
    {
        Transform bg = null;

        if (bgEnum == NPCAnimatorManager.BGEnmu.ShenShe)
        {
            bg = GameObject.FindWithTag("ShenSheBG").transform;
        }

        else if (bgEnum == NPCAnimatorManager.BGEnmu.Village)
        {
            bg = GameObject.FindWithTag("VillageBG").transform;
        }
        curBG = bg;
        GameObject character = bg.transform.FindRecursively(characterName).gameObject;
        rightCharacter = character;
        savaPos_2 = character.transform.localPosition;
        for (int i = 0; i < bg.transform.childCount; i++)
        {
            GameObject go = bg.transform.GetChild(i).gameObject;
            SpriteRenderer render = go.GetComponent<SpriteRenderer>();
            if (render != null)
            {

                if (go != character)
                {
                    Material[] m_Materials = new Material[1];
                    m_Materials[0] = ResourcesManager.Instance.LoadMaterial("Blur");
                    render.materials = m_Materials;
                    render.material.SetFloat("_BlurSize", 2);
                }

                else
                {
                    Material[] m_Materials = new Material[1];
                    m_Materials[0] = ResourcesManager.Instance.LoadMaterial("Normal");
                    render.materials = m_Materials;

                }
            }
        }
        Material m_Material = character.GetComponent<SpriteRenderer>().material;
        m_Material = ResourcesManager.Instance.LoadMaterial("Normal");


        BattleCamera.Instance.needMoveWithPlayer = false;
        main_Camera.orthographicSize = 2.0f;

        OnOnlyOneCharacterFeatureFished(FeatureMode.right,character);

        Rigidbody rgb = character.GetComponent<Rigidbody>();
        rgb.isKinematic = true;
        NPCAnimatorManager.Instance.PlayCharacterAnimator(character, aniamtorName);
    }
}
