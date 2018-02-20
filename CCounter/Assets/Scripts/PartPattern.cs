using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class PartPattern : UIBase
    {

        [SerializeField] private RoundUI m_Round;

        private string m_PartName;
        private int m_RoundNumber;

        private List<Stich> m_ListStiches;
        private Round m_CurrentRound;


        public void CreateNewRound(string name, int roundNumber)
        {
            m_PartName = name;
            m_RoundNumber = roundNumber;

            m_CurrentRound = new Round();            
            m_CurrentRound.PartName = m_PartName;
            m_CurrentRound.RoundNumber = m_RoundNumber;
        }

        public override void Show()
        {
            base.Show();
           
            m_ListStiches = new List<Stich>();          

            m_Round.Show();

            AppController.Instance.TopBar.Title = m_CurrentRound.PartName + "  - Round: " + m_CurrentRound.RoundNumber;
        }
        

        public void AddStich()
        {
            if (!string.IsNullOrEmpty(m_Round.Stich))
            {
                Stich stich = new Stich();
                stich.Name = m_Round.Stich;

                stich.NumberRepeats = m_Round.StichRepeats;

                m_ListStiches.Add(stich);

                m_Round.CurrentRound = PrintStiches();
            }
            else
            {
                /*m_MessagePopup.ShowPopup(
                   "Warning",
                   "Add at least 1 stich",
                   "Ok", OnOkBtn,
                   string.Empty, null, string.Empty, null);*/
            }
        }

        public void UndoStich()
        {
            if (m_ListStiches.Count > 0)
            {
                m_ListStiches.RemoveAt(m_ListStiches.Count - 1);
                m_Round.CurrentRound = PrintStiches();
            }
            else
            {
                m_Round.CurrentRound = string.Empty;
            }
        }

        private string PrintStiches()
        {
            string stiches = string.Empty;
            if ((m_ListStiches != null) && (m_ListStiches.Count > 0))
            {
                for (int i = 0; i < m_ListStiches.Count; i++)
                {
                    // Check special stich
                    string auxS = string.Empty;
                    if (m_ListStiches[i].NumberRepeats > 0)
                    {
                        auxS = " - " + m_ListStiches[i].NumberRepeats + " " + m_ListStiches[i].Name;
                    }
                    else
                    {
                        auxS = " - " + m_ListStiches[i].Name;
                    }

                    stiches += auxS;

                    if (i < (m_ListStiches.Count - 1))
                    {
                        stiches += "\n";
                    }
                }
            }

            return stiches;
        }

        public void OnSaveRound()
        {
            if (m_ListStiches.Count > 0)
            {
                m_CurrentRound.Repeats = m_Round.RoundRepeat;
                
                m_CurrentRound.StichCount = 0;

                m_CurrentRound.Stiches = new List<Stich>();
                m_CurrentRound.AllRepeatsStiches = new List<Stich>();

                for (int i = 0; i < m_ListStiches.Count; i++)
                {
                    Stich stich = m_ListStiches[i];
                    m_CurrentRound.Stiches.Add(stich);
                }

                for (int iRepeat = 0; iRepeat < m_CurrentRound.Repeats; iRepeat++)
                {
                    for (int iStich = 0; iStich < m_ListStiches.Count; iStich++)
                    {
                        Stich stich = m_ListStiches[iStich];

                        m_CurrentRound.AllRepeatsStiches.Add(stich);
                        m_CurrentRound.StichCount += stich.NumberRepeats;

                        // Find incremental 
                        string auxS = stich.Name.TrimStart().TrimEnd().ToLower();
                        if (auxS.Contains("inc") || auxS.Contains("incremental"))
                        {
                            Debug.Log("Number Stiches x2");
                            m_CurrentRound.StichCount += (stich.NumberRepeats * 2);
                        }
                    }
                }

                // Save current round in JSON and create new round 
                int numberRounds = AppController.Instance.SaveRound(m_CurrentRound);

                string message = "Round " + m_RoundNumber + " - " + m_CurrentRound.StichCount + " stich(es)";
                AppController.Instance.MessagePopup.ShowPopup("New round added", message,
                    "Ok", OnOkPopupBtnPress, string.Empty, null, string.Empty, null);


                // Reset all 
                m_RoundNumber += 1;
                m_CurrentRound = new Round();
                m_CurrentRound.PartName = m_PartName;
                m_CurrentRound.RoundNumber = m_RoundNumber; 

                m_ListStiches = new List<Stich>();
                m_Round.Reset();

                AppController.Instance.TopBar.Title = m_PartName + "  - Round: " + m_CurrentRound.RoundNumber;

            }
            else
            {
                AppController.Instance.MessagePopup.ShowPopup("No stiches",
                    "Include some stiches for the round",
                    "Ok", OnOkPopupBtnPress, 
                    string.Empty, null, string.Empty, null);
            }
        }

        private void OnOkPopupBtnPress()
        {
            AppController.Instance.MessagePopup.Hide();
        }

    }
}
