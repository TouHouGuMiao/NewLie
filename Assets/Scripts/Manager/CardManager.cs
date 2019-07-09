using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class CardManager  {
    private static CardManager _Instance;
    public static CardManager Instance {
        get {
            if (_Instance == null) {
                _Instance = new CardManager();
            }
            return _Instance;
        }
    }
    private List<EventCardData> CollectionCardList = new List<EventCardData>();
    public List<EventCardData> GetCollectionList() {
        //LoadEventCardsXml("EventCardsConfig", CollectionCardList);
        List<EventCardData> list = new List<EventCardData>();
        LoadEventCardsXml("EventCardsConfig", list);
        return list;
    }
    public EventCardData GetlistDataById(int id) {
        EventCardData data = null;
        for (int i = 0; i < CollectionCardList.Count; i++) {
            if (CollectionCardList[i].id == id) {
                data = CollectionCardList[i];
                break;
            }
        }
        return data;
    }

    private void LoadEventCardsXml(string pathName,List<EventCardData> list) {
        string path = "Config";
        string text = ResourcesManager.Instance.LoadConfig(path, pathName).text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(text);
        XmlNode root = xmlDoc.SelectSingleNode("Card");
        XmlNodeList nodeList = root.ChildNodes;

        foreach (XmlNode node in nodeList) {
            XmlNode id = node.SelectSingleNode("id");
            XmlNode name = node.SelectSingleNode("name");
            XmlNode Des = node.SelectSingleNode("Des");
            XmlNode spriteName = node.SelectSingleNode("spriteName");

            EventCardData data = new EventCardData();
            data.id = CommonHelper.Str2Int(id.InnerText);
            data.name = name.InnerText;
            data.Des = Des.InnerText;
            data.spriteName = spriteName.InnerText;

            list.Add(data);
        }
    }
    
}
