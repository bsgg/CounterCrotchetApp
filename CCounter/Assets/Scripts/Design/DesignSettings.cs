using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class DesignSettings : UIBase
    {
        [SerializeField] private InputField m_PartNameInput;
        [SerializeField] private InputField m_PartStartIndexInput;

        public int RoundStartIndex
        {
            get
            {
                int partStartIndex = 1;
                int auxStartIndex = 0;
                if (int.TryParse(m_PartStartIndexInput.text, out auxStartIndex))
                {
                    partStartIndex = auxStartIndex;
                }

                return partStartIndex;
            }
        }


        public string Name
        {
            get
            {
                string partName = "Part";
                if (!string.IsNullOrEmpty(m_PartNameInput.text))
                {
                    partName = m_PartNameInput.text;
                }
               
                return partName;
            }
        }

        public override void Show()
        {
            base.Show();
            m_PartNameInput.text = "Part";
            m_PartStartIndexInput.text = "1";

        }
    }
}
