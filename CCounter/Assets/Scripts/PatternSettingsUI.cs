using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class PatternSettingsUI : UIBase
    {

        [SerializeField] private InputField m_PatternName;
        [SerializeField] private RoundSettingsUI m_RoundSettingsUI;
        [SerializeField] private Text m_CurrentTextRound;
        [SerializeField] private InputField m_RoundNumber;
        [SerializeField] private InputField m_RepeatsPerGroupStiches;

        private Round m_CurrentRound;
        private int m_CurrentRoundNumber = 1;

        public int RepeatsPerGroupStiches
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
        }

        private void Start()
        {
            m_RoundSettingsUI.Init();

            m_CurrentRound = new Round();
            m_CurrentRoundNumber = 1;
            m_RoundNumber.text = m_CurrentRoundNumber.ToString();
            m_RepeatsPerGroupStiches.text = "1";
            m_CurrentTextRound.text = "";
        }

        public void OnAddStich()
        {
            m_RoundSettingsUI.AddStich();
            int roundNumber = int.Parse(m_RoundNumber.text);
            m_CurrentTextRound.text = RepeatsPerGroupStiches.ToString() + " Repeat(s) For: \n" + m_RoundSettingsUI.PrintStiches();
        }

        public void OnAddSpecialStich()
        {
            m_RoundSettingsUI.AddSpecialStich();
            int roundNumber = int.Parse(m_RoundNumber.text);
            m_CurrentTextRound.text = RepeatsPerGroupStiches.ToString() + " Repeat(s) For: \n"  + m_RoundSettingsUI.PrintStiches();
        }

        public void OnSaveRound()
        {            
            int nRepeatsPerGroup = RepeatsPerGroupStiches;

            int auxRoundN = 0;
            if (int.TryParse(m_RoundNumber.text, out auxRoundN))
            {
                m_CurrentRoundNumber = auxRoundN;
            }

            m_CurrentRound = m_RoundSettingsUI.CreateRound(nRepeatsPerGroup, m_CurrentRoundNumber);
            if (!string.IsNullOrEmpty(m_PatternName.text))
            {
                m_CurrentRound.NamePattern = m_PatternName.text;
            }
            else
            {
                m_CurrentRound.NamePattern = "Pattern";
            }

            if (m_CurrentRound.Stiches.Count > 0)
            { 

                // Add round to  list of rounds
                AppController.Instance.AddRound(m_CurrentRound);

                m_CurrentRound.Clear();
                m_CurrentTextRound.text = "";
                RepeatsPerGroupStiches = 1;

                m_CurrentRoundNumber += 1;
                m_RoundNumber.text = m_CurrentRoundNumber.ToString();
                
            }else
            {
                m_CurrentTextRound.text = "Add some stiches, It was not possible to generate JSON data";
            }
        }

        public void OnRemoveRound()
        {
            if (m_CurrentRound != null)
            {
                m_CurrentRound.Clear();
            }

            m_CurrentRoundNumber -= 1;
            if (m_CurrentRoundNumber <= 0)
            {
                m_CurrentRoundNumber = 1;
            }
            m_RoundNumber.text = m_CurrentRoundNumber.ToString();
        }


         
        
	}
}
