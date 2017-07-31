using System.Text;
using System;
using UnityEngine;
using System.IO;
using LitJson;
using System.Collections.Generic;

namespace CCounter
{
    public class CCFileUtil
    {
        public static string PATHJSONFILES = "/Resources/Patterns/";

        public static void SaveRoundToJSON(Round round)
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

        public static List<string> ListJSONFiles()
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
                        string filename = Path.GetFileNameWithoutExtension(auxFiles[i]);
                        filename.Trim();
                        lfiles.Add(filename);
                    }
                }
            }

            return lfiles;
        }

        public static bool LoadRoundJSON(string fileName, out Round rounData)
        {
            rounData = null;

            string pathFile = Application.dataPath + PATHJSONFILES + fileName + ".json";
            if (!File.Exists(pathFile))
            {
                DebugManager.Instance.Log("FileNotFound: " + pathFile + "\n");
            }
            else
            {
                DebugManager.Instance.Log("File: " + pathFile + "\n");
                string jsonString = File.ReadAllText(pathFile);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    rounData = JsonMapper.ToObject<Round>(jsonString);
                    return true;

                }else
                {
                    DebugManager.Instance.Log("jsonString is null or empty\n");
                }
            }

            return false;
        }

        public static int RemoveAllRounds()
        {
            List<string> listFiles = ListJSONFiles();

            int numberFilesRemoved = 0;

            if (listFiles != null)
            {
                numberFilesRemoved = listFiles.Count;
                for (int i= listFiles.Count -1; i > 0; i--)
                {
                    File.Delete(listFiles[i]);
                }
            }

            return numberFilesRemoved;            
        }

    }
}
