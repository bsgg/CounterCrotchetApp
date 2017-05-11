using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ScrollList
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] protected Text           m_LabelTitleButton;
        [SerializeField]
        protected Button         m_ButtonComponent;
        private int                             m_IdButton;

        public delegate void OnMenuButtonPress(int index);

        public void SetupMenuButton(string title, int id, OnMenuButtonPress callback)
        {
            m_LabelTitleButton.text = title;
            m_IdButton = id;
            m_ButtonComponent.onClick.AddListener(() => { callback(m_IdButton); });
        }
    }
}
