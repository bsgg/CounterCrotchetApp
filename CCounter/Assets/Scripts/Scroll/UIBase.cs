using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class UIBase : MonoBehaviour
    {
        [SerializeField]  private CanvasGroup m_CanvasGroup;	

        public virtual void Init(){}

        public virtual void Finish(){}

        public virtual void Back() { }

        public virtual void Show()
        {
            if (m_CanvasGroup != null)
            {
                m_CanvasGroup.alpha = 1.0f;

                m_CanvasGroup.interactable = true;
                m_CanvasGroup.blocksRaycasts = true;
            }
        }

        public virtual void Hide()
        {
            if (m_CanvasGroup != null)
            {
                m_CanvasGroup.alpha = 0.0f;
                m_CanvasGroup.interactable = false;
                m_CanvasGroup.blocksRaycasts = false;
                    
            }
        }
    }
}
