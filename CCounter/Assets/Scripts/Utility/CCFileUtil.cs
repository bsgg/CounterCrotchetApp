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


        [SerializeField] private string m_URLServer = "Patterns";

        public IEnumerator SaveAndUpload(Round round)
        {
            // Convert to JSON
            string json = JsonUtility.ToJson(round);
            byte[] data = Encoding.UTF8.GetBytes(json);

            string root = Path.Combine(Application.persistentDataPath, m_PatternFolder);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            string fileName = round.RoundNumber.ToString();
            if (round.RoundNumber < 10)
            {
                fileName = "0" + round.RoundNumber.ToString();
            }
            fileName += "_" + round.PartName + ".json";
            string path = Path.Combine(root,fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Saving data
            using (FileStream fs = File.Create(path))
            {
                fs.Write(data, 0, data.Length);
                fs.Close();
            }

            Debug.Log("<color=purple>" + "[CCFileUtil.SaveAndUpload] SaveAndUpload Save file (" + data.Length + ") at: " + path + "</color>");


            /*Debug.Log("<color=purple>" + "[CCFileUtil.SaveAndUpload] Uploading to " + m_URLServer + "</color>");


            ServicePointManager.ServerCertificateValidationCallback = delegate (
                    System.Object obj, X509Certificate certificate, X509Chain chain,
                    SslPolicyErrors errors)
            {               
                return true;
            };

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(m_URLServer);
            request.Method = "POST";
            request.KeepAlive = true;
            request.PreAuthenticate = true;
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = data.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            yield return new WaitForEndOfFrame();
            try
            {
                // Get response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
                {
                    string result = rdr.ReadToEnd();
                    Debug.Log("<color=purple>" + "[CCFileUtil.SaveAndUpload] Upload complete "+ result + "</color>");
                }
            }
            catch (Exception e)
            {
                Debug.Log("<color=purple>" + "[CCFileUtil.SaveAndUpload] Unable to upload: " + e.ToString() + "</color>");
            }
            */
            yield return new WaitForEndOfFrame();
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
