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
        [SerializeField] private Message m_Message;


        private Round m_CurrentRound;

        public override void Show()
        {
            base.Show();
            m_RoundSettingsUI.Init();

            m_PartSettings.Show();
            m_SaveRoundSettings.Hide();
            m_Message.Hide();
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
        

        public void OnAddStich()
        {
            m_RoundSettingsUI.AddStich();
        }

        public void OnAddSpecialStich()
        {
            m_RoundSettingsUI.AddSpecialStich();
        }

        public void OnSaveRound()
        {
            if (m_RoundSettingsUI.ListStiches.Count > 0)
            {
                m_SaveRoundSettings.Show();

            }else
            {
                m_Message.MessageText = "The current round does not have any stiches.";
                m_Message.OnOK += OnOkMessage;
                m_Message.Show();
            }
        }

        public void OnOkMessage()
        {
            m_Message.OnOK -= OnOkMessage;
            m_Message.Hide();
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

                List<Stich> specialStiches = new List<Stich>();

                // Add all stiches
                for (int i = 0; i < m_RoundSettingsUI.ListStiches.Count; i++)
                {
                    Stich stich = m_RoundSettingsUI.ListStiches[i];
                    m_CurrentRound.Stiches.Add(stich);
                    if (stich.SpecialStich && !stich.CountAsStich)
                    {
                        specialStiches.Add(stich);
                    }
                }

                // Generate all repeats for this round and count total number stiches
                m_CurrentRound.TotalNumberStiches = 0;              

                for (int iRepeat = 0; iRepeat < m_CurrentRound.Repeats; iRepeat++)
                {
                    for (int iStich = 0; iStich < m_RoundSettingsUI.ListStiches.Count; iStich++)
                    {
                        Stich stich = m_RoundSettingsUI.ListStiches[iStich];                       

                        // Only count if it's not a special stich and special stich but count as a stich
                        if ((!stich.SpecialStich) || ((stich.SpecialStich) && (stich.CountAsStich)))
                        {
                            m_CurrentRound.AllRepeatsStiches.Add(stich);
                            if (stich.SpecialStich)
                            {
                                m_CurrentRound.TotalNumberStiches += stich.NumberRepeats;
                            }else
                            {
                                PatternSettings.ESTICH eStich = (PatternSettings.ESTICH)stich.IdStich;
                                switch(eStich)
                                {
                                    case PatternSettings.ESTICH.INCREASE:
                                    case PatternSettings.ESTICH.INVISIBLE_INCREASE:
                                        // 2 Stiches when increase
                                        m_CurrentRound.TotalNumberStiches += (stich.NumberRepeats * 2);
                                    break;
                                    case PatternSettings.ESTICH.DECREASE:
                                    case PatternSettings.ESTICH.INVISIBLE_DECREASE:
                                        // Decrease count as a 1 only.
                                        m_CurrentRound.TotalNumberStiches += (stich.NumberRepeats);
                                    break;
                                    default:
                                        // Reast of them number or repeats
                                        m_CurrentRound.TotalNumberStiches += stich.NumberRepeats;
                                    break;
                                }
                            }
                        }
                        
                    }
                }

                // Add special stiches (not count as a stiches)
                for (int i= 0; i<specialStiches.Count; i++)
                {
                    m_CurrentRound.AllRepeatsStiches.Add(specialStiches[i]);
                }


                //m_SaveRoundSettings.Hide();
                // Save current round in JSON and create new round 
                int numberRounds = AppController.Instance.SaveRound(m_CurrentRound);


                m_SaveRoundSettings.ShowConfirm("The current round has been saved with " + m_CurrentRound.TotalNumberStiches + " stich(es)");
                
                
                               

                /*m_Message.MessageText = "The current round has been saved with " + m_CurrentRound.TotalNumberStiches + " stich(es)";
                m_Message.OnOK += OnOkMessage;
                m_Message.Show();*/

                // Update Round
                int indexRound =  m_CurrentRound.RoundNumber + 1;
                m_CurrentRound = new Round();
                m_CurrentRound.PartName = m_PartSettings.PartName;
                m_CurrentRound.RoundNumber = indexRound;

                m_Title.text = m_CurrentRound.PartName + "  - Round: " + m_CurrentRound.RoundNumber;

                m_RoundSettingsUI.Clear();
            }
        }
       
	}
}
