using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DownLoadManager : MonoBehaviour
{
    public static DownLoadManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public delegate void HandBackCall(params object[] args); 

    public void LoadScene(string name,HandBackCall HandCall,params object[] args)
    {
        StartCoroutine(LoadSceneBundle(name, HandCall, args));
    }
    AsyncOperation asyn = null;

    public bool isDo
    {
        get
        {
           return asyn.isDone;
        }
        private set { }
    }

    IEnumerator LoadSceneBundle(string name, HandBackCall HandCall,params object[] args)
    {
        asyn = SceneManager.LoadSceneAsync(name);
        yield return asyn;
        Resources.UnloadUnusedAssets();
        GC.Collect();
        if (HandCall != null)
        {
            HandCall(args);
        }
        //asyn = null;


    }

    
}
