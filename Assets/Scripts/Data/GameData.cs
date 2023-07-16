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
            //������� �������� ��������������, ����� ������ �����
            BinaryFormatter formatter = new BinaryFormatter();
            //�������� �� ��������� � ����
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);
            //�������� ����� ������ ��� ����������
            SaveData data = new SaveData();
            data = _saveData;
            //���������� ������ ��� �������
            formatter.Serialize(file, data);
            file.Close();
            Debug.Log("���������");
        }

        public void Load()
        {
            //�������� ������� ����� ����������
            if(File.Exists(Application.persistentDataPath + "/player.dat"))
            {
                //������� �������� ������
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
                _saveData = formatter.Deserialize(file) as SaveData;
                file.Close();
                Debug.Log("��������");
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
