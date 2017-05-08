using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class RoundSettingsUI : MonoBehaviour
    {
        string[] m_Stiches = new string[] {
            "Magic Ring", "Slip Stich","Single Crotchet","Increasec",
            "Invisible Increase", "Decrease", "Invisible Decrease", "Double Crotchet",
            "Half Double Crotchet" };

        string[] m_StichesAbbreviations = new string[] {
            "Mg", "Sl", "Sc", "Inc",
            "Inv Inc", "Dec", "Inv Dec", "Dc",
            "Hdc" };

        [Header("Stich Type")]
        [SerializeField] private Dropdown m_StichType;
        [SerializeField] private InputField m_Repeats;
        private int m_StichSelected = 0;

        [Header("Special Stich")]
        [SerializeField] private InputField m_SpecialStich;
        [SerializeField] private InputField m_SpecialRepeats;
       
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
            for (int i = 0; i < m_Stiches.Length; i++)
            {
                dropDownList.Add(m_Stiches[i] + "(" + m_StichesAbbreviations[i] + ")");
            }
            m_StichType.AddOptions(dropDownList);
            Clear();
        }

        public void OnValuStichTypeChange(int id)
        {
            Debug.Log("OnValuStichTypeChange: " + id);
            m_StichSelected = id;
        }

        private void Clear()
        {
            m_SpecialStich.text = string.Empty;

            m_StichSelected = 0;
            m_StichType.value = m_StichSelected;
            m_Repeats.text = "0";
            m_SpecialRepeats.text = "0";

        }

        public void AddStich()
        {
            Stich stich = new Stich();
            stich.NumberRepeats = int.Parse(m_Repeats.text);
            stich.Abbr = m_StichesAbbreviations[m_StichSelected];
            stich.Name = m_Stiches[m_StichSelected];
            m_ListStiches.Add(stich);



            // Check reps for stich
            /*int reps = int.Parse(m_Repeats.text);

            // Add curent Stich
            string currentS = m_StichesAbbreviations[m_StichSelected];

            // Check reps
            if (reps > 0)
            {
                if (m_StichSelected == 0)
                {
                    currentS = m_StichesAbbreviations[m_StichSelected] + " " + reps.ToString();
                }
                else
                {
                    currentS = reps.ToString() + " " + m_StichesAbbreviations[m_StichSelected];
                }
            }
            m_ListStiches.Add(currentS);*/
        }

        public void AddSpecialStich()
        {
            Stich stich = new Stich();
            stich.NumberRepeats = int.Parse(m_SpecialRepeats.text);
            stich.Abbr = m_SpecialStich.text;
            stich.Name = m_SpecialStich.text;
            m_ListStiches.Add(stich);


            // Check reps for stich
            /*int reps = int.Parse(m_SpecialRepeats.text);

            string stichTest = string.Empty;            
            // Check reps
            if (reps > 0)
            {
                stichTest = reps.ToString() + " " + m_SpecialStich.text;
            }else
            {
                stichTest = m_SpecialStich.text;
            }
            m_ListStiches.Add(stichTest);*/
        }

        public string PrintStiches()
        {
            string printS = string.Empty;
            if ((m_ListStiches != null) && (m_ListStiches.Count > 0))
            {
                for (int i=0; i< m_ListStiches.Count; i++)
                {
                    if (m_ListStiches[i].NumberRepeats > 0)
                    {
                        printS += " - " + m_ListStiches[i].NumberRepeats + " " + m_ListStiches[i].Name;
                    }else
                    {
                        printS += " - " + m_ListStiches[i].Name;
                    }
                    
                    if (i < (m_ListStiches.Count - 1))
                    {
                        printS += "\n";
                    }
                }
            }

            return printS;
        }

        public Round CreateRound(int numberRepeatsPerStich, int roundNumber)
        {
            Round round = new Round();
            round.RoundNumber = roundNumber;

            if ((m_ListStiches != null) && (m_ListStiches.Count > 0))
            {
                for (int i = 0; i < m_ListStiches.Count; i++)
                {
                    // Fill the summary
                    round.AddStich(m_ListStiches[i]);
                }

                // Generate all repeats for list of stiches
                for (int iRepeat = 0; iRepeat < numberRepeatsPerStich; iRepeat++)
                {                   
                    for (int i = 0; i < m_ListStiches.Count; i++)
                    {
                        // Fill the summary
                        round.AddStichesAllRepeats(m_ListStiches[i]);
                    }
                }
            }
            return round;
        }
    }
}
