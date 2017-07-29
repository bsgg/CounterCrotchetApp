using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class PatternSettingsUI : UIBase
    {
        [SerializeField]
        private Text m_Title;

        [SerializeField] private RoundSettingsUI m_RoundSettingsUI;
        [SerializeField] private PartPatternSettings m_PartSettings;
        [SerializeField] private SaveRoundSettings m_SaveRoundSettings;


        private Round m_CurrentRound;

        public override void Show()
        {
            base.Show();
            m_RoundSettingsUI.Init();

            m_PartSettings.Show();
            m_SaveRoundSettings.Hide();
        }


        public void OnAcceptPartSettings()
        {
            m_PartSettings.Hide();

            // Create round
            m_CurrentRound = new Round();
            m_CurrentRound.PartName = m_PartSettings.PartName;
            m_CurrentRound.RoundNumber = m_PartSettings.PartStartIndex;

            m_Title.text = m_CurrentRound.PartName + " Round: " + m_CurrentRound.RoundNumber;
            
        }

       // private Round m_CurrentRound;
       // private int m_CurrentRoundNumber = 1;

       /* public int RepeatsPerGroupStiches
        {
            get
            {
                int nRepeatsPerGroup = 1;
                int auxNRepeats = 0;
                if (int.TryParse(m_RepeatsPerGroupStiches.text, out auxNRepeats))
                {
                    nRepeatsPerGroup = auxNRepeats;
                }

                return nRepeatsPerGroup;
            }
            set
            {
                if (value <= 0)
                {
                    m_RepeatsPerGroupStiches.text = "1";
                }
                else
                {
                    m_RepeatsPerGroupStiches.text = value.ToString();
                }
            }
        }*/
        

        public void OnAddStich()
        {
            m_RoundSettingsUI.AddStich();




           /* m_RoundSettingsUI.AddStich();
           // int roundNumber = int.Parse(m_RoundNumber.text);
           // m_CurrentTextRound.text = RepeatsPerGroupStiches.ToString() + " Repeat(s) For: \n" + m_RoundSettingsUI.PrintStiches();*/
        }

        public void OnAddSpecialStich()
        {
            m_RoundSettingsUI.AddSpecialStich();

            /* m_RoundSettingsUI.AddSpecialStich();
             int roundNumber = int.Parse(m_RoundNumber.text);
             m_CurrentTextRound.text = RepeatsPerGroupStiches.ToString() + " Repeat(s) For: \n"  + m_RoundSettingsUI.PrintStiches();*/
        }

        public void OnSaveRound()
        {
            if (m_RoundSettingsUI.ListStiches.Count > 0)
            {
                m_SaveRoundSettings.Show();

            }else
            {
                m_RoundSettingsUI.CurrentRound = "Add some stiches, It is not possible to generate JSON data";
            }

            


            /*int nRepeatsPerGroup = RepeatsPerGroupStiches;

            int auxRoundN = 0;
            if (int.TryParse(m_RoundNumber.text, out auxRoundN))
            {
                m_CurrentRoundNumber = auxRoundN;
            }

            m_CurrentRound = m_RoundSettingsUI.CreateRound(nRepeatsPerGroup, m_CurrentRoundNumber);*/
            /*if (!string.IsNullOrEmpty(m_PatternName.text))
            {
                m_CurrentRound.NamePattern = m_PatternName.text;
            }
            else
            {
                m_CurrentRound.NamePattern = "Pattern";
            }
            */
            /* if (m_CurrentRound.Stiches.Count > 0)
             { 

                 // Add round to  list of rounds
                 AppController.Instance.AddRound(m_CurrentRound);

                 //m_CurrentRound.Clear();
                 m_CurrentTextRound.text = "";
                 //RepeatsPerGroupStiches = 1;

                 m_CurrentRoundNumber += 1;
                 m_RoundNumber.text = m_CurrentRoundNumber.ToString();

             }else
             {
                 m_CurrentTextRound.text = "Add some stiches, It was not possible to generate JSON data";
             }*/
        }

        public void OnDeleteRound()
        {
            m_RoundSettingsUI.RemoveStiches();
        }


        /// <summary>
        /// Finish saving the current round
        /// </summary>
        public void OnAcceptRound()
        {
            m_CurrentRound.TypeRound = m_SaveRoundSettings.TypeRound;
            m_CurrentRound.Repeats = m_SaveRoundSettings.RepeatsRound;

            m_CurrentRound.Stiches = new List<Stich>();
            m_CurrentRound.AllRepeatsStiches = new List<Stich>();

            if (m_RoundSettingsUI.ListStiches != null)
            {
                // Add all stiches
                for (int i = 0; i < m_RoundSettingsUI.ListStiches.Count; i++)
                {
                     m_CurrentRound.Stiches.Add(m_RoundSettingsUI.ListStiches[i]);
                }

                // Generate all repeats for this round and count total number stiches
                m_CurrentRound.TotalNumberStiches = 0;
                bool foundSpecialStich = false;
                for (int iRepeat = 0; iRepeat < m_CurrentRound.Repeats; iRepeat++)
                {
                    for (int iStich = 0; iStich < m_RoundSettingsUI.ListStiches.Count; iStich++)
                    {
                        Stich stich = m_RoundSettingsUI.ListStiches[iStich];

                        // Check if it's a special stich and not cound as a stich
                        if (stich.SpecialStich && !stich.CountAsStich)
                        {
                            if (stich.CountAsStich)
                            {
                                m_CurrentRound.AllRepeatsStiches.Add(stich);
                                m_CurrentRound.TotalNumberStiches++;

                            }
                            else if (!foundSpecialStich)
                            {
                                // Add only the first time if it's not a stich
                                m_CurrentRound.AllRepeatsStiches.Add(stich);
                                foundSpecialStich = true;
                            }
                        }
                        else
                        {
                            m_CurrentRound.TotalNumberStiches++;
                            m_CurrentRound.AllRepeatsStiches.Add(stich);
                        }

                    }

                }
            }



        }

        public void OnSavePart()
        {

        }
	}
}
