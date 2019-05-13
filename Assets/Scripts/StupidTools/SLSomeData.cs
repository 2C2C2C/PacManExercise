using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace HentaiTools
{



    public class SLSomeData
    {
        private static SLSomeData instance;
        public static SLSomeData Instance => instance ?? (instance = SLSomeData.GetANewOne());

        //m_paths.Add((new LevelData).GetType(),"ss")
        public Dictionary<Type, string> paths;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>wat u want</returns>
        private static SLSomeData GetANewOne()
        {
            SLSomeData wa = new SLSomeData
            {
                paths = new Dictionary<Type, string>()
            };
            wa.paths.Add((new LevelData().GetType()), $"{Application.dataPath}/MacMan/AppData/LevelData/");
            wa.paths.Add((new Ghost().GetType()), $"{Application.dataPath}/MacMan/AppData/KemonoData/");
            wa.paths.Add((new MacMan().GetType()), $"{Application.dataPath}/MacMan/AppData/KemonoData/");
            wa.paths.Add((new Pill().GetType()), $"{Application.dataPath}/MacMan/AppData/ItemData/");
            wa.paths.Add((new RecordData("wa").GetType()), $"{Application.dataPath}/MacMan/AppData/RecordData/");
            return wa;
        }



        public void SaveData<T>(string _fileName, T _obj)
        {
            if (_fileName == "")
            {
                Debug.LogError("file name should not be empty");
                return;
            }

            string outStr;
            try
            {
                outStr = JsonConvert.SerializeObject(_obj);
            }
            catch (Exception e)
            {
                Debug.LogError("ikd wat happens, but it error");
                //throw;
                return;
            }

            string path;
            if (!paths.ContainsKey(typeof(T)))
            {
                Debug.LogError("you can not save this kind of object");
                return;
            }
            else
                path = paths[typeof(T)];

            // wat if file exist???
            if (System.IO.File.Exists(path))
            {
                Debug.LogError("file exist, rename or something");
                return;
            }

            path = $"{path}{_fileName}.json";
            System.IO.File.WriteAllText(path, outStr);
            Debug.Log($"save {path} la");

        }


        /// <summary>
        /// file name should not contain File extensions!!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_fileName">should not contain File extensions!!!</param>
        /// <returns></returns>
        public T GetData<T>(string _fileName)
        {
            if (_fileName == "")
            {
                Debug.LogError("file name should not be empty");
                return default;
            }

            T something = default;
            string path;
            if (!paths.ContainsKey(typeof(T)))
            {
                Debug.LogError("you can not get this kind of object");
                return default;
            }
            else
            {
                path = $"{paths[typeof(T)]}{_fileName}.json";
            }

            string jsonStr;
            try
            {
                jsonStr = System.IO.File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Debug.LogError("this file does not exist");
                return default;
                //throw;
            }

            try
            {
                something = JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch (Exception)
            {
                Debug.LogError("json file format error");
                //throw;
                return default;
            }

            return something;
        }

        //class end
    }



    public enum GhostType
    {
        Noramal, Strong
    }
    [Serializable]
    public class GhostData
    {
        public float speed;
        public GhostType type;
        public GhostData()
        {
            speed = 0.0f;
            type = GhostType.Noramal;
        }
    }

    [Serializable]
    public class MacManData
    {
        public float speed;
        public MacManData()
        {
            speed = 0.0f;
        }
    }

    [Serializable]
    public class PillData
    {
        public PillType type;
        public PillData()
        {
            type = PillType.Normal;
        }
    }

    [Serializable]
    public class RecordData
    {
        public string name;
        public int score;
        public int levelCount;
        public string whenItRecord;
        public RecordData(string _name, int _score = 0, int _levelCount = 0, string _whenItRecord = "a")
        {
            name = _name;
            score = _score;
            levelCount = _levelCount;
            whenItRecord = _whenItRecord;
        }
    }

    // namespace end
}
