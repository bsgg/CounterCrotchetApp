using System.Text;
using UnityEngine;
using System.IO;
using LitJson;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace CCounter
{
    [Serializable]
    public class IndexFile
    {
        public string FileName;
        public string URL;
        public string Data;
    }

    [Serializable]
    public class FileData
    {
        public List<IndexFile> Data;
        public FileData()
        {
            Data = new List<IndexFile>();
        }        
    }

    public class CCFileUtil: MonoBehaviour
    {
        public static string PATHJSONFILES = "/Resources/Patterns/";


        [SerializeField] private string m_PatternFolder = "Patterns";
        [SerializeField] private string m_URLServer = "Patterns";

        private string m_LocalPath;
        private string m_FilePath;
        public string FilePath
        {
            get { return m_FilePath; }
        }

        [SerializeField]
        private FileData m_FileData;
        public FileData FileData
        {
            get
            {
                return m_FileData;
            }
        }

        public void Init()
        {
            m_LocalPath = Path.Combine(Application.dataPath, m_PatternFolder);

            if (!Directory.Exists(m_LocalPath))
            {
                Directory.CreateDirectory(m_LocalPath);
            }

            Debug.Log("<color=purple>" + "[CCFileUtil.Init] Local Path: " + m_LocalPath + "</color>");
        }
        
        public bool Save(Round round)
        {
            // Convert to JSON
            string json = JsonUtility.ToJson(round);
            byte[] data = Encoding.UTF8.GetBytes(json);

           
            string fileName = round.RoundNumber.ToString();
            if (round.RoundNumber < 10)
            {
                fileName = "0" + round.RoundNumber.ToString();
            }
            fileName += "_" + round.PartName + ".json";
            m_FilePath = Path.Combine(m_LocalPath, fileName);

            try
            {
                if (File.Exists(m_FilePath))
                {
                    File.Delete(m_FilePath);
                }

                // Saving data
                using (FileStream fs = File.Create(m_FilePath))
                {
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.Log("<color=purple>" + "[CCFileUtil.Save] Unable to save: " + e.ToString() + "</color>");
            }

            return false;
        }


        public IEnumerator Load()
        {

            m_FileData = new FileData();

            string urlFile = Path.Combine(m_URLServer, "Index.json");

            WWW wwwFile = new WWW(urlFile);
            yield return wwwFile;
            string jsonData = wwwFile.text;
            if (!string.IsNullOrEmpty(jsonData))
            {
                Debug.Log("<color=purple>" + "[CCFileUtil] Requesting... " + jsonData + " Files " + "</color>");

                m_FileData = JsonUtility.FromJson<FileData>(jsonData);

                Debug.Log("<color=purple>" + "[CCFileUtil] Requesting... " + m_FileData.Data.Count + " Files " + "</color>");
                for (int i = 0; i < m_FileData.Data.Count; i++)
                {
                    if (string.IsNullOrEmpty(m_FileData.Data[i].URL))
                    {
                        continue;
                    }

                    // Request
                    Debug.Log("<color=blue>" + "[CCFileUtil] Requesting: " + (i + 1) + "/" + m_FileData.Data.Count + " : " + m_FileData.Data[i].FileName + "</color>");
                    WWW www = new WWW(m_FileData.Data[i].URL);
                    yield return www;

                    m_FileData.Data[i].Data = www.text;
                }

            }

                yield return new WaitForEndOfFrame();
            /* WWW wwwFile = new WWW(m_URLServer);

             yield return wwwFile;

             string jsonData = wwwFile.text;


             yield return new WaitForEndOfFrame();*/
        }




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
