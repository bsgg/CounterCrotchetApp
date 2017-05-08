﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class PatternSettingsUI : MonoBehaviour
    {
        [SerializeField]
        private InputField m_NamePattern;

        [SerializeField] private RoundSettingsUI m_RoundSettingsUI;
        [SerializeField] private Text m_CurrentTextRound;


        /* [Header("Cell Round")]
         [SerializeField]
         private Dropdown m_StichType;

         [SerializeField]
         private InputField m_NumberRepetitionsStich;

         [SerializeField]
         private InputField m_SpecificStich;

         [SerializeField]
         private Text m_TestCell;

         private List<string> m_CurrentStiches = new List<string>();

         //List<string> m_ListDropDownStiches;
         string[] m_Stiches = new string[] {
             "Magic Ring", "Slip Stich","Single Crotchet","Increasec",
             "Invisible Increase", "Decrease", "Invisible Decrease", "Double Crotchet",
             "Half Double Crotchet" };
         string[] m_StichesAbbreviations = new string[] {
             "Mg", "Sl", "Sc", "Inc",
             "Inv Inc", "Dec", "Inv Dec", "Dc",
             "Hdc" };

         private int m_StichSelected = 0;*/

        [SerializeField]
        private InputField m_NumberRepsPerCell;

        private Round m_CurrentRound;
        private PatternSettings m_PatterSettings;

        private void Start()
        {
            m_PatterSettings = new PatternSettings();
            m_RoundSettingsUI.Init();

            m_CurrentTextRound.text = "Round 3 Stich(es):";
            /*List<string> dropDownList = new List<string>();
            for (int i= 0; i< m_Stiches.Length; i++)
            {
                dropDownList.Add(m_Stiches[i] + "(" + m_StichesAbbreviations[i] + ")");
            }
            m_StichType.AddOptions(dropDownList);
            m_StichSelected = 0;
            m_StichType.value = m_StichSelected;


            Show();*/
        }

        public void OnAddStich()
        {
            m_RoundSettingsUI.AddStich();
            m_CurrentTextRound.text = "Round 3 Stich(es):" + m_RoundSettingsUI.PrintStiches();
        }

        public void OnAddSpecialStich()
        {
            m_RoundSettingsUI.AddSpecialStich();
            m_CurrentTextRound.text = "Round 3 Stich(es):" + m_RoundSettingsUI.PrintStiches();
        }



        /*public void Show()
        {
            m_NamePattern.text= "";
            m_CurrentTextRound.text = "Stich(es): ";
            m_NumberRepsPerCell.text = "1";
            ClearCurrentCellStich();
        }*/




       /* public void OnValuStichTypeChange(int id)
        {
            Debug.Log("OnValuStichTypeChange: " + id);
            m_StichSelected = id;
        }*/

        /*public void OnAddCell()
        {
            // Check reps for stich
            int reps = int.Parse(m_NumberRepetitionsStich.text);

            // Add curent Stich
            string currentSt = string.Empty;

            // Check if specific stich is empty
            if (!string.IsNullOrEmpty(m_SpecificStich.text))
            {
                currentSt = m_SpecificStich.text;
                if (reps > 0)
                {
                    currentSt += " " + reps.ToString();
                }

                m_CurrentStiches.Add(currentSt);

                // Set test cell
                string cell = currentSt;
                if (reps > 0)
                {
                    cell += " x " + reps.ToString();
                }
                m_TestCell.text += cell;
            }
            // otherwise, select from predefined stiches
            else
            {
                currentSt = m_StichesAbbreviations[m_StichSelected];
                // Check reps
                if (reps > 0)
                {
                    if (m_StichSelected == 0)
                    {
                        currentSt = m_StichesAbbreviations[m_StichSelected] + " " + reps.ToString();
                    }
                    else
                    {
                        currentSt = reps.ToString() + " " + m_StichesAbbreviations[m_StichSelected];
                    }
                }
                m_CurrentStiches.Add(currentSt);

                // Set test cell
                string cell = " " + m_Stiches[m_StichSelected] + "(" + m_StichesAbbreviations[m_StichSelected] + ")";
                if (reps > 0)
                {
                    cell += " x " + reps.ToString();
                }
                m_TestCell.text += cell;
            }

            // Clean selection
            ClearCurrentCellStich();
        }

        private void ClearCurrentCellStich()
        {
            m_SpecificStich.text = string.Empty;
            m_StichSelected = 0;
            m_StichType.value = m_StichSelected;
            m_NumberRepetitionsStich.text = "0";
        }*/
        
      /*  public void OnRemoveCurrentCell()
        {
            ClearCurrentCellStich();
            m_TestCell.text = "Stich(es): ";
            m_CurrentStiches.Clear();
            m_CurrentStiches = new List<string>();
        }   */    

        /*public void GenerateRound()
        {
            if (m_CurrentStiches.Count > 0)
            {
                m_CurrentRound = new Round();
                int reps = int.Parse(m_NumberRepsPerCell.text);
                if (reps <= 0)
                {
                    reps = 1;
                }

                // 1 Stich
                string stich = "";
                for (int iStich = 0; iStich < m_CurrentStiches.Count; iStich++)
                {
                    stich += m_CurrentStiches[iStich] + " ";
                }
                stich += "x" + reps;
                m_CurrentRound.Summary = stich;

                // Total Stiches
                string stiches = "";
                for (int iReps = 0; iReps < reps; iReps++)
                {
                    for (int iStich = 0; iStich < m_CurrentStiches.Count; iStich++)
                    {
                        stiches += m_CurrentStiches[iStich] + " ";
                    }
                    m_CurrentRound.Stiches.Add(stiches);
                    stiches += " , ";
                }

                // TODO: TOTAL STICHES!!
                m_PatterSettings.AddRound(m_CurrentRound);
            }          
        }

        private void RemoveCurrentRound()
        {
            m_CurrentRound.Clear();
            m_PatterSettings.RemoveLastRound();
        }

        public void GeneratePattern()
        {
            m_PatterSettings.Name = m_NamePattern.text;
            CCFileUtil.SavePatternJSON(m_PatterSettings);
        }*/
	}
}
