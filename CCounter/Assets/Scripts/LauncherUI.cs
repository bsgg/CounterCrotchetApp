using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class LauncherUI : UIBase
    {
        [SerializeField] private Text m_Description;
        public string Description
        {
            get { return m_Description.text; }
            set { m_Description.text = value; }
        }

        [SerializeField] private Button m_OKButton;

        public Button OKButton
        {
            get { return m_OKButton; }
            set { m_OKButton = value; }
        }

        

        public override void Show()
        {
            base.Show();

            m_Description.text = string.Empty;
            m_OKButton.interactable = false;


        }

    }
}
