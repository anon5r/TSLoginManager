using System;
using System.Collections.Generic;
using System.Text;

namespace TricksterTools
{
    /// <summary>
    /// バージョン管理用XML構造体
    /// </summary>
    namespace Versions
    {
        class XmlVersions
        {
        }


        [System.Xml.Serialization.XmlRoot("versions")]
        public class XmlVersionsRoot
        {
            [System.Xml.Serialization.XmlElement("status")]
            public string Status;
            [System.Xml.Serialization.XmlElement(ElementName = "information")]
            public XmlVersionInfo Information = new XmlVersionInfo();
        }


        /// <summary>
        /// バージョン情報クラス
        /// </summary>
        public class XmlVersionInfo
        {
            //[System.Xml.Serialization.XmlAttribute("guid")]
            //public string GUID;
            [System.Xml.Serialization.XmlElement("name")]
            public string Name;
            [System.Xml.Serialization.XmlElement("version")]
            public string Version;
            [System.Xml.Serialization.XmlElement("beta")]
            public bool isBeta;
            [System.Xml.Serialization.XmlElement("filename")]
            public string FileName;
            [System.Xml.Serialization.XmlElement("size")]
            public int FileSize;
            [System.Xml.Serialization.XmlElement("url")]
            public string Url;
            [System.Xml.Serialization.XmlElement("message")]
            public string Message;
        }
    }
}
