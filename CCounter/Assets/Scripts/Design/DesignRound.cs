using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class DesignRound : UIBase
    {
        [SerializeField] private Text m_CurrentRound;
        public string CurrentRound
        {
            get { return m_CurrentRound.text; }
            set { m_CurrentRound.text = value; }
        }

        [Header("Stich Type")]
        [SerializeField] private InputField m_Stich;
        public string Stich
        {
            get { return m_Stich.text; }
        }
        [SerializeField] private InputField m_Repeats;

        /*[SerializeField]
        private Toggle m_NoDuplicateStichToogle;*/


        [Header("Round Settings")]
        [SerializeField]
        private InputField m_RoundRepeats;
        [SerializeField]
        private Toggle m_NormalToggle;
        [SerializeField]
        private Toggle m_FrongLoopToggle;
        [SerializeField]
        private Toggle m_BackLoopToggle;

        public int RoundRepeat
        {
            get
            {
                int repeats = 1;
                int auxRepeats = 0;
                if (int.TryParse(m_RoundRepeats.text, out auxRepeats))
                {
                    repeats = auxRepeats;
                }

                return repeats;
            }
        }

        public int StichRepeats
        {
            get
            {
                int repeats = 1;
                int auxRepeats = 0;
                if (int.TryParse(m_Repeats.text, out auxRepeats))
                {
                    repeats = auxRepeats;
                }

                return repeats;
            }
        }

        public bool NoDuplicate
        {
            get
            {
                return m_BackLoopToggle.isOn;
            }
        }

        public Round.ETYPEROUND TypeRound
        {
            get
            {
                Round.ETYPEROUND tRound = Round.ETYPEROUND.NORMAL;

                if (m_NormalToggle.isOn)
                {
                    tRound = Round.ETYPEROUND.NORMAL;

                }
                else if (m_FrongLoopToggle.isOn)
                {
                    tRound = Round.ETYPEROUND.FRONTLOOPY;

                }
                else if (m_BackLoopToggle.isOn)
                {
                    tRound = Round.ETYPEROUND.BACKLOOP;
                }

                return tRound;
            }
        }
        
        public override void Show()
        {
            base.Show();
            Reset();
        }

        public void Reset()
        {
           // m_ListStiches = new List<Stich>();
            m_CurrentRound.text = string.Empty;

            //m_StichSelected = 0;
            m_Stich.text = string.Empty;
            m_Repeats.text = "1";

            //m_NoDuplicateStichToogle.isOn = false;

            m_RoundRepeats.text = "1";
            m_NormalToggle.isOn = true;
            m_FrongLoopToggle.isOn = false;
            m_BackLoopToggle.isOn = false;
            
        }
    }
}
