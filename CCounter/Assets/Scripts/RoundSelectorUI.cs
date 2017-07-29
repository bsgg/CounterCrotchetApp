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

            // Check rounds which has been ticked off yet
            for (int i= 0; i< AppController.Instance.NumberRounds; i++ )
            {
                Round rnd = AppController.Instance.GetRoundById(i);
                if (rnd != null /*&& rnd.IsTickedOff*/)
                {
                    GameObject stichObj = m_RoundListScroll.GetElement(i);
                    if (stichObj != null)
                    {
                        CheckedMenuButton chk = stichObj.GetComponent<CheckedMenuButton>();
                        if (chk != null)
                        {
                            /*if (rnd.IsTickedOff)
                            {
                                chk.Check();
                            }else
                            {
                                chk.UnCheck();
                            }*/                            
                        }
                    }
                }
            }

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
