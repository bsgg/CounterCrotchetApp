﻿using System.Text;
using System;
using UnityEngine;
using System.IO;
using LitJson;

namespace CCounter
{
    public class CCFileUtil
    {
        public static string PATHJSONFILES = "/Resources/Patterns/";

        public static bool SavePatternJSON(PatternSettings pattern)
        {
            // Generate json data
            string jsonString = LitJson.JsonMapper.ToJson(pattern);

            string root = Application.dataPath + PATHJSONFILES;

            // Check directory
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            // Check path
            string path = root +pattern.Name + ".json";
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

            return true;
        }


        public static bool LoadMazeJSON(string namePattern, out PatternSettings pattenrData)
        {
            pattenrData = null;
            // Load data from resources
            string pathFile = "Patterns/" + namePattern;

            TextAsset text_asset = (TextAsset)Resources.Load(pathFile, typeof(TextAsset));
            if (text_asset == null)
            {
                Debug.Log("ERROR: Could not find file: Assets/Resources/" + pathFile);
                return false;
            }

            string json_string = text_asset.ToString();
            if (!string.IsNullOrEmpty(json_string))
            {
                pattenrData = JsonMapper.ToObject<PatternSettings>(json_string);
                return true;
            }

            return false;

        }
    }
}
