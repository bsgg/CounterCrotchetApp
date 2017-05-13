using ScrollList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class RoundSelectorUI : UIBase
    {
        [SerializeField] private ScrollPanelUI m_RoundListScroll;

        public override void Show()
        {
            m_RoundListScroll.InitScroll(AppController.Instance.GetListRounds());
            m_RoundListScroll.OnButtonPress += OnButtonMenuPress;

            base.Show();
        }

        public override void Hide()
        {
            base.Hide();

            if (m_RoundListScroll != null)
            {
                m_RoundListScroll.OnButtonPress -= OnButtonMenuPress;
            }
        }


        private void OnButtonMenuPress(int id)
        {
            AppController.Instance.OnShowRoundCounter(id);
        }
    }
}
