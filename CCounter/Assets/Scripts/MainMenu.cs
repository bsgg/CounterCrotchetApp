using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCounter
{
    public class MainMenu : UIBase
    {
        [SerializeField]   private Message m_ConfirmMessage;

        public override void Show()
        {
            base.Show();

            m_ConfirmMessage.Hide();
        }

        public override void Hide()
        {
            base.Hide();

            m_ConfirmMessage.Hide();
        }

        public void ShowConfirm(string text)
        {
            m_ConfirmMessage.MessageText = text;
            m_ConfirmMessage.Show();
        }


        public void OnConfirmOk()
        {
            m_ConfirmMessage.Hide();
        }

		
	}
}
