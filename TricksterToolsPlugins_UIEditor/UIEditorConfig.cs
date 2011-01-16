using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using TricksterTools.Plugins.Config;

namespace TricksterTools.Plugins.UIEditor
{

    public class XmlPluginUIEditor : XmlPlugin
    {
        [XmlAttribute(AttributeName = "name")]
        public string name = "UIEditor";

        [XmlElement(ElementName = "Config")]
        public IPluginConfig Config;
    }

    public class UIEditorConfig : IPluginConfig
    {
        [XmlElement(ElementName = "UIConfig")]
        public UIConfig[] config;
    }
    
    public abstract class UIConfig
    {
        [XmlAttribute(AttributeName = "name")]
        public string name;
    }

    public class ChatUIConf : UIConfig
    {
        [XmlAttribute(AttributeName = "name")]
        public string name
        {
            get
            {
                return "ChatUI";
            }
        }

        [XmlElement(ElementName = "width")]
        public int width = 325;

        [XmlElement(ElementName = "height")]
        public int height = 478;
    }
}
