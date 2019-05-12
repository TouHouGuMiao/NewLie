using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public static class GUIManager
{
    private static Dictionary<string, KeyValuePair<GameObject, IView>> m_UIViewDic
        =new Dictionary<string, KeyValuePair<GameObject, IView>> ();


    public static GameObject InstantatePanel(string name)
    {
        GameObject prefab = ResourcesManager.Instance.LoadPanel(name);
        if(prefab == null)
        {
            Debug.LogError("prefab is null");
            return null;
        }
        GameObject panel = GameObject.Instantiate(prefab) as GameObject;
        GameObject UIRoot = GameObject.FindWithTag("UIRoot") as GameObject;

        if (panel == null || UIRoot == null)
        {
            Debug.LogError("panel is null or UIRoot is null");
            return null;
        }
        panel.name = name;
        panel.transform.localScale = Vector3.one;
        panel.transform.localPosition = Vector3.zero;
        panel.transform.SetParent(UIRoot.transform, false);

        return panel;
    }

    public static void ShowView(string name)
    {
        GameObject panel = null;
        IView view = null;
        KeyValuePair<GameObject, IView> found;

        if(!m_UIViewDic.TryGetValue(name,out found))
        {
            view = Assembly.GetExecutingAssembly().CreateInstance(name) as IView;
            panel = InstantatePanel(name);
            if (view == null || panel == null)
            {
                Debug.LogError("panel or view is null");
                return;
            }
            m_UIViewDic.Add(name, new KeyValuePair<GameObject, IView>(panel, view));
            view.Start();
        }

        else
        {
            panel = found.Key;
            view = found.Value;
            if (view == null || panel == null)
            {
                Debug.LogError("panel or view is null");
                return;
            }
        }

        foreach (KeyValuePair<string,KeyValuePair<GameObject,IView>> item in m_UIViewDic)
        {
            if (item.Value.Value.m_Layer != view.m_Layer) continue;

            if (!item.Value.Key.activeSelf) continue;

            if (item.Value.Value == view) continue;

            HideView(item.Key);
        }

        view.Show();
        panel.SetActive(true);
        
    }


    public static void HideView(string name)
    {
        KeyValuePair<GameObject, IView> pair;
     
        if(!m_UIViewDic.TryGetValue(name,out pair))
        {
            Debug.LogError(name + "has error");
            return;
        }
        pair.Value.Hide();
        pair.Key.SetActive(false);
    }

    public static GameObject FindPanel(string panelName)
    {
        KeyValuePair<GameObject, IView> pair;

        if(!m_UIViewDic.TryGetValue(panelName,out pair))
        {
            Debug.LogError(panelName + "not exit");
            return null;
        }
        return pair.Key;
    }

    public static GameObject GetRootPanel(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return null;
        }

        Transform parent = gameObject.transform.parent;
        if (parent == null)
        {
            UIPanel panel = gameObject.GetComponent<UIPanel>();
            return panel == null ? null : panel.gameObject;
        }

        UIPanel parentPanel = null;

        while (parent != null)
        {
            UIPanel panel = parent.GetComponent<UIPanel>();
            if (panel != null)
            {
                parentPanel = panel;
            }
            parent = parent.parent;
        }
        return parentPanel.gameObject;


    }

    public static IView FindView(GameObject go)
    {
        GameObject panel = GetRootPanel(go);

        if (panel == null)
        {
            return null;
        }
        KeyValuePair<GameObject, IView> pair;

        if(!m_UIViewDic.TryGetValue(panel.name,out pair))
        {
            Debug.LogError("not panel in dic");
            return null;
        }
        return pair.Value;
    }

    public static void Update()
    {
        List<IView> NeedUpdataViewList = new List<IView>();
        foreach (KeyValuePair<GameObject,IView> pair in m_UIViewDic.Values)
        {
            if (pair.Key.activeInHierarchy)
            {
                NeedUpdataViewList.Add(pair.Value);
            }
        }
        for (int i = 0; i < NeedUpdataViewList.Count; i++)
        {
            NeedUpdataViewList[i].Update();
        }
        
    }


 
    public static T GetChild<T>(this IView view,string name) where T:MonoBehaviour
    {
        Transform t = GetChild(view,name);
        if (t == null)
        {
            Debug.LogError("not child ");
            return null;
        }

        T t2 = t.GetComponent<T>();
        if (t2 == null)
        {
            Debug.LogError("not compent in it");
            return null;
        }
        return t2;


    }

    public static Transform GetChild(this IView view,string name)
    {
        GameObject panel=null;
        foreach (KeyValuePair<GameObject,IView> item in m_UIViewDic.Values)
        {
            if (view == item.Value)
            {
                panel = item.Key;
            }
        }
        if (panel == null)
        {
            Debug.LogError("not panel with view");
            return null;
        }

        Transform child= panel.transform.FindRecursively(name);
        if (child == null)
        {
            Debug.LogError("child is null---"+name);
            return null;
        }

        return child;

    }


}
