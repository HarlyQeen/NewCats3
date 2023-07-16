using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Cats3.UI;

namespace Cats3.Data
{
    [Serializable]
    public class SaveData
    {

        public List<bool> _isActive;
        public List<int> _highScores;
        public List<int> _stars;
    }

    public class GameData : MonoBehaviour
    {
        public static GameData _gameData;
        public SaveData _saveData;

        private void Awake()
        {
            if(_gameData == null)
            {
                DontDestroyOnLoad(this.gameObject);
                _gameData = this;
            }
            else 
            {
                Destroy(this.gameObject);
            }
            Load();
        }

        private void Start()
        {
           
        }

        public void Save()
        {
            //сделать бинарное форматирование, чтобы читать файлы
            BinaryFormatter formatter = new BinaryFormatter();
            //Направим из программы в файл
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);
            //Создадим копию данных для сохранения
            SaveData data = new SaveData();
            data = _saveData;
            //Актуальные данные для сохрана
            formatter.Serialize(file, data);
            file.Close();
            Debug.Log("Сохранено");
        }

        public void Load()
        {
            //Проверим наличие файла сохранения
            if(File.Exists(Application.persistentDataPath + "/player.dat"))
            {
                //сделаем двоичный формат
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
                _saveData = formatter.Deserialize(file) as SaveData;
                file.Close();
                Debug.Log("Загрузка");
            }
        }

        private void OnApplicationQuit()
        {
            Save();
        }


        private void OnDisable()
        {
            Save();
        }
    }
}
