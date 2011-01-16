using System;
using System.Collections.Generic;
using System.Text;

namespace TricksterTools
{
    /// <summary>
    /// トリックスター サードパーティツール向けの共通仕様XML構造クラス
    /// </summary>
    namespace CommonXmlStructure
    {
        /// <summary>
        /// Tricksterクラス
        /// </summary>
        [System.Xml.Serialization.XmlRoot("Trickster")]
        public class XmlTricksterRoot
        {
            [System.Xml.Serialization.XmlElement(ElementName = "MyAccounts")]
            public XmlMyAccounts MyAccount;
        }

        /// <summary>
        /// アカウント情報を包括して保持しておくクラス
        /// </summary>
        public class XmlMyAccounts
        {
            // 管理アカウント一覧
            [System.Xml.Serialization.XmlArrayItem(ElementName = "Account")]
            public XmlAccount[] AccountList;
            // アカウントパスワードの暗号化を行うためのキー
            [System.Xml.Serialization.XmlAttributeAttribute("EncryptKey")]
            public string EncryptKey;
        }

        /// <summary>
        /// アカウント情報を保持しておくクラス
        /// </summary>
        public class XmlAccount
        {
            [System.Xml.Serialization.XmlAttribute("ID")]
            public string ID;
            [System.Xml.Serialization.XmlAttribute("Password")]
            public string Password;
            [System.Xml.Serialization.XmlAttribute("Site")]
            public int Site;
        }
    }
}
