using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cats3.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource[] _destroyCats;
        public AudioSource _backMusic;

        private void Start()
        {
            if(PlayerPrefs.HasKey("����"))
            {
                if(PlayerPrefs.GetInt("����") == 0)
                {
                    _backMusic.Play();
                    _backMusic.volume = 0;
                }
                else 
                {
                    _backMusic.Play();
                    _backMusic.volume = 1;

                }
            }
            else
            {
                _backMusic.Play();
                _backMusic.volume = 1;
            }
        }

        public void Volume()
        {
            if (PlayerPrefs.HasKey("����"))
            {
                if (PlayerPrefs.GetInt("����") == 0)
                {
                    _backMusic.volume = 0;
                }
                else
                {
                    _backMusic.volume = 1;

                }
            }
        }

        public void PlayRandomDestroyCats()
        {
            if (PlayerPrefs.HasKey("����"))
            {
                if (PlayerPrefs.GetInt("����") == 1)
                {
                    //��������� �����
                    int sourceToPlay = Random.Range(0, _destroyCats.Length);
                    //�������� ���
                    _destroyCats[sourceToPlay].Play();
                }
            }
            else
            {
                //��������� �����
                int sourceToPlay = Random.Range(0, _destroyCats.Length);
                //�������� ���
                _destroyCats[sourceToPlay].Play();
            }
           
        }
    }
}
