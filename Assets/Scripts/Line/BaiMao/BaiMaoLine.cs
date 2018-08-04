using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaiMaoLine:MonoBehaviour
{
    void Line_1()
    {
        StoryData data = new StoryData();
        data.id = 1;
        data.state = 0;
        data.name = "村民";
        string speak1 = "可恶的妖怪，竟然跑到村子里偷东西!";
        string speak2 = "就这样打死它吧。";
        string speak3 = "哦，是博丽大人。博丽大人，这个妖怪潜入村子里偷东西，被我们发现了。";
        data.SpeakList.Add(speak1);
        data.SpeakList.Add(speak2);
        data.SpeakList.Add(speak3);
        data.cout = 3;
        data.index = 0;
        StoryPanel.data = data;
        GUIManager.ShowView("StoryPanel");
    }
}
