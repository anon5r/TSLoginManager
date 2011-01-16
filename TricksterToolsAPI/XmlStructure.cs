using System;
using System.Collections.Generic;
using System.Text;

namespace TricksterTools
{
    /// <summary>
    /// �g���b�N�X�^�[ �T�[�h�p�[�e�B�c�[�������̋��ʎd�lXML�\���N���X
    /// </summary>
    namespace CommonXmlStructure
    {
        /// <summary>
        /// Trickster�N���X
        /// </summary>
        [System.Xml.Serialization.XmlRoot("Trickster")]
        public class XmlTricksterRoot
        {
            [System.Xml.Serialization.XmlElement(ElementName = "MyAccounts")]
            public XmlMyAccounts MyAccount;
        }

        /// <summary>
        /// �A�J�E���g�������ĕێ����Ă����N���X
        /// </summary>
        public class XmlMyAccounts
        {
            // �Ǘ��A�J�E���g�ꗗ
            [System.Xml.Serialization.XmlArrayItem(ElementName = "Account")]
            public XmlAccount[] AccountList;
            // �A�J�E���g�p�X���[�h�̈Í������s�����߂̃L�[
            [System.Xml.Serialization.XmlAttributeAttribute("EncryptKey")]
            public string EncryptKey;
        }

        /// <summary>
        /// �A�J�E���g����ێ����Ă����N���X
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
