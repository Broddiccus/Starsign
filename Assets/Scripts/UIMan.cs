using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Platformer
{

    public class UIMan : MonoBehaviour
    {
        public static UIMan Instance;
        public GameObject Title;
        public GameObject Credits;
        public GameObject Pause;

        private void Awake()
        {
            SingletonInit();
        }
        void SingletonInit()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void CreditStart()
        {
            Credits.SetActive(true);
            Title.SetActive(false);
        }
        public void TitleStart()
        {
            Credits.SetActive(false);
            Pause.SetActive(false);
            Title.SetActive(true);
        }
        public void Quit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        public void StartGame()
        {
            GameManager.Instance.isGame = true;
            Title.SetActive(false);
            //do the camera pan
        }
        public void Pauser()
        {
            Pause.SetActive(true);
            GameManager.Instance.isPause = true;
        }
        public void Unpauser()
        {
            Pause.SetActive(false);
            GameManager.Instance.isPause = false;
        }
    }
}
