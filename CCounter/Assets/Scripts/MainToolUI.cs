using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class MainToolUI : UIBase
    {
        [SerializeField] private Text m_FilePath;

        public override void Show()
        {
            m_FilePath.text = ToolController.Instance.FileHandler.LocalRootPath;

            base.Show();
        }

    }
}
