using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TricksterTools
{
    namespace Library
    {
        namespace Xml
        {
            namespace Settings
            {

                [XmlRoot("Trickster")]
                public class XmlTricksterRoot
                {
                    [XmlElement(ElementName = "Tools")]
                    public XmlTools Tools;
                }

                public class XmlTools
                {
                    [XmlAttribute(AttributeName = "name")]
                    public string name = "TSLoginManager";

                    [XmlElement(ElementName = "Settings")]
                    public XmlSettings Settings;
                }


                public class XmlSettings
                {
                    [XmlElement(ElementName = "HungupTime")]
                    public XmlSettingHungUpTime HungupTime;

                    [XmlElement(ElementName = "UpdateCheck")]
                    public XmlSettingUpdate UpdateCheck;

                    [XmlElement(ElementName = "GameStartUp")]
                    public XmlSettingGameStartUp StartUpGame;

                    [XmlElement(ElementName = "Logging")]
                    public XmlSettingLogging Logging;

                    [XmlElement(ElementName = "Icon")]
                    public XmlSettingIcons Icon;
                }

                public class XmlSettingHungUpTime
                {
                    [XmlAttribute(AttributeName = "enable")]
                    public string enable = "true";

                    [XmlAttribute(AttributeName = "sec")]
                    public int sec = 1;
                }

                public class XmlSettingUpdate
                {
                    [XmlAttribute(AttributeName = "startup")]
                    public string startup = "false";

                    [XmlAttribute(AttributeName = "beta")]
                    public string checkBeta = "false";
                }

                public class XmlSettingGameStartUp
                {
                    [XmlAttribute(AttributeName = "mode")]
                    public int mode = 0;
                }

                public class XmlSettingLogging
                {
                    [XmlAttribute(AttributeName = "enable")]
                    public string enable = "false";

                    [XmlAttribute(AttributeName = "path")]
                    public string Path = Environment.CurrentDirectory + @"\logs";
                }

                public class XmlSettingIcons
                {
                    [XmlAttribute(AttributeName = "resource")]
                    public string resourceName = "char99";
                }
            }
        }
    }
}
