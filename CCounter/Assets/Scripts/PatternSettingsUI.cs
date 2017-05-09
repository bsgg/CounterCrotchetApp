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
            int numberRepeatsPerGroup = 1;

            int.TryParse(m_RepeatsPerGroupStiches.text, out numberRepeatsPerGroup));
            

            //int numberRepeats = int.Parse(m_RepeatsPerGroupStiches.text);
            int roundNumber = int.Parse(m_RoundNumber.text);

            Round round = m_RoundSettingsUI.CreateRound(numberRepeatsPerGroup, roundNumber);
            CCFileUtil.SaveRoundToJSON(round, m_PatternName.text);

            Debug.Log("SAVE ROUND TO JSON");
        }
        
	}
}
