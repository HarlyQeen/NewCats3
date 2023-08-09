using Cats3.Base;
using Cats3.Data;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Cats3.UI
{
    public class Shop : MonoBehaviour
    {
        private ShopData dataShop = new ShopData();
        public Image[] _img;
        public Sprite[] _spr;
        [SerializeField]
        private List<Button> _but;
        private bool isBuying;
        private int _buyNum;
        public Text _shopText;
        private GameData _gameData;
        private int _allScores;
        private int _coast;
        

        [System.Serializable]
        public class ShopData
        {
            public List<int> num;
            public int money;
            public List<int> coast;
        }

        

        public void Save()
        {
            File.WriteAllText(Application.streamingAssetsPath + "Shop.json", JsonUtility.ToJson(dataShop));
        }

        public void Load()
        {
            dataShop = JsonUtility.FromJson<ShopData>(File.ReadAllText(Application.streamingAssetsPath + "Shop.json"));
        }

        private void Update()
        {
            SetText();
            
        }

        private void OnEnable()
        {
            _gameData = FindObjectOfType<GameData>();
            Load();
            
            if (dataShop != null && _gameData != null)
            {
                LoadData();
                _buyNum = CheckFurni();
                _coast = CheckCoast();
                for (int i = 0; i < dataShop.num.Count; i++)
                {
                    _img[dataShop.num[i]].sprite = _spr[dataShop.num[i]];
                }
                
            }
        }

        void LoadData()
        {
            _allScores = CheckScores() - CheckCoast();

        }

        void SetText()
        {
            _shopText.text = "" + _allScores;
        }

        private int CheckScores()
        {
            int scores = 0;
            foreach (var score in _gameData._saveData._highScores)
            {
                scores += score;

            }
            return scores;
        }

        public int CheckCoast()
        {
            int coasts = 0;
            foreach (var coastes in dataShop.coast)
            {
                coasts += coastes;
            }
            return coasts;
        }

        public int CheckFurni()
        {
            int furnies = 0;
            foreach (var nums in dataShop.num)
            {
                furnies += nums;
            }
            return furnies;
        }

        public void BuyFurni1()
        {
            if (_allScores >= 20000 && isBuying == false)
            {
                _coast = 20000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                isBuying = true;
                _buyNum = 0;
                _img[_buyNum].sprite = _spr[_buyNum];
                dataShop.coast.Add(_coast);
                dataShop.num.Add(_buyNum);
                Save();

            }
            else
            {
                isBuying = false;
                _but[0].image.color = new Color(255, 0, 0, 1);
            }
        }
        public void BuyFurni2()
        {
            if (_allScores >= 25000 && isBuying == false)
            {
                _coast = 25000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 1;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.coast.Add(_coast);
                dataShop.num.Add(_buyNum);
                Save();
            }
            else
            {
                isBuying = false;
                _but[1].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni3()
        {
            if (_allScores >= 30000 && isBuying == false)
            {
                _coast = 30000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 2;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[2].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni4()
        {
            if (_allScores >= 60000 && isBuying == false)
            {
                _coast = 60000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 3;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[3].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni5()
        {
            if (_allScores >= 70000 && isBuying == false)
            {
                _coast = 70000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 4;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[4].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni6()
        {
            if (_allScores >= 100000 && isBuying == false)
            {
                _coast = 100000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 5;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[5].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni7()
        {
            if (_allScores >= 110000 && isBuying == false)
            {
                _coast = 110000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 6;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[6].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni8()
        {
            if (_allScores >= 120000 && isBuying == false)
            {
                _coast = 120000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 7;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[7].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni9()
        {
            if (_allScores >= 200000 && isBuying == false)
            {
                _coast = 200000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 8;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[8].image.color = new Color(255, 0, 0, 1);
            }

        }

        public void BuyFurni10()
        {
            if (_allScores >= 220000 && isBuying == false)
            {
                _coast = 220000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 9;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[9].image.color = new Color(255, 0, 0, 1);
            }

        }

        public void BuyFurni11()
        {
            if (_allScores >= 250000 && isBuying == false)
            {
                _coast = 250000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 10;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[10].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni12()
        {
            if (_allScores >= 150000 && isBuying == false)
            {
                _coast = 150000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 11;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[11].image.color = new Color(255, 0, 0, 1);
            }
        }
        public void BuyFurni13()
        {
            if (_allScores >= 200000 && isBuying == false)
            {
                _coast = 200000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 12;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[12].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni14()
        {
            if (_allScores >= 50000 && isBuying == false)
            {
                _coast = 50000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 13;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[13].image.color = new Color(255, 0, 0, 1);
            }
        }

        public void BuyFurni15()
        {
            if (_allScores >= 100000 && isBuying == false)
            {
                _coast = 100000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 14;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[14].image.color = new Color(255, 0, 0, 1);
            }
        }
        public void BuyFurni16()
        {
            if (_allScores >= 110000 && isBuying == false)
            {
                _coast = 110000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 15;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[15].image.color = new Color(255, 0, 0, 1);
            }


        }

        public void BuyFurni17()
        {
            if (_allScores >= 120000 && isBuying == false)
            {
                _coast = 120000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 16;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[16].image.color = new Color(255, 0, 0, 1);
            }

        }

        public void BuyFurni18()
        {
            if (_allScores >= 150000 && isBuying == false)
            {
                _coast = 150000;
                _allScores -= _coast;
                dataShop.money = _allScores;
                _buyNum = 17;
                _img[_buyNum].sprite = _spr[_buyNum];
                isBuying = true;
                dataShop.num.Add(_buyNum);
                dataShop.coast.Add(_coast);
                Save();
            }
            else
            {
                isBuying = false;
                _but[17].image.color = new Color(255, 0, 0, 1);
            }
        }
    }
}