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
        //public string URL;
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
        [SerializeField] private string m_IndexFileName = "Index.json";
        [SerializeField] private string m_URLServer = "Patterns";

        [SerializeField] private string m_LocalRootPath = "D:/Downloads/CCrotchet";

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

        private List<Pattern> m_PatternList;
        public List<Pattern> PatternList
        {
            get
            {
                return m_PatternList;
            }
        }

        public void Init()
        {
            m_FileData = new FileData();
            //m_LocalPath = Path.Combine(Application.dataPath, m_PatternFolder);

            if (!Directory.Exists(m_LocalRootPath))
            {
                Directory.CreateDirectory(m_LocalRootPath);
            }

            Debug.Log("<color=purple>" + "[CCFileUtil.Init] Local Path: " + m_LocalRootPath + "</color>");
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

            fileName += "_" + round.PartName;
            string fileNameExt = fileName + ".json";
            m_FilePath = Path.Combine(m_LocalRootPath, fileNameExt);

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

        public void CreateFileIndex()
        {
            // Get name files
            if (Directory.Exists(m_LocalRootPath))
            {
                FileData data = new FileData();

                string[] auxFiles = Directory.GetFiles(m_LocalRootPath, "*.json");
                if (auxFiles != null)
                {
                    for (int i = 0; i < auxFiles.Length; i++)
                    {
                        string filename = Path.GetFileNameWithoutExtension(auxFiles[i]);
                        filename.Trim();
                        string fileNameExt = Path.GetFileName(auxFiles[i]);
                        fileNameExt.Trim();

                        if (fileNameExt != m_IndexFileName)
                        {
                            IndexFile index = new IndexFile();
                            index.FileName = filename;
                            //index.URL = fileNameExt;
                            data.Data.Add(index);
                        }
                    }
                }

                if ((data != null) && (data.Data != null) && (data.Data.Count  > 0))
                {
                    string json = JsonUtility.ToJson(data);
                    string path = Path.Combine(m_LocalRootPath, m_IndexFileName);

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    using (FileStream fs = File.Create(path))
                    {
                        byte[] info = new UTF8Encoding(true).GetBytes(json);
                        fs.Write(info, 0, info.Length);
                        fs.Close();
                    }

                    Debug.Log("<color=purple>" + "[CCFileUtil.Save] File index created: "+ path + "</color>");
                }
                else
                {
                    Debug.Log("<color=purple>" + "[CCFileUtil.Save] No data to create file "  + "</color>");
                }
            }
        }

        public IEnumerator Load()
        {
            m_PatternList = new List<Pattern>();
            m_FileData = new FileData();

            string urlFile = Path.Combine(m_URLServer, m_IndexFileName);

            AppController.Instance.Launcher.Description = "\n - Retrieving files from " + urlFile;

            WWW wwwFile = new WWW(urlFile);


            yield return wwwFile;
            string jsonData = wwwFile.text;
            if (!string.IsNullOrEmpty(jsonData))
            {
                m_FileData = JsonUtility.FromJson<FileData>(jsonData);

                Debug.Log("<color=purple>" + "[CCFileUtil] Requesting... " + m_FileData.Data.Count + " Files " + "</color>");

                AppController.Instance.Launcher.Description += "\n - Requesting " + m_FileData.Data.Count + " Files ";

                for (int i = 0; i < m_FileData.Data.Count; i++)
                {

                    if (string.IsNullOrEmpty(m_FileData.Data[i].FileName))
                    {
                        continue;
                    }

                    // Request 
                    string fileNameExt = m_FileData.Data[i].FileName + ".json";
                    urlFile = Path.Combine(m_URLServer, fileNameExt);

                    Debug.Log("<color=purple>" + "[CCFileUtil] Requesting: " + (i + 1) + "/" + m_FileData.Data.Count + " : " + urlFile + "</color>");


                    AppController.Instance.Launcher.Description += "\n - Requesting " + (i + 1) + " / " + m_FileData.Data.Count + " : " + urlFile;


                    WWW www = new WWW(urlFile);

                    yield return www;

                    string data = www.text;

                    if (!string.IsNullOrEmpty(data))
                    {
                        try
                        {
                            Round round = JsonUtility.FromJson<Round>(data);

                            string[] splitted = m_FileData.Data[i].FileName.Split('_');

                            if (splitted != null && splitted.Length > 1)
                            {
                                string designName = splitted[1].Trim();

                                // Check if this design exist or new design
                                bool found = false;
                                int indexP = 0;
                                for (indexP = 0; (indexP < m_PatternList.Count) && (!found); indexP++)
                                {
                                    if (m_PatternList[indexP].Name.ToLower() == designName.ToLower())
                                    {
                                        found = true;
                                        break;
                                    }
                                }

                                if (found)
                                {
                                    m_PatternList[indexP].Rounds.Add(round);
                                }
                                else
                                {

                                    m_PatternList.Add(new Pattern(designName, round));
                                }

                            }
                        }
                        catch(Exception e)
                        {
                            Debug.Log("<color=purple>" + "[CCFileUtil] Unable to parse data" + "</color>");

                            AppController.Instance.Launcher.Description += "\n - Unable to parse data: " + urlFile;
                        }
                    }else
                    {
                        Debug.Log("<color=purple>" + "[CCFileUtil] Data is empty" + "</color>");
                    }
                }

                AppController.Instance.Launcher.Description += "\n - Completed: " + urlFile;
            }             
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
