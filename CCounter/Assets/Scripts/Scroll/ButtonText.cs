using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Utility
{
    public class ButtonText : MonoBehaviour
    {
        [SerializeField]
        private Text m_Text;
        [SerializeField]
        protected Button         m_ButtonComponent;

        private UnityAction m_ButtonCallback;

        public void Show(string text, UnityEngine.Events.UnityAction callback)
        {
            m_Text.text = text;

            gameObject.SetActive(true);

            m_ButtonCallback = callback;
            if (m_ButtonCallback != null)
            {
                m_ButtonComponent.onClick.RemoveListener(m_ButtonCallback);
            }
            m_ButtonComponent.onClick.AddListener(m_ButtonCallback);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
