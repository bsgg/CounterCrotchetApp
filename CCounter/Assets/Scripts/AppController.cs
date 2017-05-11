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
        private UIBase m_MainMenuUI;

        [SerializeField]
        private PatternSettingsUI m_PatternSettingsUI;

        [SerializeField]
        private RoundSelectorUI m_RoundSelectorUI;

        [SerializeField]
        private RounCounterUI m_RoundCounterUI;

        private void Start()
        {
            SelectMenu(ETYPEMENU.MAINMENU);
        }

        private void SelectMenu(ETYPEMENU menu)
        {            
            m_CurrentMenu = menu;
            switch (menu)
            {
                case ETYPEMENU.MAINMENU:

                    m_RoundCounterUI.Hide();
                    m_RoundSelectorUI.Hide();
                    m_PatternSettingsUI.Hide();
                    m_MainMenuUI.Show();

                break;

                case ETYPEMENU.PATTERNSETTINGS:

                    m_RoundCounterUI.Hide();
                    m_RoundSelectorUI.Hide();
                    m_PatternSettingsUI.Show();
                    m_MainMenuUI.Hide();
                break;

                case ETYPEMENU.ROUNDCOUNTER:
                    m_RoundCounterUI.Show();
                    m_RoundSelectorUI.Hide();
                    m_PatternSettingsUI.Hide();
                    m_MainMenuUI.Hide();
                    break;

                case ETYPEMENU.ROUNDSELECTOR:
                    m_RoundCounterUI.Hide();
                    m_RoundSelectorUI.Show();
                    m_PatternSettingsUI.Hide();
                    m_MainMenuUI.Hide();
                break;
            }
        }

        public void OnAddNewRound()
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

        public void OnShowRoundCounter(Round round)
        {            
            m_RoundCounterUI.CurrentRound = round;

            SelectMenu(ETYPEMENU.ROUNDCOUNTER);            
        }

    }
}
