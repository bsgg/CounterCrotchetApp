using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class Message : UIBase
    {
        public delegate void MessageAction();
        public event MessageAction OnOK;


        [SerializeField]
        private Text m_TitleText;

        public string TitleText
        {
            get { return m_TitleText.text; }
            set { m_TitleText.text = value; }
        }


        [SerializeField] private Text m_MessageText;

        public string MessageText
        {
            get { return m_MessageText.text  ; }
            set { m_MessageText.text = value; }
        }

        public void OnOkPress()
        {
            if (OnOK != null)
            {
                OnOK();
            }

        }
    }
}
