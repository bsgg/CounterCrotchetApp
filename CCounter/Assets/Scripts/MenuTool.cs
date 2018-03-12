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


        public void GenerateIndexFile()
        {
            string path = m_FilePathInput.text;
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
                catch(Exception e)
                {
                    ToolController.Instance.MessagePopup.ShowPopup("Error",
                   "There was a problem creating folder: " + path,
                   "OK", OnOkPopupBtnPress,
                   string.Empty, null, string.Empty, null);
                }
                
            }

            string message;

            string fileName = m_FileNameInput.text;

            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "Index.json";
            }

            if (ToolController.Instance.FileHandler.CreateFileIndex(path, fileName, out message))
            {
                ToolController.Instance.MessagePopup.ShowPopup("Completed",
                  message,
                  "OK", OnOkPopupBtnPress,
                  string.Empty, null, string.Empty, null);
            }else
            {
                ToolController.Instance.MessagePopup.ShowPopup("Error",
                message,
                "OK", OnOkPopupBtnPress,
                string.Empty, null, string.Empty, null);
            }

        }

        public void CleanFiles() 
        {
            string path = m_FilePathInput.text;
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

            }
        }

        private void OnOkPopupBtnPress()
        {
            ToolController.Instance.MessagePopup.Hide();
        }

    }
}
