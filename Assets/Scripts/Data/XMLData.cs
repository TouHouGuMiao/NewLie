using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;


public class XMLDataManager
{
    private static XMLDataManager _instance=null;
    public static XMLDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new XMLDataManager();
            }
            return _instance;
        }
    }

    private List<CardData> HasDataList = new List<CardData>();


    public List<CardData> GetHasList()
    {
        HasDataList.Sort(Tools.CompareByRank);
        return HasDataList;
    }



    public CardData GetHasCardDataById(int id)
    {
        if (HasDataList == null)
        {
            LoadXmlData();
        }

        for (int i = 0; i < HasDataList.Count; i++)
        {
            if (HasDataList[i].heroData.id == id)
            {
                return HasDataList[i];
            }
        }
        return null;
    }

    public void LoadXmlData()
    {
        HasDataList.Clear();
        HasDataList = CardDataManager.Instance.GetInitCardList();
        string filePath = Application.dataPath + @"/Resources/Config/PlayerConfig.xml";

        if (!File.Exists(filePath))
        {
            Debug.LogError("not fiel in it");
            return;
        }

        else if (File.Exists(filePath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            XmlNode node = xmlDoc.SelectSingleNode("Hero");
            XmlNodeList nodeList = node.ChildNodes;

            foreach (XmlNode item in nodeList)
            {
                XmlNode id = item.SelectSingleNode("id");
                XmlNode name = item.SelectSingleNode("name");
                XmlNode des = item.SelectSingleNode("des");
                XmlNode starLv = item.SelectSingleNode("startLv");
                XmlNode spriteName = item.SelectSingleNode("spriteName");

                CardData m_data = new CardData();
                m_data.heroData.id = CommonHelper.Str2Int(id.InnerText);
                m_data.heroData.name = name.InnerText;
                m_data.heroData.starLv = CommonHelper.Str2Int(starLv.InnerText);
                m_data.heroData.spriteName = spriteName.InnerText;
                m_data.heroData.des = des.InnerText;
                m_data.isHas = true;
                m_data.num = 1;
                for (int i = 0; i <HasDataList.Count; i++)
                {
                    if (m_data.heroData.id == HasDataList[i].heroData.id)
                    {
                        HasDataList[i].num += m_data.num;
                    }    
                }     
            }

          
            for (int i = 0; i < HasDataList.Count; i++)
            {
                if (HasDataList[i].num >= 1)
                {
                    HasDataList[i].isHas = true;
                }
            }
        }
    }

    public void CraetOrSaveXml(CardData data)
    {
        string filepath = Application.dataPath + @"/Resources/Config/PlayerConfig.xml";

        if (!File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("Hero");
            root.SetAttribute("name", "PlayerData");
            XmlElement HeroData = xmlDoc.CreateElement("HeroData");
            XmlElement id = xmlDoc.CreateElement("id");
            id.InnerText = data.heroData.id.ToString();
            XmlElement name = xmlDoc.CreateElement("name");
            name.InnerText = data.heroData.name;
            XmlElement des = xmlDoc.CreateElement("des");
            des.InnerText = data.heroData.des;
            XmlElement starLv = xmlDoc.CreateElement("startLv");
            starLv.InnerText = data.heroData.starLv.ToString();
            XmlElement spriteName = xmlDoc.CreateElement("spriteName");
            spriteName.InnerText = data.heroData.spriteName;
          

            HeroData.AppendChild(id);
            HeroData.AppendChild(name);
            HeroData.AppendChild(des);
            HeroData.AppendChild(starLv);
            HeroData.AppendChild(spriteName);
            root.AppendChild(HeroData);

            xmlDoc.AppendChild(root);
            xmlDoc.Save(filepath);
        }

        else if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);

            XmlNode root = xmlDoc.SelectSingleNode("Hero");
            XmlElement HeroData = xmlDoc.CreateElement("HeroData");
            XmlElement id = xmlDoc.CreateElement("id");
            id.InnerText = data.heroData.id.ToString();
            XmlElement name = xmlDoc.CreateElement("name");
            name.InnerText = data.heroData.name;
            XmlElement des = xmlDoc.CreateElement("des");
            des.InnerText = data.heroData.des;
            XmlElement starLv = xmlDoc.CreateElement("startLv");
            starLv.InnerText = data.heroData.starLv.ToString();
            XmlElement spriteName = xmlDoc.CreateElement("spriteName");
            spriteName.InnerText = data.heroData.spriteName;

            HeroData.AppendChild(id);
            HeroData.AppendChild(name);
            HeroData.AppendChild(des);
            HeroData.AppendChild(starLv);
            HeroData.AppendChild(spriteName);
            root.AppendChild(HeroData);

            xmlDoc.AppendChild(root);
            xmlDoc.Save(filepath);

        }
        LoadXmlData();
    }


    //public void CraetOrSaveGroundXml(CardData data)
    //{
    //    string filepath = Application.dataPath + @"/Resources/Config/GroundCardsConfig.xml";

    //    if (!File.Exists(filepath))
    //    {
    //        XmlDocument xmlDoc = new XmlDocument();
    //        XmlElement root = xmlDoc.CreateElement("Hero");
    //        root.SetAttribute("name", "PlayerData");
    //        XmlElement HeroData = xmlDoc.CreateElement("HeroData");
    //        XmlElement id = xmlDoc.CreateElement("id");
    //        id.InnerText = data.heroData.id.ToString();
    //        XmlElement name = xmlDoc.CreateElement("name");
    //        name.InnerText = data.heroData.name;
    //        XmlElement des = xmlDoc.CreateElement("des");
    //        des.InnerText = data.heroData.des;
    //        XmlElement starLv = xmlDoc.CreateElement("startLv");
    //        starLv.InnerText = data.heroData.starLv.ToString();
    //        XmlElement spriteName = xmlDoc.CreateElement("spriteName");
    //        spriteName.InnerText = data.heroData.spriteName;


    //        HeroData.AppendChild(id);
    //        HeroData.AppendChild(name);
    //        HeroData.AppendChild(des);
    //        HeroData.AppendChild(starLv);
    //        HeroData.AppendChild(spriteName);
    //        root.AppendChild(HeroData);

    //        xmlDoc.AppendChild(root);
    //        xmlDoc.Save(filepath);
    //    }

    //    else if (File.Exists(filepath))
    //    {
    //        XmlDocument xmlDoc = new XmlDocument();
    //        xmlDoc.Load(filepath);

    //        XmlNode root = xmlDoc.SelectSingleNode("Hero");
    //        XmlElement HeroData = xmlDoc.CreateElement("HeroData");
    //        XmlElement id = xmlDoc.CreateElement("id");
    //        id.InnerText = data.heroData.id.ToString();
    //        XmlElement name = xmlDoc.CreateElement("name");
    //        name.InnerText = data.heroData.name;
    //        XmlElement des = xmlDoc.CreateElement("des");
    //        des.InnerText = data.heroData.des;
    //        XmlElement starLv = xmlDoc.CreateElement("startLv");
    //        starLv.InnerText = data.heroData.starLv.ToString();
    //        XmlElement spriteName = xmlDoc.CreateElement("spriteName");
    //        spriteName.InnerText = data.heroData.spriteName;

    //        HeroData.AppendChild(id);
    //        HeroData.AppendChild(name);
    //        HeroData.AppendChild(des);
    //        HeroData.AppendChild(starLv);
    //        HeroData.AppendChild(spriteName);
    //        root.AppendChild(HeroData);

    //        xmlDoc.AppendChild(root);
    //        xmlDoc.Save(filepath);

    //    }

    //}

    //public void LoadGroundsXmlData()
    //{
    //    GroundCardList.Clear();
    //    GroundCardList = CardDataManager.Instance.GetInitCardList();
    //    string filePath = Application.dataPath + @"/Resources/Config/GroundCardsConfig.xml";

    //    if (!File.Exists(filePath))
    //    {
    //        Debug.LogError("not fiel in it");
    //        return;
    //    }

    //    else if (File.Exists(filePath))
    //    {
    //        XmlDocument xmlDoc = new XmlDocument();
    //        xmlDoc.Load(filePath);

    //        XmlNode node = xmlDoc.SelectSingleNode("Hero");
    //        XmlNodeList nodeList = node.ChildNodes;

    //        foreach (XmlNode item in nodeList)
    //        {
    //            XmlNode id = item.SelectSingleNode("id");
    //            XmlNode name = item.SelectSingleNode("name");
    //            XmlNode des = item.SelectSingleNode("des");
    //            XmlNode starLv = item.SelectSingleNode("startLv");
    //            XmlNode spriteName = item.SelectSingleNode("spriteName");

    //            CardData m_data = new CardData();
    //            m_data.heroData.id = CommonHelper.Str2Int(id.InnerText);
    //            m_data.heroData.name = name.InnerText;
    //            m_data.heroData.starLv = CommonHelper.Str2Int(starLv.InnerText);
    //            m_data.heroData.spriteName = spriteName.InnerText;
    //            m_data.heroData.des = des.InnerText;
    //            m_data.isHas = true;
    //            m_data.num = 1;
    //            for (int i = 0; i < GroundCardList.Count; i++)
    //            {
    //                if (m_data.heroData.id == GroundCardList[i].heroData.id)
    //                {
    //                    if (GroundCardList[i].num >= 2) continue;
                                          
    //                    GroundCardList[i].num += m_data.num;      
    //                }
    //            }
    //        }


    //        for (int i = 0; i < GroundCardList.Count; i++)
    //        {
    //            if (GroundCardList[i].num >= 1)
    //            {
    //                GroundCardList[i].isHas = true;
    //            }
    //        }
    //    }
    //}

}
