using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

       [HideInInspector] public Stats playerStats;

        [Header("Parameters")]
        public bool isGame;
        public bool isPause;
        public bool isEditing;

        [Header("Gameplay")]
        public Level[] LevelOBJS;
        public int currLev = 0;
        public event Action<int> onLevelChange;

        void SingletonInit()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Awake()
        {
            SingletonInit();

            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>(); //find player data component
        }

        private void Start()
        {
            StartGame(); //when scene load game'll start
        }
        public void LevelReset()
        {
            
            if (currLev > 0)
            {
                currLev--;
                foreach (GameObject x in LevelOBJS[currLev + 1].LevelOBJ)
                {
                    x.SetActive(false); //make more complicated later
                }
            }
            if (onLevelChange != null)
                onLevelChange(currLev);
        }
        public void LevelChange() //call this when
        {

            currLev++;
            if (currLev <= LevelOBJS.Length)
            {
                foreach (GameObject x in LevelOBJS[currLev].LevelOBJ)
                {
                    x.SetActive(true); //make more complicated later
                }
                
            }
            
            if (onLevelChange != null)
                onLevelChange(currLev);
        }

        public void StartGame()
        {
            isGame = true;
        }

        //Lose method
        public void GameOver()
        {
            isGame = false; //disable game
            UIManager.Instance.ChangeScreen(UIManager.ScreenState.Lose); //change screen to lose
        }

    }
}