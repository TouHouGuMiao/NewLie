using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameStateManager : MonoBehaviour
{
    private static  Dictionary<string, GameState> GameStateDic;
    private static GameState m_CurState;
  
    private void Start()
    {
        GameStateDic = new Dictionary<string, GameState>();
        m_CurState = null;
        LoadScene(1);
    }

    static  void  SetState(GameState state)
    {
        if (state == null)
        {
            Debug.LogError("state is null");
            return;
        }

        if (m_CurState != state && m_CurState != null)
        {
            m_CurState.Stop();
        }
        m_CurState = state;
        state.Start();

    }

    public static void LoadScene(int id)
    {
        SceneData data = SceneDataManager.Instance.GetSceneData(id);
        if (data == null)
        {
            Debug.LogError("data is null");
            return;
        }
        if (string.IsNullOrEmpty(data.sceneName) || string.IsNullOrEmpty(data.stateName))
        {
            Debug.LogError("sceneName is null or stateName is null");
            return;
        }

        GameState state = null;
        if (!GameStateDic.TryGetValue(data.stateName,out state))
        {
            //Debug.LogError(data.stateName)
            state = Assembly.GetExecutingAssembly().CreateInstance(data.stateName) as GameState;
            if (state == null)
            {
                Debug.LogError("state is null");
                return;
            }
            GameStateDic.Add(data.stateName, state);
        }
        SetState(state);
        DownLoadManager.Instance.LoadScene(data.sceneName, state.LoadComplete);
    }
}
