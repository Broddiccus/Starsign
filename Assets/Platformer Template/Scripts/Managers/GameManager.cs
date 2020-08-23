using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public bool isEvent;

        [Header("Gameplay")]
        public Level[] LevelOBJS;
        public int currLev = 0;
        public event Action<int> onLevelChange;

        [Header("Dialogue")]
        public Transform camMoveLoc;
        private Queue<string> names;
        private Queue<string> sentences;
        private Queue<Sprite> portraits;
        public Animator popUp;
        public Text nameUI;
        public Text sentenceUI;
        public Image portraitUI;
        public EVENTCOMPENDIUM events;
        private int[] eventLoc;
        private string[] eventName;


        void SingletonInit()
        {
            if (Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }
        private void OnMouseUp()
        {
            
        }
        private void Awake()
        {
            
            SingletonInit();
            sentences = new Queue<string>();
            portraits = new Queue<Sprite>();
            names = new Queue<string>();
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>(); //find player data component
        }

        private void Start()
        {
            //StartGame(); //when scene load game'll start
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
        public void StartDialogue(Dialogue dialogue)
        {
            isEvent = true;
            camMoveLoc = dialogue.camMoveLoc;
            popUp.SetBool("Talking", true);
            sentences.Clear();
            portraits.Clear();
            names.Clear();
            eventLoc = dialogue.eventloc;
            eventName = dialogue.eventname;
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            foreach (string name in dialogue.names)
            {
                names.Enqueue(name);
            }
            foreach (Sprite portrait in dialogue.portraits)
            {
                portraits.Enqueue(portrait);
            }
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            for (int i = 0; i < eventLoc.Length; i++)
            {

                if (eventLoc[i] == 0)
                {
                    events.Invoke(eventName[i],0.0f);
                }
                eventLoc[i]--;
            }

            if (sentences.Count == 0)
            {

                EndDialogue();
                return;
            }
            string name = names.Dequeue();
            string sentence = sentences.Dequeue();
            Sprite portrait = portraits.Dequeue();

            nameUI.text = name;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            portraitUI.sprite = portrait;
        }
        IEnumerator TypeSentence(string sentence)
        {
            sentenceUI.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                sentenceUI.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            isEvent = false;
            popUp.SetBool("Talking", false);
            //end dialogue
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