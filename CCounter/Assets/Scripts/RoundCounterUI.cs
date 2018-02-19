﻿using ScrollList;
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
        private Round m_SelectedRound;
        public Round SelectedRound
        {
            set { m_SelectedRound = value; }
        }



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

            if (m_SelectedRound != null)
            {               

                m_RoundDescription.text = "R" + m_SelectedRound.RoundNumber.ToString() + ":";

                if (m_SelectedRound.TypeRound == Round.ETYPEROUND.FRONTLOOPY)
                {
                    m_RoundDescription.text = "Round " + m_SelectedRound.RoundNumber.ToString() + " (FLO): ";
                }
                else if (m_SelectedRound.TypeRound == Round.ETYPEROUND.BACKLOOP)
                {
                    m_RoundDescription.text = "Round " + m_SelectedRound.RoundNumber.ToString() + " (BLO): ";
                }else
                {
                    m_RoundDescription.text = "Round " + m_SelectedRound.RoundNumber.ToString() + " : ";
                }

                if (m_SelectedRound.Stiches != null)
                {
                    string stiches = "";
                    for (int iStich = 0; iStich < m_SelectedRound.Stiches.Count; iStich++)
                    {
                        stiches += m_SelectedRound.Stiches[iStich].NumberRepeats + m_SelectedRound.Stiches[iStich].Name;
                        if (iStich < (m_SelectedRound.Stiches[iStich].NumberRepeats - 1))
                        {
                            stiches += " , ";
                        }
                    }

                    m_RoundDescription.text += stiches;


                    // Add all stiches
                    List<string> listStiches = new List<string>();
                    for (int iRepeat = 0; iRepeat < m_SelectedRound.Repeats; iRepeat++)
                    {
                        for (int iStich = 0; iStich < m_SelectedRound.Stiches.Count; iStich++)
                        {
                            string st = m_SelectedRound.Stiches[iStich].NumberRepeats + "  " + m_SelectedRound.Stiches[iStich].Name.ToUpper();
                            listStiches.Add(st);
                        }
                    }
                    m_CounterRoundListScroll.InitScroll(listStiches);
                    m_CounterRoundListScroll.OnButtonPress += OnButtonMenuPress;
                }



                //int numberStiches = 0;



                /*for (int iStich = 0; iStich < m_SelectedRound.Stiches.Count; iStich++)
                {






                    /*if (m_CurrentRound.Stiches[iStich].SpecialStich && !m_CurrentRound.Stiches[iStich].CountAsStich)
                    {
                        m_RoundDescription.text += " " + m_CurrentRound.Stiches[iStich].Name;
                    }else
                    {
                        m_RoundDescription.text += m_CurrentRound.Stiches[iStich].NumberRepeats.ToString() + " " + m_CurrentRound.Stiches[iStich].Abbr;
                    }*/

                  /*  if (iStich < m_SelectedRound.Stiches.Count - 1)
                    {
                        m_RoundDescription.text += " , ";
                    }*/
                /*}

                if (m_SelectedRound.Repeats > 1)
                {
                    m_RoundDescription.text += "  - Repeat x " + m_SelectedRound.Repeats.ToString();
                }
                m_RoundDescription.text += " (" + m_SelectedRound.StichCount + " stiches)";*/

               // Add number stiches 
                /*List <string> listStiches = new List<string>();
                for (int iStich = 0; iStich < m_SelectedRound.AllRepeatsStiches.Count; iStich++)
                {
                    string stich = "";
                    /*if (m_CurrentRound.AllRepeatsStiches[iStich].SpecialStich && !m_CurrentRound.AllRepeatsStiches[iStich].CountAsStich)
                    {
                        stich = " " + m_CurrentRound.AllRepeatsStiches[iStich].Name;
                    }
                    else
                    {
                        stich = m_CurrentRound.AllRepeatsStiches[iStich].NumberRepeats + " " + m_CurrentRound.AllRepeatsStiches[iStich].Abbr.ToString();
                    }*/
                   /* listStiches.Add(stich);
                }*/

                
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
            if (m_SelectedRound != null)
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
           /* AppController.Instance.FinishCurrentRoundInList(m_RoundCompletedToggle.isOn);

            Round round = AppController.Instance.GetNextRoundInList();

            if (round != null)
            {
                SetRound(round);
            }
            else
            {
                Debug.Log("[ROUNDCOUNTERUI] NO MORE ROUNDS");
            }*/
        }

    }
}
