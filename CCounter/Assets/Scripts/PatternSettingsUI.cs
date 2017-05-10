using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class PatternSettingsUI : MonoBehaviour
    {
        [SerializeField]
        private InputField m_PatternName;

        [SerializeField] private RoundSettingsUI m_RoundSettingsUI;
        [SerializeField] private Text m_CurrentTextRound;
        [SerializeField] private InputField m_RoundNumber;

        [SerializeField] private InputField m_RepeatsPerGroupStiches;

        //private Round m_CurrentRound;
        private PatternSettings m_PatterSettings;

        private void Start()
        {
            m_PatterSettings = new PatternSettings();
            m_RoundSettingsUI.Init();

            m_RoundNumber.text = "1";
            m_RepeatsPerGroupStiches.text = "1";

            int roundNumber = 0;
            m_CurrentTextRound.text = "Round " + roundNumber + " Stich(es)";
        }

        public void OnAddStich()
        {
            m_RoundSettingsUI.AddStich();
            int roundNumber = int.Parse(m_RoundNumber.text);
            m_CurrentTextRound.text = "Round " + roundNumber + " Stich(es):\n" + m_RoundSettingsUI.PrintStiches();
        }

        public void OnAddSpecialStich()
        {
            m_RoundSettingsUI.AddSpecialStich();
            int roundNumber = int.Parse(m_RoundNumber.text);
            m_CurrentTextRound.text = "Round " + roundNumber + " Stich(es):\n" + m_RoundSettingsUI.PrintStiches();
        }


        public void OnSaveRound()
        {
            int nRepeatsPerGroup = 1;
            int auxNRepeats = 0;
            if (int.TryParse(m_RepeatsPerGroupStiches.text, out auxNRepeats))
            {
                nRepeatsPerGroup = auxNRepeats;
            }

            int roundNumber = 1;
            int auxRoundN = 0;
            if (int.TryParse(m_RoundNumber.text, out auxRoundN))
            {
                roundNumber = auxRoundN;
            }
            Round round = m_RoundSettingsUI.CreateRound(nRepeatsPerGroup, roundNumber);

            CCFileUtil.SaveRoundToJSON(round, m_PatternName.text);

            Debug.Log("SAVE ROUND TO JSON");
        }
        
	}
}
