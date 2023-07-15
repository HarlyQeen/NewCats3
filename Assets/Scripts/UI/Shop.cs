
using Cats3.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cats3.UI
{
    public class Shop : MonoBehaviour
    {
        public Image[] _img;
        public Sprite[] _spr;


        public void BuyFurni1()
        {
            if (Coins._allScores > 20000)
            {
                Coins._allScores -= 20000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[0].sprite = _spr[0];

            }
        }
        public void BuyFurni2()
        {
            if (Coins._allScores > 25000)
            {
                Coins._allScores -= 25000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[1].sprite = _spr[1];

            }
        }

        public void BuyFurni3()
        {
            if (Coins._allScores > 30000)
            {
                Coins._allScores -= 30000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[2].sprite = _spr[2];

            }
        }

        public void BuyFurni4()
        {
            if (Coins._allScores > 60000)
            {
                Coins._allScores -= 60000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[3].sprite = _spr[3];

            }
        }

        public void BuyFurni5()
        {
            if (Coins._allScores > 70000)
            {
                Coins._allScores -= 70000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[4].sprite = _spr[4];

            }
        }

        public void BuyFurni6()
        {
            if (Coins._allScores > 100000)
            {
                Coins._allScores -= 100000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[5].sprite = _spr[5];

            }
        }

        public void BuyFurni7()
        {
            if (Coins._allScores > 110000)
            {
                Coins._allScores -= 110000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[6].sprite = _spr[6];

            }
        }

        public void BuyFurni8()
        {
            if (Coins._allScores > 120000)
            {
                Coins._allScores -= 120000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[7].sprite = _spr[7];

            }
        }

        public void BuyFurni9()
        {
            if (Coins._allScores > 200000)
            {
                Coins._allScores -= 200000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[8].sprite = _spr[8];

            }
        }

        public void BuyFurni10()
        {
                if (Coins._allScores > 220000)
                {
                    Coins._allScores -= 220000;
                    PlayerPrefs.SetInt("Money", Coins._allScores);
                    _img[9].sprite = _spr[9];
                }
        }

        public void BuyFurni11()
        {
            if (Coins._allScores > 250000)
            {
                Coins._allScores -= 250000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[10].sprite = _spr[10];
            }
        }

        public void BuyFurni12()
        {
            if (Coins._allScores > 150000)
            {
                Coins._allScores -= 150000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[11].sprite = _spr[11];
            }
        }
        public void BuyFurni13()
        {
            if (Coins._allScores > 200000)
            {
                Coins._allScores -= 200000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[12].sprite = _spr[12];
            }
        }

        public void BuyFurni14()
        {
            if (Coins._allScores > 50000)
            {
                Coins._allScores -= 50000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[13].sprite = _spr[13];
            }
        }

        public void BuyFurni15()
        {
            if (Coins._allScores > 100000)
            {
                Coins._allScores -= 100000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[14].sprite = _spr[14];
            }
        }
        public void BuyFurni16()
        {
            if (Coins._allScores > 110000)
            {
                Coins._allScores -= 110000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[15].sprite = _spr[15];
            }
        }

        public void BuyFurni17()
        {
            if (Coins._allScores > 120000)
            {
                Coins._allScores -= 120000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[16].sprite = _spr[16];
            }
        }

        public void BuyFurni18()
        {
            if (Coins._allScores > 150000)
            {
                Coins._allScores -= 150000;
                PlayerPrefs.SetInt("Money", Coins._allScores);
                _img[17].sprite = _spr[17];
            }
        }

    }
}
