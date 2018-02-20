using ScrollList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class RoundSelectorUI : UIBase
    {
        [SerializeField] private ScrollPanelUI m_RoundListScroll;

        private int m_SelectedPattern;
        private int m_SelectedRound;

        public override void Show()
        {
            base.Show();

            // Load patterns 
            LoadPatternList();
            m_RoundListScroll.OnButtonPress += OnPatternPress;

        }

        private void LoadPatternList()
        {
            List<string> lPattern = new List<string>();
            for (int i=0; i<  AppController.Instance.Patterns.Count; i++)
            {
                lPattern.Add(AppController.Instance.Patterns[i].Name);

            }
            m_RoundListScroll.InitScroll(lPattern);
        }

        private void OnPatternPress(int id)
        {
            m_RoundListScroll.OnButtonPress -= OnPatternPress;
            m_SelectedPattern = id;

            LoadRoundsList();
            
        }

        private void LoadRoundsList()
        {
            List<string> lTitle = new List<string>();
            for (int i = 0; i < AppController.Instance.Patterns[m_SelectedPattern].Rounds.Count; i++)
            {
                Round r = AppController.Instance.Patterns[m_SelectedPattern].Rounds[i];
                string title = "";
                if (r.TypeRound == Round.ETYPEROUND.FRONTLOOPY)
                {
                   // title = r.PartName + " - R" + r.RoundNumber + " (Front Loops): ";
                    title ="R" + r.RoundNumber + " (FLO): ";
                }
                else if (r.TypeRound == Round.ETYPEROUND.BACKLOOP)
                {
                    //title = r.PartName + " - R" + r.RoundNumber + " (Back Loops): ";
                    title = "R" + r.RoundNumber + " (BLO): ";
                }
                else
                {
                    //title = r.PartName + " - R" + r.RoundNumber + " : ";
                    title = "R" + r.RoundNumber + ": ";
                }

                if (r.Stiches != null)
                {
                    //Debug.Log("[RoundSelectorUI.LoadRoundsList] Stiches " + r.Stiches.Count + " for: " + r.PartName + " " + r.RoundNumber);
                    for (int s = 0; s < r.Stiches.Count; s++)
                    {
                        title += r.Stiches[s].NumberRepeats + r.Stiches[s].Name;
                        if (s < (r.Stiches[s].NumberRepeats-1))
                        {
                            title += ", ";
                        }
                    }

                    //title += r.RoundNumber;
                    if (r.Repeats > 1)
                    {
                        title += " x " + r.Repeats.ToString();
                    }
                }else
                {
                    Debug.Log("[RoundSelectorUI.LoadRoundsList] No stiches for " + r.PartName + " " + r.RoundNumber);
                }

                lTitle.Add(title);
            }
            m_RoundListScroll.InitScroll(lTitle);

            for (int i=0; i< m_RoundListScroll.Num(); i++)
            {
                GameObject go = m_RoundListScroll.Get(i);

                if (go != null)
                {
                    CheckedMenuButton chk = go.GetComponent<CheckedMenuButton>();
                    if (chk != null)
                    {
                        Round r = AppController.Instance.Patterns[m_SelectedPattern].Rounds[i];
                        if (r.IsCompleted)
                        {
                            chk.Check();
                        }else
                        {
                            chk.UnCheck();
                        }                        
                    }                
                }
            }

            m_RoundListScroll.OnButtonPress += OnRoundPress;
        }

        private void OnRoundPress(int id)
        {
            m_RoundListScroll.OnButtonPress -= OnRoundPress;
            m_SelectedRound = id;

            AppController.Instance.SelectRoundInPattern(m_SelectedPattern, m_SelectedRound);

            // Selects menu
            AppController.Instance.SelectMenu(AppController.ETYPEMENU.ROUNDCOUNTER);
        }        
        
    }
}
