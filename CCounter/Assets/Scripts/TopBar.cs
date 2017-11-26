using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class TopBar : UIBase
    {
        [SerializeField]
        private Button m_BackBtn;
        public Button CurrentRound
        {
            get { return m_BackBtn; }
        }

        [SerializeField]
        private Text m_Title;
        public string Title
        {
            get { return m_Title.text; }
            set { m_Title.text = value; }
        }

    }
}
