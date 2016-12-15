using System;
using System.IO;
using System.Xml;
using UnityEngine;


class ReadWriteXmlFile 
{
    public static int Speed;
    public static int Hard;
    public static int Mode;

    public static void LoadXml()
    {
            //创建xml文档
        XmlDocument xml = new XmlDocument();
        XmlReaderSettings set = new XmlReaderSettings();
        set.IgnoreComments = true;//这个设置是忽略xml注释文档的影响。有时候注释会影响到xml的读取
        xml.Load(XmlReader.Create((Application.dataPath +  "/dataFile.xml"), set));
        //得到objects节点下的所有子节点
        XmlNodeList xmlNodeList = xml.SelectSingleNode("objects").ChildNodes;
        //遍历所有子节点
        foreach (XmlElement xl in xmlNodeList){
            if (xl.GetAttribute("name") == "speed")
            {
                Speed = int.Parse(xl.InnerText);
            }
            else if (xl.GetAttribute("name") == "hard")
            {
                Hard = int.Parse(xl.InnerText);
            }
            else if (xl.GetAttribute("name") == "mode") {
                Mode = int.Parse(xl.InnerText);
            }
        }
    }

    public static void updateXML(int SPEED, int HARD, int MODE)
    {
        string path = Application.dataPath + "/dataFile.xml";
        if (File.Exists(path))
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);
            XmlNodeList xmlNodeList = xml.SelectSingleNode("objects").ChildNodes;
            foreach (XmlElement xl in xmlNodeList)
            {
                if (xl.GetAttribute("name") == "speed")
                {
                    xl.InnerText = Convert.ToString(SPEED);
                }
                else if (xl.GetAttribute("name") == "hard")
                {
                    xl.InnerText = Convert.ToString(HARD);
                }
                else if (xl.GetAttribute("name") == "mode") {
                    xl.InnerText = Convert.ToString(MODE);
                }
            }
            xml.Save(path);
        }
    }

}
