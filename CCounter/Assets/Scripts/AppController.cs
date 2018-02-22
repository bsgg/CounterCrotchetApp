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

        public enum ETYPEMENU {  MAINMENU, PATTERNSETTINGS, ROUNDSELECTOR, ROUNDCOUNTER, PARTPATTERN };

        private ETYPEMENU m_CurrentMenu = ETYPEMENU.MAINMENU;

        [SerializeField]
        private TopBar m_TopBar;
        public TopBar TopBar
        {
            get { return m_TopBar; }
        }

        /*[SerializeField]
        private UIBase m_MainMenuUI;*/

        /*[SerializeField]
        private DesignSettings m_PartPatternSettings;*/

        [SerializeField]
        private RoundSelectorUI m_RoundSelectorUI;

        [SerializeField]
        private RoundCounterUI m_RoundCounterUI;        

        /*[SerializeField]
        private PartPattern m_PartPattern;
        */

        [SerializeField]
        private MessagePopup m_MessagePopup;
        public MessagePopup MessagePopup
        {
            get { return m_MessagePopup; }
        }

        [SerializeField]
        private LauncherUI m_Launcher;
        public LauncherUI Launcher
        {
            get { return m_Launcher; }
        }

        [SerializeField]
        private List<Pattern> m_Patterns;
        public List<Pattern> Patterns
        {
            get { return m_Patterns; }
        }

        [SerializeField] private CCFileUtil m_FileHandler;
        public CCFileUtil FileHandler
        {
            get { return m_FileHandler; }
        }

        private int m_SelectedPatternID;
        private int m_SelectedRoundID;
        private Round m_CurrentRound;

        public Round CurrentRound
        {
            get { return m_CurrentRound; }
        }


        private void Start()
        {          

            StartCoroutine(Init());


            //LoadPatterns();
            /*m_RoundCounterUI.Init();
            //m_CurrentRoundIDSelected = 0;
            SelectMenu(ETYPEMENU.MAINMENU);     */       
        }

        private IEnumerator Init()
        {
            m_RoundSelectorUI.Hide();
            m_MessagePopup.Hide();
            m_RoundCounterUI.Hide();

            m_Launcher.Show();

            m_FileHandler.Init();

            yield return m_FileHandler.Load();

            m_Launcher.OKButton.interactable = true;
            
        }

        public void OnOkLauncher()
        {
            m_Launcher.Hide();
            m_Patterns = m_FileHandler.PatternList;

            m_RoundSelectorUI.Show();
        }

        public void SelectRound(int patternID, int roundID)
        {
            m_CurrentRound = new Round();

            if ((m_Patterns == null) || (patternID < 0) || (patternID >= m_Patterns.Count)) return;
            if ((m_Patterns[patternID].Rounds == null) || (roundID < 0) || (roundID >= m_Patterns[patternID].Rounds.Count)) return;

            m_SelectedPatternID = patternID;
            m_SelectedRoundID = roundID;
            m_CurrentRound = m_Patterns[m_SelectedPatternID].Rounds[m_SelectedRoundID];

            m_RoundSelectorUI.Hide();
            m_RoundCounterUI.Show();
        }


        public bool MarkRound(bool completed)
        {
            if (m_CurrentRound != null)
            {
                m_CurrentRound.IsCompleted = completed;
               // m_CCFile.SaveRoundToJSON(m_CurrentRound);

                // Selects next round
                m_SelectedRoundID += 1;

                if (m_SelectedRoundID < m_Patterns[m_SelectedPatternID].Rounds.Count)
                {
                    SelectRound(m_SelectedPatternID, m_SelectedRoundID);

                    return true;
                }
            }

            return false;
        }



        /* private void LoadPatterns()
         {
             m_Patterns = new List<Pattern>();

             // Get current list of rounds
             List<string> listFiles = m_FileHandler.ListJSONFiles(false);

             DebugManager.Instance.Log("FilesFound(" + listFiles.Count + ")\n");


             for (int i = 0; i < listFiles.Count; i++)
             {
                 Round round = new Round();

                 bool roundLoaded = m_FileHandler.LoadRoundJSON(listFiles[i], out round);

                 if (roundLoaded)
                 {

                     // Check categorie
                     string[] splitted = listFiles[i].Split('_');
                     if (splitted != null && splitted.Length > 1)
                     {
                         string designName = splitted[1].Trim();

                         //Debug.Log("[AppController.LoadPatterns] designName " + designName);


                         //string designNameLower = designName.ToLower();

                         // Check if this design exist or new design
                         bool found = false;
                         int indexP = 0;
                         for (indexP = 0; (indexP < m_Patterns.Count) && (!found); indexP++)
                         {
                             if (m_Patterns[indexP].Name.ToLower() == designName.ToLower())
                             {
                                 found = true;
                                 break;
                             }
                         }
                         if (found)
                         {
                             //Debug.Log("[AppController.LoadPatterns] New round for a pattern " + designName);

                             m_Patterns[indexP].Rounds.Add(round);
                         }
                         else
                         {
                             //Debug.Log("[AppController.LoadPatterns] New pattern " + designName);

                             m_Patterns.Add(new Pattern(designName, round));
                         }
                     }
                 }
             }

             Debug.Log("[AppController.LoadPatterns] m_Patterns count " + m_Patterns.Count);

         }*/

        /**/

        /*public int NumberRounds
        {
            get
            {
                if (m_RoundList != null)
                {
                    return m_RoundList.Count;
                }
                return 0;
            }
        }*/


        /* public List<string> GetListRounds()
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

        /*  if (iStich < m_RoundList[i].Stiches.Count - 1)
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
}*/


        /*public void SelectMenu(ETYPEMENU menu)
        {            
            m_CurrentMenu = menu;

            // Hide all
            m_RoundCounterUI.Hide();
            m_RoundSelectorUI.Hide();

            m_MainMenuUI.Hide();
            m_MessagePopup.Hide();
            m_PartPatternSettings.Hide();


            switch (menu)
            {
                case ETYPEMENU.MAINMENU:
                    m_TopBar.Title = "Counter Crotchet";
                    
                    m_MainMenuUI.Show();

                break;

                case ETYPEMENU.PATTERNSETTINGS:
                    m_PartPatternSettings.Show();

                break;

                case ETYPEMENU.PARTPATTERN:
                    m_PartPattern.Show();
                break;

                case ETYPEMENU.ROUNDCOUNTER:
                    m_RoundCounterUI.Show();

                    break;

                case ETYPEMENU.ROUNDSELECTOR:
                    m_TopBar.Title = "Counter Crotchet";
                    m_RoundSelectorUI.Show();
                break;                
            }
        }*/

        /* public void OnBack()
         {
             // Hide all
             m_RoundCounterUI.Hide();
             m_RoundSelectorUI.Hide();
             m_PartPattern.Hide();
             m_MainMenuUI.Hide();
             m_MessagePopup.Hide();
             m_PartPatternSettings.Hide();

             switch (m_CurrentMenu)
             {
                 case ETYPEMENU.MAINMENU:
                     Application.Quit();
                 break;

                 case ETYPEMENU.PATTERNSETTINGS:
                 case ETYPEMENU.ROUNDCOUNTER:
                 case ETYPEMENU.ROUNDSELECTOR:
                 case ETYPEMENU.PARTPATTERN:
                     m_MainMenuUI.Show();
                 break;

             }
         }*/

        //#region NewDesign
        /* public void OnAddNewDesign()
         {
             SelectMenu(ETYPEMENU.PATTERNSETTINGS);
         }

         public void OnAcceptNewDesign()
         {
             m_PartPattern.CreateNewRound(m_PartPatternSettings.Name, m_PartPatternSettings.RoundStartIndex);
             SelectMenu(ETYPEMENU.PARTPATTERN);
         }
         #endregion NewDesign

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
             int nfiles = m_CCFile.RemoveAllRounds();
             if (nfiles <= 0)
             {
                 //m_MainMenuUI.ShowConfirm(" There weren't any rounds to remove.");
             }else if (nfiles == 1)
             {
                // m_MainMenuUI.ShowConfirm(" There was 1 round to remove.");
             }
             else
             {
                // m_MainMenuUI.ShowConfirm(nfiles + " have been removed ");
             }

         }*/

        /*public void OnShowRoundCounter(int idRound)
        {
            m_CurrentRoundIDSelected = idRound;
            //m_RoundCounterUI.SetRound(GetRoundById(m_CurrentRoundIDSelected));

            SelectMenu(ETYPEMENU.ROUNDCOUNTER);            
        } */



        /* public void FinishCurrentRoundInList(bool completed)
         {*/
        /*Round round = GetRoundById(m_CurrentRoundIDSelected);

        if (round != null)
        {
            round.IsCompleted = completed;
            CCFileUtil.SaveRoundToJSON(round);
        }*/
        /* }

         public Round GetNextRoundInList()
         {*/
        /*m_CurrentRoundIDSelected++;
        if (m_CurrentRoundIDSelected < m_RoundList.Count)
        {
            return m_RoundList[m_CurrentRoundIDSelected];
        }*/
        /* return null;
     }*/


        /*private int m_SelectedPatternID;
        private int m_SelectedRoundID;
        private Round m_CurrentRound;*/

        /*public Round CurrentRound
        {
            get { return m_CurrentRound; }
        }*/



        /* public void SelectRoundInPattern(int idPattern, int idRound)
         {
             m_CurrentRound = new Round();

             if ((m_Patterns == null) || (idPattern < 0) || (idPattern >= m_Patterns.Count)) return;
             if ((m_Patterns[idPattern].Rounds == null) || (idRound < 0) || (idRound >= m_Patterns[idPattern].Rounds.Count)) return;

             m_SelectedPatternID = idPattern;
             m_SelectedRoundID = idRound;
             m_CurrentRound = m_Patterns[idPattern].Rounds[idRound];

            // m_RoundCounterUI.SelectedRound = m_CurrentRound;            
         }*/

        /*public bool MarkRound(bool completed)
        {           
            if (m_CurrentRound != null)
            {
                m_CurrentRound.IsCompleted = completed;
                m_CCFile.SaveRoundToJSON(m_CurrentRound);

                // Selects next round
                m_SelectedRoundID += 1;

                if (m_SelectedRoundID < m_Patterns[m_SelectedPatternID].Rounds.Count)
                {
                    SelectRoundInPattern(m_SelectedPatternID, m_SelectedRoundID);

                    return true;
                }
            }

            return false;
        }*/

        /* public int SaveRound(Round round)
         {
             m_CCFile.SaveRoundToJSON(round);         
             return 0;
         }
         */


        #region Menu

        public void OnMenuPress()
        {

        }


        #endregion Menu

    }
}
