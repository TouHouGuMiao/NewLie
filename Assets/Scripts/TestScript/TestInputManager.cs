using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class TestInputManager
{
    private static TestInputManager _Instance=null;
    public  static TestInputManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new TestInputManager();
            }
            return _Instance;
        }
    }


    public void CreatOrWriteConfig(string text)
    {
        string filePath = Application.dataPath + @"/Resources/InputConfig.xml";
        if (!File.Exists(filePath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("root");
            XmlElement node = xmlDoc.CreateElement("Input");
            node.InnerText = text;
            root.AppendChild(node);
            xmlDoc.AppendChild(root);
            xmlDoc.Save(filePath);
        }
        else
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            XmlNode root = xmlDoc.SelectSingleNode("root");
            XmlElement node = xmlDoc.CreateElement("Input");
            node.InnerText = text;
            root.AppendChild(node);
            xmlDoc.Save(filePath);
        }
    }
}
