using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScrollList
{
    public class CellText : MonoBehaviour
    {
        [SerializeField]
        private Text m_Text;

        public void SetText(string text)
        {
            m_Text.text = text;
        }
		
	}
}
