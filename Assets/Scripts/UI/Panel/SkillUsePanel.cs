using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUsePanel : IView
{
    private enum ArrowState
    {
        moveToUp=1,
        moveToDown=2,
    }
    private static  UIGrid targetWidget;
    private static ArrowState arrowState = ArrowState.moveToUp;
    /// <summary>
    /// 用于传输数据的工具
    /// </summary>
    private static  List<Skill> skillList = new List<Skill>();
    private float radius=1.0f;
    private static Transform cardWidget;
    private static  Transform arrow;
    private static  GameObject infoItem;
    /// <summary>
    /// 记录所有已有Skill的List
    /// </summary>
    private static List<Skill> allSkillList = new List<Skill>();

    public static void ClearAllSkillList()
    {
        allSkillList.Clear();
    }

    public SkillUsePanel()
    {
        m_Layer = Layer.UseSkill;
    }

    protected override void OnStart()
    {
        arrow = this.GetChild("arrow");
        UIButton arrowBtn = arrow.GetComponent<UIButton>();
        arrowBtn.onClick.Add(new EventDelegate(OnUpArrowBtnClick));
        cardWidget = this.GetChild("CardWidget");
        targetWidget = this.GetChild("TargetWidget").GetComponent<UIGrid>();
        infoItem = this.GetChild("target").gameObject;
    }

    protected override void OnShow()
    {

    }

    static void SetSkillCardPos(bool isCoexist)
    {
        int count = 0;

        if (isCoexist)
        {
            count = skillList.Count + cardWidget.childCount;
        }

        else
        {
            count = skillList.Count;
        }
      
    
        if (count == 0)
        {
            cardWidget.DestroyChildren();
            return;
        }
       
        for (int i = 0; i < skillList.Count; i++)
        {
            GameObject go = GameObject.Instantiate(skillList[i].cardPrefab);
            go.transform.SetParent(cardWidget, false);
            go.name = skillList[i].data.Name;
            DragSkillCard cardDrag = go.GetComponent<DragSkillCard>();
            cardDrag.OnCardDragFished.Clear();
            cardDrag.OnCardDragFished.Add( new EventDelegate (OnCardDragFished));
            cardDrag.finshed_y = -1.5f;
           
            go.tag = "SkillCard";
         
        }

        GridCard(count);

    }

    private static  void GridCard(int count)
    {
        Vector2 centerPoint = new Vector2(0, 0);
        float offset_x = 3.0f / (count + 1);
        float time_x = 1.0f / (count + 1);
        for (int i = 0; i < count; i++)
        {
            GameObject go = cardWidget.transform.GetChild(i).gameObject;
            float sphere_x;

            sphere_x = -0.5f + (i + 1) * time_x;


            float sphere_y = Mathf.Sqrt((1 - (sphere_x * sphere_x)));
            float go_y = -5.0f / sphere_y;
            Vector2 vecPoint = new Vector2(sphere_x, sphere_y);
            Vector2 oppositeVec = vecPoint - centerPoint;
            float angle = Mathf.Atan2(oppositeVec.x, oppositeVec.y) * Mathf.Rad2Deg;
            go.transform.rotation = Quaternion.Euler(go.transform.rotation.x, 180, angle);
            go.transform.localPosition = new Vector3(-1.5f + (i + 1) * offset_x, go_y, 5 + i * 0.04f);
        }
    }

    protected override void OnDestroy()
    {
     
    }

    protected override void OnHide()
    {
       
    }


    static void OnUpArrowBtnClick()
    {
        TweenPosition arrow_Tp = arrow.GetComponent<TweenPosition>();
        TweenRotation arrow_RT = arrow.GetComponent<TweenRotation>();
        TweenPosition cardWidget_Tp = cardWidget.GetComponent<TweenPosition>();
        arrow_Tp.enabled = true;
        cardWidget_Tp.enabled = true;
        arrow_RT.enabled = true;
        if (arrowState== ArrowState.moveToUp)
        {
            arrow_Tp.from = arrow_Tp.transform.localPosition;
            arrow_Tp.to = new Vector3(0, -183.9f, 0);
            arrowState = ArrowState.moveToDown;

            cardWidget_Tp.from = cardWidget_Tp.transform.localPosition;
            cardWidget_Tp.to = new Vector3(0, 2, 0);

            arrow_RT.from = arrow_RT.transform.rotation.eulerAngles;
            arrow_RT.to = new Vector3(0, 0, 180);
        }
        else if(arrowState == ArrowState.moveToDown)
        {
            arrow_Tp.from = arrow_Tp.transform.localPosition;
            arrow_Tp.to = new Vector3(0, -518.94f, 0);
            arrowState = ArrowState.moveToUp;


            cardWidget_Tp.from = cardWidget_Tp.transform.localPosition;
            cardWidget_Tp.to = new Vector3(0, -1, 0);


            arrow_RT.from = arrow_RT.transform.rotation.eulerAngles;
            arrow_RT.to = new Vector3(0, 0, 0);
        }
        arrow_Tp.ResetToBeginning();
        cardWidget_Tp.ResetToBeginning();
        arrow_RT.ResetToBeginning();
    }

    public static void UpdataUseSkill(List<Skill> m_SkillList,bool mustCheck=false,bool isCoexist=false)
    {
        if (m_SkillList == null)
        {
            skillList.Clear();
        }
        else
        {
            skillList = m_SkillList;
            for (int i = 0; i < m_SkillList.Count; i++)
            {
                bool needAdd = true;
                for (int j = 0; j < allSkillList.Count; j++)
                {
                    if (allSkillList[j].data.Name == m_SkillList[i].data.Name)
                    {
                        allSkillList[j].TargetWithHanderDic = m_SkillList[i].TargetWithHanderDic;
                        needAdd = false;
                    }  
                }
                if (needAdd)
                {
                    allSkillList.Add(m_SkillList[i]);
                }

            }
        }

        if (mustCheck)
        {
            arrowState = ArrowState.moveToUp;
        }
        else
        {
            arrowState = ArrowState.moveToDown;
        }
      
        OnUpArrowBtnClick();
        SetSkillCardPos(isCoexist);
    }

    static void OnCardDragFished()
    {
        arrowState = ArrowState.moveToDown;
        OnUpArrowBtnClick();
        DragSkillCard current = DragSkillCard.current;
        string name = current.gameObject.name;
        Skill skill = null;
        for (int i = 0; i < allSkillList.Count; i++)
        {
            if (name == allSkillList[i].data.Name)
            {
                skill = allSkillList[i];
            }
        }
        List<string> targetNameList = new List<string>();
        foreach (string item in skill.TargetWithHanderDic.Keys)
        {
            targetNameList.Add(item);
        }
        int count = targetWidget.transform.childCount;
        for (int i = 0; i < targetNameList.Count; i++)
        {
            GameObject go = null;
            if (i < count)
            {
                go = targetWidget.transform.GetChild(i).gameObject;
                UISprite sprite = go.transform.FindRecursively("Sprite").GetComponent<UISprite>();
                sprite.spriteName = targetNameList[i];
            }

            else
            {
                go = GameObject.Instantiate(infoItem);
                go.transform.SetParent(targetWidget.transform, false);
            }
            UIButton button = go.GetComponent<UIButton>();
            button.onClick.Clear();
            button.onClick.Add(skill.TargetWithHanderDic[targetNameList[i]]);
            button.onClick.Add(new EventDelegate(OnTargetBtnClick));
            UILabel nameLabel = go.transform.FindRecursively("nameLabel").GetComponent<UILabel>();
            if (targetNameList[i] == "myself")
            {
                nameLabel.text = "自己";
            }

            else if(targetNameList[i]== "scene")
            {
                nameLabel.text = "环境";
            }
            go.name = targetNameList[i];
            go.SetActive(true);
        }

        for (int i = targetWidget.transform.childCount-1; i >= targetNameList.Count; i--)
        {
            GameObject go = targetWidget.transform.GetChild(i).gameObject;
            go.SetActive(false);
        }
        targetWidget.gameObject.SetActive(true);
        targetWidget.Reposition();
    }


    static  void OnTargetBtnClick()
    {
        targetWidget.gameObject.SetActive(false);
    }

    public static void MoveSkillInPanel(int id)
    {
        for (int i = 0; i < allSkillList.Count; i++)
        {
            if (allSkillList[i].data.ID == id)
            {
                allSkillList.Remove(allSkillList[i]);
            }
        }
        int count = cardWidget.transform.childCount;
        Skill skill = SkillManager.Instance.GetSkillDataById(id);
        GameObject go = cardWidget.transform.Find(skill.data.Name).gameObject;
        if (go == null)
        {
            Debug.LogError("Not has this skill");
            return;
        }
        GameObject.Destroy(go);
        count--;
        GridCard(count);

    }
 
}
