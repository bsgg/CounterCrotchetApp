using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCounter
{
    public class ToolController : MonoBehaviour
    {
        #region Instance
        private static ToolController m_Instance;
        public static ToolController Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = (ToolController)FindObjectOfType(typeof(ToolController));

                    if (m_Instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(ToolController) + " is needed in the scene, but there is none.");
                    }
                }
                return m_Instance;
            }
        }
        #endregion Instance

        [SerializeField] private CCFileUtil m_FileHandler;
        public CCFileUtil FileHandler
        { 
            get { return m_FileHandler; }
        }

        [SerializeField] private DesignControl m_DesignControl;

        [SerializeField]
        private MessagePopup m_MessagePopup;
        public MessagePopup MessagePopup
        {
            get { return m_MessagePopup; }
        }

        [SerializeField]
        private TopBar m_TopBar;
        public TopBar TopBar
        {
            get { return m_TopBar; }
        }

        [SerializeField]
        private MenuTool m_MenuTool;
        public MenuTool MenuTool
        {
            get { return m_MenuTool; }
        }
        

        private void Start()
        {
            //m_FileHandler.CreateFileIndex();
            m_MenuTool.Hide();
            m_MessagePopup.Hide();
            //m_DesignControl.Show();
        }

        #region MainMenu

        public void OnMenuPress()
        {
            m_MenuTool.Show();
        }

        public void OnNewDesignPress()
        {
            m_MessagePopup.Hide();
            m_DesignControl.Show();
        }

        #endregion MainMenu


    }
}
