using System.Text;
using System;
using UnityEngine;
using System.IO;
using LitJson;

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
    }
}
