using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimatorManager  {
    public enum BGEnmu
    {
        ShenShe,
        Village,
    }

    private static NPCAnimatorManager _Instance = null;

    public static NPCAnimatorManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new NPCAnimatorManager();
            }
            return _Instance;
        }
    }

 

    public void PlayCharacterAnimator(GameObject go,string name)
    {
        Animator m_Animator = go.GetComponent<Animator>();
        m_Animator.Play(name);
    }

    public void PlayCharacterAnimator(BGEnmu bGEnmu, string goName, string animatorName)
    {
        Transform parentBG = null;

     
        if(bGEnmu== BGEnmu.ShenShe)
        {
            parentBG = GameObject.FindWithTag("ShenSheBG").transform;
        }

        else if(bGEnmu == BGEnmu.Village)
        {
            parentBG = GameObject.FindWithTag("VillageBG").transform;
        }

        GameObject go = parentBG.transform.FindRecursively(goName).gameObject;
        if (animatorName == "rotate")
        {
            go.transform.Rotate(new Vector3(0, 180, 0));
        }
        else
        {
            Animator m_Animator = go.GetComponent<Animator>();
            if (m_Animator == null)
            {
                Debug.LogError("NPC Animator is null" + "__" + bGEnmu + "__" + goName);
                return;
            }
            m_Animator.Play(animatorName);
        }
  
    }


    public void PlayCharacterTweenScale(BGEnmu bGEnmu, string goName, EventDelegate OnTSFished = null)
    {
        Transform parentBG = null;


        if (bGEnmu == BGEnmu.ShenShe)
        {
            parentBG = GameObject.FindWithTag("ShenSheBG").transform;
        }

        else if (bGEnmu == BGEnmu.Village)
        {
            parentBG = GameObject.FindWithTag("VillageBG").transform;
        }


      

        GameObject go = parentBG.transform.FindRecursively(goName).gameObject;
        TweenScale ts = go.GetComponent<TweenScale>();
        ts.enabled = true;
        ts.duration = 1.0f;
        ts.ignoreTimeScale = false;
        ts.onFinished.Clear();
        ts.onFinished.Add(OnTSFished);
        ts.ResetToBeginning();
        AudioManager.Instance.PlayEffect_Source("TweenScale");
    }


    public void PlayCharacterTweenScale(BGEnmu bGEnmu,CameraManager.FeatureMode modeEnmu, string goName,EventDelegate OnTSFished=null)
    {
        Transform parentBG = null;



        if (bGEnmu == BGEnmu.ShenShe)
        {
            parentBG = GameObject.FindWithTag("ShenSheBG").transform;
        }

        else if (bGEnmu == BGEnmu.Village)
        {
            parentBG = GameObject.FindWithTag("VillageBG").transform;
        }

        GameObject main_Camera = GameObject.FindWithTag("MainCamera");
        TweenRotation tr = main_Camera.GetComponent<TweenRotation>();
        tr.enabled = true;
        Vector3 startVec = main_Camera.transform.rotation.eulerAngles;
        if (startVec.z > 180)
        {
            startVec = new Vector3(startVec.x, startVec.y, (startVec.z - 360));
        }
        if (startVec.y > 180)
        {
            startVec = new Vector3(startVec.x, startVec.y - 360, startVec.z);
        }
        tr.from = startVec;
        if(modeEnmu== CameraManager.FeatureMode.left)
        {
            tr.to = new Vector3(0, 0, 0);
        }

        else
        {
            tr.to = new Vector3(0,- 1, 0);
        }
        tr.onFinished.Clear();
        tr.ResetToBeginning();

        GameObject go = null;
        if (goName == "player")
        {
            go = GameObject.FindWithTag("Player");
        }
        else
        {
            go = parentBG.transform.FindRecursively(goName).gameObject;
        }     
        TweenScale ts = go.GetComponent<TweenScale>();
        ts.enabled = true;
        ts.duration = 1.0f;
        ts.ignoreTimeScale = false;
        ts.onFinished.Clear();
        ts.onFinished.Add(OnTSFished);
        ts.ResetToBeginning();
        AudioManager.Instance.PlayEffect_Source("TweenScale");
    }
 




}
