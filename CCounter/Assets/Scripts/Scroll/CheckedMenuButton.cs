using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScrollList
{
    public class CheckedMenuButton : MenuButton
    {
        [SerializeField]
        private Color m_NoCheckedColor;
        [SerializeField]
        private Color m_CheckedColor;

        [SerializeField]
        private GameObject m_LineCheck;

        private void Start()
        {
            //UnCheck();
        }

        public void Check()
        {
            m_LabelTitleButton.color = m_CheckedColor;
            m_LineCheck.SetActive(true);
        }

        public void UnCheck()
        {
            m_LabelTitleButton.color = m_NoCheckedColor;
            m_LineCheck.SetActive(false);
        }

    }
}
