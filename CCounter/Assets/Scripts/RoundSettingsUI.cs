using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class RoundSettingsUI : MonoBehaviour
    {
        [SerializeField] private Text m_CurrentRound;
        public string CurrentRound
        {
            get { return m_CurrentRound.text; }
            set { m_CurrentRound.text = value; }
        }

        [Header("Stich Type")]
        [SerializeField] private Dropdown m_StichType;
        [SerializeField] private InputField m_Repeats;
        private int m_StichSelected = 0;

        [Header("Special Stich")]
        [SerializeField] private InputField m_SpecialStich;
        [SerializeField] private InputField m_SpecialRepeats;
        [SerializeField] private Toggle m_CountAsStich;
       
        private List<Stich> m_ListStiches = new List<Stich>();
        public List<Stich> ListStiches
        {
            get { return m_ListStiches; }
            set { m_ListStiches = value; }
        }

        public void Init()
        {
            // Init Drowdown
            List<string> dropDownList = new List<string>();
            for (int i = 0; i < PatternSettings.Stiches.Length; i++)
            {
                dropDownList.Add(PatternSettings.Stiches[i] + "(" + PatternSettings.StichesAbbreviations[i] + ")");
            }
            m_StichType.AddOptions(dropDownList);
            Clear();
        }

        public void OnValuStichTypeChange(int id)
        {
            m_StichSelected = id;
        }

        public void Clear()
        {
            m_ListStiches = new List<Stich>();
            m_CurrentRound.text = string.Empty;
            m_SpecialStich.text = string.Empty;

            m_StichSelected = 0;
            m_StichType.value = m_StichSelected;
            m_Repeats.text = "1";
            m_SpecialRepeats.text = "1";
            m_CountAsStich.isOn = true;

        }

        public void AddStich()
        {
            Stich stich = new Stich();

            stich.IdStich = m_StichSelected;
            stich.Abbr = PatternSettings.StichesAbbreviations[m_StichSelected];
            stich.Name = PatternSettings.Stiches[m_StichSelected];
            stich.SpecialStich = false;
            stich.CountAsStich = true;

            stich.NumberRepeats = 0;
            int auxRepeats = 0;
            if (int.TryParse(m_Repeats.text, out auxRepeats))
            {
                stich.NumberRepeats = auxRepeats;
            }

            m_ListStiches.Add(stich);

            // Remove stich selected
            m_StichSelected = 0;
            m_StichType.value = m_StichSelected;
            m_Repeats.text = "1";

            m_CurrentRound.text = PrintStiches();

        }

        public void AddSpecialStich()
        {
            if (!string.IsNullOrEmpty(m_SpecialStich.text))
            {
                Stich stich = new Stich();

                stich.IdStich = 0;
                stich.Abbr = m_SpecialStich.text;
                stich.Name = m_SpecialStich.text;
                stich.SpecialStich = true;
                stich.CountAsStich = false;

                stich.NumberRepeats = 0;
                if (m_CountAsStich.isOn)
                {
                    stich.CountAsStich = true;
                    int auxRepeats = 0;
                    if (int.TryParse(m_SpecialRepeats.text, out auxRepeats))
                    {
                        stich.NumberRepeats = auxRepeats;
                    }
                }

                m_ListStiches.Add(stich);

                m_SpecialRepeats.text = "1";
                m_SpecialStich.text = string.Empty;

                m_CurrentRound.text = PrintStiches();
            }
        }

        public void RemoveStiches()
        {
            m_ListStiches.Clear();
            m_ListStiches = new List<Stich>();
            Clear();
        }

        public string PrintStiches()
        {
            string printS = string.Empty;
            if ((m_ListStiches != null) && (m_ListStiches.Count > 0))
            {
                for (int i=0; i< m_ListStiches.Count; i++)
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

                    // Special stich no stich, only show name
                    if ((m_ListStiches[i].SpecialStich) && (!m_ListStiches[i].CountAsStich))
                    {
                        auxS = " - " + m_ListStiches[i].Name;
                    } 

                    printS += auxS;

                    if (i < (m_ListStiches.Count - 1))
                    {
                        printS += "\n";
                    }
                }
            }

            return printS;
        }        
    }
}
