﻿using System.Text;
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
            string path = root + round.NamePattern + "_Round_" + round.RoundNumber + ".json";       

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
                        lfiles.Add(filename);
                    }
                }
            }

            return lfiles;
        }

        public static bool LoadRoundJSON(string fileName, out Round rounData)
        {
            rounData = null;
            // Load data from resources
            string pathFile = "Patterns/" + fileName;

            TextAsset text_asset = (TextAsset)Resources.Load(pathFile, typeof(TextAsset));
            if (text_asset == null)
            {
                Debug.Log("ERROR: Could not find file: Assets/Resources/" + pathFile);
                return false;
            }

            string json_string = text_asset.ToString();
            if (!string.IsNullOrEmpty(json_string))
            {
                rounData = JsonMapper.ToObject<Round>(json_string);
                return true;
            }

            return false;
        }

    }
}
