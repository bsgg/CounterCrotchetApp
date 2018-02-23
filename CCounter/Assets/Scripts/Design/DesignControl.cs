using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CCounter 
{
    public class DesignControl : UIBase
    {
        [SerializeField] private DesignRound m_Round;
        [SerializeField] private DesignSettings m_Settings;

        private List<Stich> m_ListStiches;
        private Round m_CurrentRound;
        private int m_RoundNumber;


        public override void Show()
        {
            base.Show();

            m_ListStiches = new List<Stich>();
            m_CurrentRound = new Round();           

            m_Round.Show();
            m_Settings.Show();
        }

        #region Settings
       
        public void OnSettingsAcceptPress()
        {            
            m_CurrentRound.PartName = m_Settings.Name;
            m_CurrentRound.RoundNumber = m_Settings.RoundStartIndex;
            m_RoundNumber = m_Settings.RoundStartIndex;

            ToolController.Instance.TopBar.Title = m_CurrentRound.PartName + "  - Round: " + m_CurrentRound.RoundNumber;

            m_Settings.Hide();
        }
        #endregion Settings

        #region Round
        public void AddStich()
        {
            if (!string.IsNullOrEmpty(m_Round.Stich))
            {
                Stich stich = new Stich();
                stich.Name = m_Round.Stich;

                stich.NumberRepeats = m_Round.StichRepeats;

                m_ListStiches.Add(stich);

                m_Round.CurrentRound = PrintStiches();
            }           
        }

        public void UndoStich()
        {
            if (m_ListStiches.Count > 0)
            {
                m_ListStiches.RemoveAt(m_ListStiches.Count - 1);
                m_Round.CurrentRound = PrintStiches();
            }
            else
            {
                m_Round.CurrentRound = string.Empty;
            }
        }

        private string PrintStiches()
        {
            string stiches = string.Empty;
            if ((m_ListStiches != null) && (m_ListStiches.Count > 0))
            {
                for (int i = 0; i < m_ListStiches.Count; i++)
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

                    stiches += auxS;

                    if (i < (m_ListStiches.Count - 1))
                    {
                        stiches += "\n";
                    }
                }
            }

            return stiches;
        }

        public void OnSaveRound()
        {
            if (m_ListStiches.Count > 0)
            {
                m_CurrentRound.Repeats = m_Round.RoundRepeat;
                m_CurrentRound.StichCount = 0;
                m_CurrentRound.Stiches = new List<Stich>();

                for (int i = 0; i < m_ListStiches.Count; i++)
                {
                    Stich stich = m_ListStiches[i];
                    m_CurrentRound.Stiches.Add(stich);
                }

                for (int iRepeat = 0; iRepeat < m_CurrentRound.Repeats; iRepeat++)
                {
                    for (int iStich = 0; iStich < m_ListStiches.Count; iStich++)
                    {
                        Stich stich = m_ListStiches[iStich];
                        m_CurrentRound.StichCount += stich.NumberRepeats;

                        // Find incremental 
                        string auxS = stich.Name.TrimStart().TrimEnd().ToLower();
                        if (auxS.Contains("inc") || auxS.Contains("incremental"))
                        {
                            Debug.Log("Number Stiches x2");
                            m_CurrentRound.StichCount += (stich.NumberRepeats * 2);
                        }
                    }
                }

                // Save
                Save();  
            }
            else
            {
                ToolController.Instance.MessagePopup.ShowPopup("No stiches",
                    "Include some stiches for the round",
                    "Ok", OnOkPopupBtnPress,
                    string.Empty, null, string.Empty, null);
            }
        }

        private void Save()
        {
            string message = "Saving.. Round " + m_RoundNumber + " - " + m_CurrentRound.StichCount + " stich(es)";
            ToolController.Instance.MessagePopup.ShowPopup("Saving", message,
                 string.Empty, null, string.Empty, null, string.Empty, null);

            bool success = ToolController.Instance.FileHandler.Save(m_CurrentRound);
            string path = ToolController.Instance.FileHandler.FilePath;
            message = string.Empty;
            if (success)
            {
                
                message = "Round " + m_RoundNumber + " - " + m_CurrentRound.StichCount + " stich(es) Saved at: " + path;
                ToolController.Instance.MessagePopup.ShowPopup("New round added", message,
                "Ok", OnOkPopupBtnPress, string.Empty, null, string.Empty, null);

            }
            else
            {
                message = "There was a problem with round " + m_RoundNumber  + " Path: " + path;
                ToolController.Instance.MessagePopup.ShowPopup("New round added", message,
                "Ok", OnOkPopupBtnPress, string.Empty, null, string.Empty, null);
            }

            // Reset all 
            string partName = m_CurrentRound.PartName;
            m_RoundNumber += 1;
            m_CurrentRound = new Round();
            m_CurrentRound.PartName = partName;
            m_CurrentRound.RoundNumber = m_RoundNumber;

            m_ListStiches = new List<Stich>();
            m_Round.Reset();

            ToolController.Instance.TopBar.Title = partName + "  - Round: " + m_CurrentRound.RoundNumber;
        }

        private void OnOkPopupBtnPress()
        {
            ToolController.Instance.MessagePopup.Hide();
        }

        #endregion Round
    }
}
