﻿using System.Text;
using System;
using UnityEngine;
using System.IO;
using LitJson;
using System.Collections.Generic;

namespace CCounter
{
    public class CCFileUtil: MonoBehaviour
    {
        [SerializeField] private string m_PatternFolder = "Patterns";

        public void Init()
        {
            string localDirectory = Path.Combine(Application.persistentDataPath,m_PatternFolder);

            

            Debug.Log("localDirectory " + localDirectory);

        }


        public static string PATHJSONFILES = "/Resources/Patterns/";



        public void SaveRoundToJSON(Round round)
        {
            string root = Application.dataPath + PATHJSONFILES;

            string jsonString = JsonMapper.ToJson(round);           

            // Check directory
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            // Check path
            string roundN = round.RoundNumber.ToString();
            if (round.RoundNumber < 10)
            {
                roundN = "0" + round.RoundNumber.ToString();
            }
            string path = root + roundN + "_" + round.PartName + ".json";

            Debug.Log("[APPController.CCFileUtil] SaveRoundToJSON path: " + path);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream fs = File.Create(path))
            {
                byte[] info = new System.Text.UTF8Encoding(true).GetBytes(jsonString);
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
        }

        public List<string> ListJSONFiles(bool includePath = false)
        {
            List<string> lfiles = new List<string>();
            // Get name files
            string root = Application.dataPath  + PATHJSONFILES;
            if (Directory.Exists(root))
            {
                string[] auxFiles = Directory.GetFiles(root, "*.json");
                if (auxFiles != null)
                {
                    for (int i = 0; i < auxFiles.Length; i++)
                    {
                        if (includePath)
                        {
                            lfiles.Add(auxFiles[i].Trim());
                        }
                        else
                        {                            
                            string filename = Path.GetFileNameWithoutExtension(auxFiles[i]);
                            filename.Trim();
                            lfiles.Add(filename);
                        }
                    }
                }
            }

            return lfiles;
        }

        public bool LoadRoundJSON(string fileName, out Round rounData)
        {
            rounData = null;

            string pathFile = Application.dataPath + PATHJSONFILES + fileName + ".json";
            if (!File.Exists(pathFile))
            {
                DebugManager.Instance.Log("FileNotFound: " + pathFile + "\n");
                Debug.Log("[CCFileUtil.LoadRoundJSON] File Not Found: " + pathFile + "\n");
            }
            else
            {
                DebugManager.Instance.Log("File: " + pathFile + "\n");
                //Debug.Log("[CCFileUtil.LoadRoundJSON] File found: " + pathFile + "\n");
                try
                {
                    string jsonString = File.ReadAllText(pathFile);
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        rounData = JsonMapper.ToObject<Round>(jsonString);
                        Debug.Log("[CCFileUtil.LoadRoundJSON] Load Json file: " + rounData.PartName);

                        return true;
                    }
                    else
                    {
                        DebugManager.Instance.Log("jsonString is null or empty\n");
                        Debug.Log("[CCFileUtil.LoadRoundJSON] json file " + pathFile + " null or empty");
                        return false;
                    }

                }
                catch (Exception e)
                {

                    DebugManager.Instance.Log("Mal formed File: " + pathFile);
                    Debug.Log("[CCFileUtil.LoadRoundJSON] unable to parse " + pathFile + " malformed json format");
                    return false;

                }
            }

            return false;
        }

        public int RemoveAllRounds()
        {
            List<string> listFiles = ListJSONFiles(true);

            int numberFilesRemoved = 0;

            if (listFiles != null)
            {
                numberFilesRemoved = listFiles.Count;
                for (int i= listFiles.Count -1; (i >= 0); i--)
                {
                    Debug.Log("[CCFileUtil] Remove file:  " + listFiles[i]);
                    if (File.Exists(listFiles[i]))
                    {
                        File.Delete(listFiles[i]);
                    }
                }
            }

            return numberFilesRemoved;            
        }

    }
}
