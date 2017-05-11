using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CCounter
{
    public class AppController : MonoBehaviour
    {
        [SerializeField]
        private PatternSettingsUI m_PatternSettingsUI;

        [SerializeField]
        private RoundSelectorUI m_RoundSelectorUI;

        private void Start()
        {
            m_RoundSelectorUI.Hide();
            m_PatternSettingsUI.Show();
        }

        public void OnShowRoundListMenu()
        {
            m_PatternSettingsUI.Hide();
            m_RoundSelectorUI.Show();
        }

    }
}
