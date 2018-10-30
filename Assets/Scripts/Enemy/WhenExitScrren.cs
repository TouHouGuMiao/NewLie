using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhenExitScrren:MonoBehaviour
{
    public static WhenExitScrren instance;

    private float radius_Long;
    private float radius_Short;
    public GameObject hand;
    public GameObject player;
    public GameObject marisa;
    public bool isShow=false;
    private float empty = 4;//空出empty份高的空隙;

    private List<Vector2> ScreenPointList=new List<Vector2> ();


    private void Awake()
    {
        instance = this;
        AddPoint();
    }

    private void Start()
    {
        
    }

    void AddPoint()
    {
        float oneHeight = Screen.height / 50;
        float oneWidth = Screen.width / 50;

        float height= empty * oneHeight;
        float widht = empty * oneHeight;
        for (int i = 0; i < 50- empty; i++)
        {
            Vector2 vec1 = new Vector2(empty * oneHeight, height);
   
            ScreenPointList.Add(vec1);
            Vector2 vec2 = new Vector2(widht, empty * oneHeight);
            ScreenPointList.Add(vec2);
            Vector2 vec3 = new Vector2(Screen.width- empty * oneHeight, height);
            ScreenPointList.Add(vec3);
            Vector2 vec4 = new Vector2(widht, Screen.height- empty * oneHeight);
            ScreenPointList.Add(vec4);
            height += oneHeight;
            widht += oneWidth;
        }
    }


    private void Update()
    {
        if (isShow)
        {
            Vector2 moveVec;
            moveVec= Camera.main.ScreenToWorldPoint(ScreenPointList[0]);
            float temp=Vector2.Distance(Camera.main.ScreenToWorldPoint(ScreenPointList[0]),marisa.transform.position);
            foreach (Vector2 item in ScreenPointList)
            {
                Vector3 tempVec= Camera.main.ScreenToWorldPoint(item);
                float distance = Vector2.Distance(tempVec, marisa.transform.position);
                if (distance <= temp)
                {
                    temp = distance;
                    moveVec = tempVec;
                }             
            }
            hand.transform.position = moveVec;
            Vector3 angleVec = hand.transform.InverseTransformPoint(marisa.transform.position);
            float angle = Mathf.Atan2(angleVec.y, angleVec.x) * Mathf.Rad2Deg;
            hand.transform.Rotate(new Vector3(0, 0, 1) * angle);
            hand.SetActive(true);
        }

        else
        {
            hand.SetActive(false);
        }
    }

}
