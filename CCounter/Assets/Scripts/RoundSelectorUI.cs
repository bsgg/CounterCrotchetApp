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
            m_RoundListScroll.OnButtonPress += OnRoundPress;
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
                    title = r.PartName + " - R" + r.RoundNumber + " (Front Loops): ";
                }
                else if (r.TypeRound == Round.ETYPEROUND.BACKLOOP)
                {
                    title = r.PartName + " - R" + r.RoundNumber + " (Back Loops): ";
                }
                else
                {
                    title = r.PartName + " - R" + r.RoundNumber + " : ";
                }

                title += r.RoundNumber;
                if (r.Repeats > 1)
                {
                    title += "  - Repeat x " + r.Repeats.ToString();
                }

                lTitle.Add(title);
            }
            m_RoundListScroll.InitScroll(lTitle);
        }

        private void OnRoundPress(int id)
        {
            m_RoundListScroll.OnButtonPress -= OnRoundPress;
            m_SelectedRound = id;

            AppController.Instance.ShowRoundCounter(m_SelectedPattern, m_SelectedRound);
        }        
        
    }
}
