using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCounter
{
    public class AppController : MonoBehaviour
    {
        #region Instance
        private static AppController m_Instance;
        public static AppController Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = (AppController) FindObjectOfType(typeof(AppController));

                    if (m_Instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(AppController) + " is needed in the scene, but there is none.");
                    }
                }
                return m_Instance;
            }
        }
        #endregion Instance

        public enum ETYPEMENU {  MAINMENU, PATTERNSETTINGS, ROUNDSELECTOR, ROUNDCOUNTER };

        private ETYPEMENU m_CurrentMenu = ETYPEMENU.MAINMENU;

        [SerializeField]
        private MainMenu m_MainMenuUI;

        [SerializeField]
        private PartPattern m_PartPattern;

        [SerializeField]
        private RoundSelectorUI m_RoundSelectorUI;

        [SerializeField]
        private RoundCounterUI m_RoundCounterUI;

        [SerializeField]
        private List<Round> m_RoundList;

        [SerializeField]
        private List<Pattern> m_Pattern;
        private int m_CurrentRoundIDSelected;

        private void Start()
        {
            LoadRoundsFromJSON();
            m_RoundCounterUI.Init();
            m_CurrentRoundIDSelected = 0;
            SelectMenu(ETYPEMENU.MAINMENU);            
        }

        private void LoadRoundsFromJSON()
        {
            m_RoundList = new List<Round>();

            // Get current list of rounds
            List<string> listFiles = CCFileUtil.ListJSONFiles(false);

            DebugManager.Instance.Log("FilesFound(" + listFiles.Count + ")\n");
            m_Pattern = new List<Pattern>();

            int nErrorRounds = 0;
            for (int i = 0; i < listFiles.Count; i++)
            {
                Round round = new Round();
                if (CCFileUtil.LoadRoundJSON(listFiles[i], out round))
                {
                    //m_RoundList.Add(round);
                }
                else
                {
                    nErrorRounds++;

                    Debug.Log("[APPController] It was not possible to get the round");
                }

                // Check categorie
                string[] splitted = listFiles[i].Split('_');
                if (splitted != null && splitted.Length > 1)
                {
                    string nameDesign = splitted[1].ToLower().Trim();

                    // Check if this design exist or new design
                    bool found = false;
                    int indexP = 0;
                    for (indexP = 0; (indexP < m_Pattern.Count)&&(!found); indexP++)
                    {
                        if (m_Pattern[indexP].Name == nameDesign)
                        {
                            found = true;
                        }
                    }
                    if (found)
                    {
                        m_Pattern[indexP].Rounds.Add(round);
                    }else
                    {
                        m_Pattern.Add(new Pattern(nameDesign, round));
                    }
                }
            }

            if (nErrorRounds > 0)
            {
                DebugManager.Instance.Log("Errors(" + nErrorRounds + ")\n");
            }
        }

        public Round GetRoundById(int id)
        {
            if (m_RoundList != null && id < m_RoundList.Count)
            {
                return m_RoundList[id];
            }

            return null;
        }

        public int NumberRounds
        {
            get
            {
                if (m_RoundList != null)
                {
                    return m_RoundList.Count;
                }
                return 0;
            }
        }


        public List<string> GetListRounds()
        {
            List<string> lTitles = new List<string>();

            // Load rounds from JSON first
            LoadRoundsFromJSON();

            if (m_RoundList != null)
            {
                for (int i=0; i< m_RoundList.Count; i++)
                {
                    string titleRound = "";                    

                    if (m_RoundList[i].TypeRound == Round.ETYPEROUND.FRONTLOOPY)
                    {
                        titleRound = m_RoundList[i].PartName + " - R" + m_RoundList[i].RoundNumber + " (Front Loops): ";
                    }else if (m_RoundList[i].TypeRound == Round.ETYPEROUND.BACKLOOP)
                    {
                        titleRound = m_RoundList[i].PartName + " - R" + m_RoundList[i].RoundNumber + " (Back Loops): ";
                    }else
                    {
                        titleRound = m_RoundList[i].PartName + " - R" + m_RoundList[i].RoundNumber + " : ";
                    }
                    

                    for (int iStich = 0; iStich < m_RoundList[i].Stiches.Count; iStich++)
                    {
                        /*if (m_RoundList[i].Stiches[iStich].SpecialStich && !m_RoundList[i].Stiches[iStich].CountAsStich)
                        {
                            titleRound += " " + m_RoundList[i].Stiches[iStich].Name;
                        }
                        else
                        {
                            titleRound += m_RoundList[i].Stiches[iStich].NumberRepeats.ToString() + " " + m_RoundList[i].Stiches[iStich].Abbr;
                        }*/

                        //titleRound += m_RoundList[i].Stiches[iStich].NumberRepeats.ToString() + " " + m_RoundList[i].Stiches[iStich].Abbr;

                        if (iStich < m_RoundList[i].Stiches.Count - 1)
                        {
                            titleRound += " , ";
                        }
                    }

                    if (m_RoundList[i].Repeats > 1)
                    {
                        titleRound += "  - Repeat x " + m_RoundList[i].Repeats.ToString();
                    }

                    lTitles.Add(titleRound);
                }
            }

            return lTitles;
        }

        public int SaveRound(Round round)
        {            
            CCFileUtil.SaveRoundToJSON(round);
            m_RoundList.Add(round);
            

            return m_RoundList.Count;
        }

        private void SelectMenu(ETYPEMENU menu)
        {            
            m_CurrentMenu = menu;
            switch (menu)
            {
                case ETYPEMENU.MAINMENU:

                    m_RoundCounterUI.Hide();
                    m_RoundSelectorUI.Hide();
                    m_PartPattern.Hide();
                    m_MainMenuUI.Show();

                break;

                case ETYPEMENU.PATTERNSETTINGS:

                    m_RoundCounterUI.Hide();
                    m_RoundSelectorUI.Hide();
                    m_PartPattern.Show();
                    m_MainMenuUI.Hide();
                break;

                case ETYPEMENU.ROUNDCOUNTER:
                    m_RoundCounterUI.Show();
                    m_RoundSelectorUI.Hide();
                    m_PartPattern.Hide();
                    m_MainMenuUI.Hide();
                    break;

                case ETYPEMENU.ROUNDSELECTOR:
                    m_RoundCounterUI.Hide();
                    m_RoundSelectorUI.Show();
                    m_PartPattern.Hide();
                    m_MainMenuUI.Hide();
                break;
            }
        }

        public void OnAddNewPatternPart()
        {
            SelectMenu(ETYPEMENU.PATTERNSETTINGS);
        }

        public void OnShowMainMenu()
        {
            SelectMenu(ETYPEMENU.MAINMENU);
        }

        public void OnSelectRound()
        {
            SelectMenu(ETYPEMENU.ROUNDSELECTOR);
        }

        public void OnRemoveAllRounds()
        {
            int nfiles = CCFileUtil.RemoveAllRounds();
            if (nfiles <= 0)
            {
                m_MainMenuUI.ShowConfirm(" There weren't any rounds to remove.");
            }else if (nfiles == 1)
            {
                m_MainMenuUI.ShowConfirm(" There was 1 round to remove.");
            }
            else
            {
                m_MainMenuUI.ShowConfirm(nfiles + " have been removed ");
            }

        }

        public void OnShowRoundCounter(int idRound)
        {
            m_CurrentRoundIDSelected = idRound;
            m_RoundCounterUI.SetRound(GetRoundById(m_CurrentRoundIDSelected));

            SelectMenu(ETYPEMENU.ROUNDCOUNTER);            
        }        

        public void FinishCurrentRoundInList(bool completed)
        {
            Round round = GetRoundById(m_CurrentRoundIDSelected);

            if (round != null)
            {
                round.IsCompleted = completed;
                CCFileUtil.SaveRoundToJSON(round);
            }
        }

        public Round GetNextRoundInList()
        {
            m_CurrentRoundIDSelected++;
            if (m_CurrentRoundIDSelected < m_RoundList.Count)
            {
                return m_RoundList[m_CurrentRoundIDSelected];
            }
            return null;
        }

    }
}
