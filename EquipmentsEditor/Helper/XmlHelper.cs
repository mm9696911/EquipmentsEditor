using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace EquipmentsEditor.Helper
{
    public class XmlHelper
    {
        string filePath = System.IO.Directory.GetCurrentDirectory() + "\\Setting.xml";

        #region XML操作
        /// <summary>  
        /// 获取值  
        /// </summary>  
        /// <param name="strKey"></param>  
        /// <returns></returns>  
        public string GetXmlValue(string key)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string strResult = ""; //找不到匹配的项;  
            if (File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
                for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
                {
                    if (xmlDoc.DocumentElement.ChildNodes[i].Name == key)
                    {
                        strResult = xmlDoc.DocumentElement.ChildNodes[i].InnerText;
                        break;
                    }
                }
            }
            return strResult;
        }
        /// <summary>  
        /// 获取某节点下的全部值  
        /// </summary>  
        /// <param name="strKey"></param>  
        /// <returns></returns>  
        public List<string> GetXmlChildValue(string key)
        {
            List<string> list = new List<string>();
            XmlDocument xmlDoc = new XmlDocument();
            //string strResult = ""; //找不到匹配的项;  
            if (File.Exists(filePath))
            {
                xmlDoc.Load(filePath);
                for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
                {
                    if (xmlDoc.DocumentElement.ChildNodes[i].Name == key)
                    {
                        XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes[i].ChildNodes;
                        foreach (XmlNode node in nodeList)
                        {
                            list.Add(node.InnerText);
                        }
                        break;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 保存XML
        /// </summary>
        /// <param name="key">节点名称</param>
        /// <param name="value">节点值</param>
        /// <returns></returns>
        public bool SaveToXML(string key, string value)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlText xmltext;
            try
            {
                if (!File.Exists(filePath))
                {
                    //声明
                    XmlNode xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                    xmlnode.InnerText += " encoding=\"GB2312\"";
                    xmldoc.AppendChild(xmlnode);

                    //添加根节点
                    XmlElement xmlelementroot = xmldoc.CreateElement("", "root", "");
                    xmldoc.AppendChild(xmlelementroot);

                    //添加一个元素
                    XmlElement xmlelement1 = xmldoc.CreateElement("", key, "");
                    xmltext = xmldoc.CreateTextNode(value);
                    xmlelement1.AppendChild(xmltext);
                    xmldoc.ChildNodes.Item(1).AppendChild(xmlelement1);

                    //保存
                    xmldoc.Save(filePath);
                }
                else
                {
                    xmldoc.Load(filePath);
                    XmlNode root = xmldoc.DocumentElement;
                    ((XmlElement)root.SelectSingleNode("//" + key)).InnerText = value;
                    //保存
                    xmldoc.Save(filePath);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
