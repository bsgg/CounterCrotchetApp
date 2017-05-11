using ScrollList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class RounCounterUI : UIBase
    {
        [SerializeField]
        private Text m_RoundDescription; 
        [SerializeField] private ScrollPanelUI m_CounterRoundListScroll;

        private int m_CurrentCounter = 0;


        private Round m_CurrentRound;
        public Round CurrentRound
        {
            get { return m_CurrentRound; }
            set { m_CurrentRound = value; }
        }

        public override void Show()
        {
            base.Show();

            m_CurrentCounter = 0;
            if (m_CurrentRound != null)
            {
                m_RoundDescription.text = "R" + m_CurrentRound.RoundNumber.ToString() + ":";

                for (int iStich = 0; iStich < m_CurrentRound.Stiches.Count; iStich++)
                {
                    m_RoundDescription.text += m_CurrentRound.Stiches[iStich].NumberRepeats.ToString() + " " + m_CurrentRound.Stiches[iStich].Abbr;

                    if (iStich < m_CurrentRound.Stiches.Count - 1)
                    {
                        m_RoundDescription.text += " , ";
                    }
                }

                if (m_CurrentRound.RepeatsPerGroupStiches > 1)
                {
                    m_RoundDescription.text += "  - Repeat x " + m_CurrentRound.RepeatsPerGroupStiches.ToString();
                }

                List<string> listStiches = new List<string>();
                for (int iStich = 0; iStich < m_CurrentRound.AllRepeatsStiches.Count; iStich++)
                {
                    string stich = m_CurrentRound.AllRepeatsStiches[iStich].NumberRepeats + " " + m_CurrentRound.AllRepeatsStiches[iStich].Abbr.ToString();
                    listStiches.Add(stich);
                }

                m_CounterRoundListScroll.InitScroll(listStiches);
                m_CounterRoundListScroll.OnButtonPress += OnButtonMenuPress;

            }

        }

        private void OnButtonMenuPress(int id)
        {
            if ((m_CounterRoundListScroll != null))
            {
                GameObject stichObj = m_CounterRoundListScroll.GetElement(id);
                if (stichObj != null)
                {
                    CheckedMenuButton chk = stichObj.GetComponent<CheckedMenuButton>();
                    if (chk != null)
                    {
                        chk.Check();
                    }
                }
            }
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
            {
                if (m_CurrentCounter < m_CounterRoundListScroll.NumberElements())
                {
                    OnButtonMenuPress(m_CurrentCounter);
                    m_CurrentCounter++;
                }
            }
        }

    }
}
