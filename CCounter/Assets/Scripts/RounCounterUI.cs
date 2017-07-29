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
       
        public override void Init()
        {
            base.Init();

            m_CurrentRound = new Round();
        }       

        public override void Hide()
        {            
            m_CounterRoundListScroll.OnButtonPress -= OnButtonMenuPress;
            base.Hide();
        }

        public void SetRound( Round round)
        {
            m_CurrentRound = round;
            m_CurrentCounter = 0;

            if (m_CurrentRound != null)
            {
                m_RoundDescription.text = "R" + m_CurrentRound.RoundNumber.ToString() + ":";

                int numberStiches = 0;
                for (int iStich = 0; iStich < m_CurrentRound.Stiches.Count; iStich++)
                {
                    m_RoundDescription.text += m_CurrentRound.Stiches[iStich].NumberRepeats.ToString() + " " + m_CurrentRound.Stiches[iStich].Abbr;

                    numberStiches += m_CurrentRound.Stiches[iStich].NumberRepeats;

                    if (iStich < m_CurrentRound.Stiches.Count - 1)
                    {
                        m_RoundDescription.text += " , ";
                    }
                }

                numberStiches *= m_CurrentRound.Repeats;

                if (m_CurrentRound.Repeats > 1)
                {
                    m_RoundDescription.text += "  - Repeat x " + m_CurrentRound.Repeats.ToString();
                }
                m_RoundDescription.text += " (" + numberStiches + " stiches)";

               // Add number stiches 

                List <string> listStiches = new List<string>();
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
            if (m_CurrentRound != null)
            {
                if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
                {
                    if (m_CurrentCounter < m_CounterRoundListScroll.NumberElements())
                    {
                        OnButtonMenuPress(m_CurrentCounter);
                        m_CurrentCounter++;
                    }
                    /*else
                    {
                        OnNextRound();
                    }*/
                }
            }
        }

        public void OnNextRound()
        {
            // Last  group, check round and get next one
            AppController.Instance.FinishCurrentRoundInList();

            Round round = AppController.Instance.GetNextRoundInList();

            if (round != null)
            {
                SetRound(round);
            }
            else
            {
                Debug.Log("[ROUNDCOUNTERUI] NO MORE ROUNDS");
            }
        }

    }
}
