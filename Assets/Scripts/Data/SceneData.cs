using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData
{
    public int ID;
    public string sceneName;
    public string stateName;
}

public class SceneDataManager
{
    private static SceneDataManager _instance=null;

    public static SceneDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SceneDataManager();
            }
            return _instance;
        }
    }

    public SceneData GetSceneData(int id)
    {
        if (sceneDataDic == null)
        {
            sceneDataDic = new Dictionary<int, global::SceneData>();
            initDic();
        }

        SceneData sceneData = null;
        if(!sceneDataDic.TryGetValue(id,out sceneData))
        {
            Debug.LogError("not sceneData in Dic");
            return null;
        }
        return sceneData;
    }

    private Dictionary<int, SceneData> sceneDataDic;

    private void initDic()
    {

        SceneData m_SceneData = new SceneData();
        m_SceneData.ID = 1;
        m_SceneData.sceneName = "LoginScene";
        m_SceneData.stateName = "LoginState";
        sceneDataDic.Add(m_SceneData.ID, m_SceneData);

        SceneData m_SceneData1 = new SceneData();
        m_SceneData1.ID = 2;
        m_SceneData1.sceneName = "CityScene";
        m_SceneData1.stateName = "CityState";
        sceneDataDic.Add(m_SceneData1.ID, m_SceneData1);

        SceneData m_SceneData2 = new SceneData();
        m_SceneData2.ID = 3;
        m_SceneData2.sceneName = "BattleSence";
        m_SceneData2.stateName = "BattleState";
        sceneDataDic.Add(m_SceneData2.ID, m_SceneData2);



        SceneData m_SceneData3 = new SceneData();
        m_SceneData3.ID = 4;
        m_SceneData3.sceneName = "Stage0";
        m_SceneData3.stateName = "Stage0State";
        sceneDataDic.Add(m_SceneData3.ID, m_SceneData3);


    }

}

