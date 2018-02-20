using ScrollList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class RoundCounterUI : UIBase
    {
        [SerializeField]
        private Text m_RoundDescription; 
        [SerializeField] private ScrollPanelUI m_CounterRoundListScroll;
        [SerializeField]
        private Toggle m_RoundCompletedToggle;

        private int m_CurrentCounter = 0;


        //private Round m_SelectedRound;
        /*public Round SelectedRound
        {
            set { m_SelectedRound = value; }
        }*/

        public override void Hide()
        {            
            m_CounterRoundListScroll.OnButtonPress -= OnButtonMenuPress;
            base.Hide();
        }

        public override void Show()
        {
            SetRound();

            base.Show();

        }

        public void SetRound()
        {
            m_CurrentCounter = 0;

            if (AppController.Instance.CurrentRound != null)
            {
                m_RoundDescription.text = "R" + AppController.Instance.CurrentRound.RoundNumber.ToString() + ":";

                if (AppController.Instance.CurrentRound.TypeRound == Round.ETYPEROUND.FRONTLOOPY)
                {
                    m_RoundDescription.text = "Round " + AppController.Instance.CurrentRound.RoundNumber.ToString() + " (FLO): ";
                }
                else if (AppController.Instance.CurrentRound.TypeRound == Round.ETYPEROUND.BACKLOOP)
                {
                    m_RoundDescription.text = "Round " + AppController.Instance.CurrentRound.RoundNumber.ToString() + " (BLO): ";
                }else
                {
                    m_RoundDescription.text = "Round " + AppController.Instance.CurrentRound.RoundNumber.ToString() + " : ";
                }

                if (AppController.Instance.CurrentRound.Stiches != null)
                {
                    string stiches = "";
                    for (int iStich = 0; iStich < AppController.Instance.CurrentRound.Stiches.Count; iStich++)
                    {
                        stiches += AppController.Instance.CurrentRound.Stiches[iStich].NumberRepeats + AppController.Instance.CurrentRound.Stiches[iStich].Name;
                        if (iStich < (AppController.Instance.CurrentRound.Stiches[iStich].NumberRepeats - 1))
                        {
                            stiches += " , ";
                        }
                    }

                    m_RoundDescription.text += stiches;


                    // Add all stiches
                    List<string> listStiches = new List<string>();
                    for (int iRepeat = 0; iRepeat < AppController.Instance.CurrentRound.Repeats; iRepeat++)
                    {
                        for (int iStich = 0; iStich < AppController.Instance.CurrentRound.Stiches.Count; iStich++)
                        {
                            string st = AppController.Instance.CurrentRound.Stiches[iStich].NumberRepeats + "  " + AppController.Instance.CurrentRound.Stiches[iStich].Name.ToUpper();
                            listStiches.Add(st);
                        }
                    }
                    m_CounterRoundListScroll.InitScroll(listStiches);
                    m_CounterRoundListScroll.OnButtonPress += OnButtonMenuPress;
                }               
                
            }
        }


        private void OnButtonMenuPress(int id)
        {
            if ((m_CounterRoundListScroll != null))
            {
                GameObject stichObj = m_CounterRoundListScroll.Get(id);
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

        /*void Update()
        {
            if (m_SelectedRound != null)
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
        }*/

        public void OnNextRound()
        {
            if (AppController.Instance.MarkRound(m_RoundCompletedToggle.isOn))
            {
                SetRound();
            }
            else
            {
                AppController.Instance.MessagePopup.ShowPopup("No more rounds", "There aren't any more rounds for this part.",
                    "Ok", OnOkPopupBtnPress, string.Empty, null, string.Empty, null);
            }
            
        }

        private void OnOkPopupBtnPress()
        {
            AppController.Instance.MessagePopup.Hide();
        }

    }
}
