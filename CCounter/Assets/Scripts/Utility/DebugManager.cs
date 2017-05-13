using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class DebugManager : MonoBehaviour
    {
        #region Instance
        private static DebugManager m_Instance;
        public static DebugManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = (DebugManager) FindObjectOfType(typeof(DebugManager));

                    if (m_Instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(DebugManager) + " is needed in the scene, but there is none.");
                    }
                }
                return m_Instance;
            }
        }
        #endregion Instance

        [SerializeField]
        private Text m_DebugText;
        private string m_Debug = string.Empty;

        public void Log(string text)
        {
            m_Debug += text;
            m_DebugText.text = m_Debug;
        }
        
    }
}
