using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class SaveRoundSettings : UIBase
    {
        [Header("Save Round Content")]
        [SerializeField]
        private GameObject m_ContentSaveRound;

        [SerializeField] private InputField m_RepeatsRound;
        [SerializeField] private Toggle m_NormalToggle;
        [SerializeField] private Toggle m_FrongLoopToggle;
        [SerializeField] private Toggle m_BackLoopToggle;


        [Header("Confirm Save Round Content")]
        [SerializeField]
        private GameObject m_ConfirmSaveRound;
        [SerializeField]
        private Text m_ConfirmText;

        public int RepeatsRound
        {
            get
            {
                int repeats = 1;
                int auxRepeats = 0;
                if (int.TryParse(m_RepeatsRound.text, out auxRepeats))
                {
                    repeats = auxRepeats;
                }

                return repeats;
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

                }else if (m_FrongLoopToggle.isOn)
                {
                    tRound = Round.ETYPEROUND.FRONTLOOPY;

                }else if (m_BackLoopToggle.isOn)
                {
                    tRound = Round.ETYPEROUND.BACKLOOP;
                }

                return tRound;
            }
        }

        public override void Show()
        {
            base.Show();
            m_RepeatsRound.text = "1";
            m_NormalToggle.isOn = true;
            m_FrongLoopToggle.isOn = false;
            m_BackLoopToggle.isOn = false;


            m_ContentSaveRound.SetActive(true);
            m_ContentSaveRound.SetActive(false);

        }

        public override void Hide()
        {
            base.Hide();

            m_RepeatsRound.text = "1";
            m_NormalToggle.isOn = true;
            m_FrongLoopToggle.isOn = false;
            m_BackLoopToggle.isOn = false;


            m_ContentSaveRound.SetActive(true);
            m_ContentSaveRound.SetActive(false);
        }
       

        public void ShowConfirm(string text)
        {
            m_ConfirmText.text = text;
            m_ContentSaveRound.SetActive(false);
            m_ContentSaveRound.SetActive(true);
        }

        
    }
}
