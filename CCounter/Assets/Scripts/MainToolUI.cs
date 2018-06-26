using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace CCounter
{
    public class MainToolUI : UIBase
    {
        [SerializeField] private Text m_FilePath;
        [SerializeField] private InputField m_FilePathInput;
        [SerializeField] private InputField m_FileNameInput;

        public override void Show()
        {
            //m_FilePath.text = ToolController.Instance.FileHandler.LocalRootPath;

            m_FilePathInput.text = ToolController.Instance.FileHandler.LocalRootPath;
            m_FileNameInput.text = ToolController.Instance.FileHandler.IndexFileName;

            base.Show();
        }

        public void OnUpdateSettings()
        {
            ToolController.Instance.FileHandler.UpdateToolPath(m_FilePathInput.text);

            /*string path = m_FilePathInput.text;
            Debug.Log("PATH " + path);

            if (string.IsNullOrEmpty(path))
            {
                ToolController.Instance.MessagePopup.ShowPopup("Error",
                  "Path is empty",
                   "OK", OnOkPopupBtnPress,
                  string.Empty, null, string.Empty, null);

                return;
            }

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    ToolController.Instance.MessagePopup.ShowPopup("Error",
                   "There was a problem creating folder: " + path,
                   "OK", OnOkPopupBtnPress,
                   string.Empty, null, string.Empty, null);
                }

            }*/

        }
        

    }
}
