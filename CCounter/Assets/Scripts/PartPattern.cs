using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class PartPattern : UIBase
    {

        [SerializeField] private RoundUI m_Round;
        //[SerializeField] private MessagePopup m_MessagePopup;




        //[SerializeField] private PartPatternSettings m_PartSettings;
        //[SerializeField] private SaveRoundSettings m_SaveRoundSettings;

        private List<Stich> m_ListStiches;
        private Round m_CurrentRound;


        public void CreateNewRound(string name, int roundNumber)
        {
            m_CurrentRound = new Round();
            m_CurrentRound.PartName = name;
            m_CurrentRound.RoundNumber = roundNumber;
        }

        public override void Show()
        {
            base.Show();

           
            m_ListStiches = new List<Stich>();
           

            m_Round.Show();
           // m_PartSettings.Show();

            //m_SaveRoundSettings.Hide();
            //m_MessagePopup.Hide();
        }

        /*public void OnAcceptPartSettings()
        {
           // m_PartSettings.Hide();

            // Create round
            
            m_CurrentRound.PartName = m_Round..PartName;
            m_CurrentRound.RoundNumber = m_PartSettings.PartStartIndex;


        }*/

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

        private void OnOkBtn()
        {
           // m_MessagePopup.Hide();
        }

        public void OnSaveRound()
        {
            if (m_ListStiches.Count > 0)
            {
                m_CurrentRound.Repeats = m_Round.RoundRepeat;
                
                m_CurrentRound.TotalNumberStiches = 0;

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
                        m_CurrentRound.TotalNumberStiches += stich.NumberRepeats;

                        // Find incremental 
                        string auxS = stich.Name.TrimStart().TrimEnd().ToLower();
                        if (auxS.Contains("inc") || auxS.Contains("incremental"))
                        {
                            // TODO STICH NUMBER X 2
                            Debug.Log("Number Stiches x2");
                            m_CurrentRound.TotalNumberStiches += (stich.NumberRepeats * 2);
                        }
                    }
                }

                // Save current round in JSON and create new round 
                int numberRounds = AppController.Instance.SaveRound(m_CurrentRound);

               /* m_MessagePopup.ShowPopup(
                  "Message",
                  "Round: " + numberRounds + " Stiches: " + m_CurrentRound.TotalNumberStiches,
                  "Ok", OnOkBtn,
                  string.Empty, null, string.Empty, null);*/

                // Update Round
                int indexRound =  m_CurrentRound.RoundNumber + 1;
                m_CurrentRound = new Round();
                //m_CurrentRound.PartName = m_PartSettings.PartName;
                m_CurrentRound.RoundNumber = indexRound;

                //m_Title.text = m_CurrentRound.PartName + "  - Round: " + m_CurrentRound.RoundNumber;

                m_ListStiches = new List<Stich>();
                m_Round.Reset();
            }
            else
            {
                /*m_MessagePopup.ShowPopup(
                    "Warning",
                    "There are no stiches in this round",
                    "Ok", OnOkBtn,
                    string.Empty, null, string.Empty, null);*/
            }
        }
       
	}
}
