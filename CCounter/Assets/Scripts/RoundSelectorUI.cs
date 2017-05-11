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

        public ScrollPanelUI ScrollMenu
        {
            get { return m_RoundListScroll; }
            set { m_RoundListScroll = value; }
        }

        private List<Round> m_RoundList;

        public override void Show()
        {
            List<string> listFiles = CCFileUtil.ListJSONFiles();

            m_RoundList = new List<Round>();
            List<string> listRounds = new List<string>();

            for (int i = 0; i < listFiles.Count; i++)
            {
                Round round = new Round();
                if (CCFileUtil.LoadRoundJSON(listFiles[i], out round))
                {
                    m_RoundList.Add(round);

                    string titleRound = "R" + round.RoundNumber + ": ";
                    for (int iStich = 0; iStich<round.Stiches.Count; iStich++)
                    {
                        titleRound += round.Stiches[iStich].NumberRepeats.ToString() + " " + round.Stiches[iStich].Abbr;

                        if (iStich < round.Stiches.Count -1 )
                        {
                            titleRound += " , ";
                        }
                    }

                    if (round.RepeatsPerGroupStiches > 1)
                    {
                        titleRound += "  - Repeat x " + round.RepeatsPerGroupStiches.ToString();
                    }

                    listRounds.Add(titleRound);
                }               
            }

            m_RoundListScroll.InitScroll(listRounds);
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
            if ((m_RoundList != null) && (id < m_RoundList.Count))
            {
                Debug.Log("ROUND...." + id);

                AppController.Instance.OnShowRoundCounter(m_RoundList[id]);

            }
        }

    }
}
