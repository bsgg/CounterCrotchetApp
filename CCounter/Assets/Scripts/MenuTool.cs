using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class MenuTool : UIBase
    {
        [SerializeField] private InputField m_FilePathInput;
        [SerializeField] private InputField m_FileNameInput;

        public override void Show()
        {
            m_FilePathInput.text = ToolController.Instance.FileHandler.LocalRootPath;
            m_FileNameInput.text = ToolController.Instance.FileHandler.IndexFileName;

            base.Show();            
        }


       

        
    }
}
