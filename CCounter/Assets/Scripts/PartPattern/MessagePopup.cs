using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

namespace CCounter
{
    public class MessagePopup : UIBase
    {
        /*public delegate void MessageAction();
        public event MessageAction OnMiddleBtn;
        public event MessageAction OnLeftBtn;
        public event MessageAction OnRightBtn;*/


        [SerializeField]
        private Text m_TitleText;

        [SerializeField]
        private Text m_MessageText;

        //[SerializeField] private 
        [SerializeField]
        private ButtonText m_MiddleBtn;
        [SerializeField]
        private ButtonText m_LeftBtn;
        [SerializeField]
        private ButtonText m_RightBtn;


        public void ShowPopup(string title, string message,
            string middleBtn, UnityAction middleBtnCallback, 
            string leftBtn, UnityAction leftBtnCallback,
            string rightBtn, UnityAction rightBtnCallback)
        {
            m_TitleText.text = title;
            m_MessageText.text = message;

            if (middleBtnCallback != null)
            {
                m_MiddleBtn.Show(middleBtn, middleBtnCallback);
            }
            else
            {
                m_MiddleBtn.Hide();
            }

            if (leftBtnCallback != null)
            {
                m_LeftBtn.Show(leftBtn, leftBtnCallback);
            }
            else
            {
                m_LeftBtn.Hide();
            }

            if (rightBtnCallback != null)
            {
                m_RightBtn.Show(rightBtn, rightBtnCallback);
            }
            else
            {
                m_RightBtn.Hide();
            }
            Show();
        }
    }
}
