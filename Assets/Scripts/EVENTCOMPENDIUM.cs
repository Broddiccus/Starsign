using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Platformer
{
    public class EVENTCOMPENDIUM : MonoBehaviour
    {
        public CameraManager CameraMan;
        public AudioManager AudioMan;
        public Transform[] eventPanLoc;
        public daynight day;
        public GameObject Destroyer;
        public GameObject Princess;
        public SpriteRenderer PrincessFace;
        public Sprite[] PrincessRef;
        public Transform PrincessMover;
        private bool PrinmoveBool = false;
        public SpriteRenderer[] Faders;
        private bool FadeOut = false;
        private bool FadeBlack = false;
        public Image Blackout;
        public Dialogue endDia;
        //THIS CONTAINS ALL EVENTS, IT WILL BE A MESS, JUST GIVE IT OPEN ACCESS TO WHATEVER
        public void MusicPlayerOne()
        {
            AudioMan.Player(0);
        }
        public void MusicPlayerTwo()
        {
            AudioMan.Player(1);
        }
        public void MusicPlayerThree()
        {
            AudioMan.Player(2);
        }
        public void MusicPlayerFour()
        {
            AudioMan.Player(3);
        }
        public void MusicPlayerFive()
        {
            AudioMan.Player(4);
        }
        public void MusicPlayerSix()
        {
            AudioMan.Player(5);
        }
        public void MusicPlayerSeven()
        {
            AudioMan.Player(6);
        }
        public void MusicPlayerEight()
        {
            AudioMan.Player(7);
        }
        public void StartPanDown()
        {
            CameraMan.smoothSpeed = 0.5f;
            GameManager.Instance.camMoveLoc = eventPanLoc[0];
            
        }
        public void StartGame()
        {
            CameraMan.smoothSpeed = 5f;
            GameManager.Instance.isEvent = false;
        }
        public void GameplayEnd()
        {
            Destroyer.SetActive(false);
            GameManager.Instance.canRewind = false;
            GameManager.Instance.canEdit = false;
        }
        public void Sunrise()
        {
            day.FullDay();
        }
        public void Princess1()
        {
            CameraMan.smoothSpeed = 1f;
            GameManager.Instance.camMoveLoc = eventPanLoc[1];

        }
        public void PrincessShock()
        {
            PrincessFace.sprite = PrincessRef[0];
        }
        public void Princess2()
        {
            CameraMan.smoothSpeed = 0.5f;
            GameManager.Instance.camMoveLoc = eventPanLoc[1];

        }
        public void PrincessMove()
        {
            CameraMan.smoothSpeed = 1.0f;
            GameManager.Instance.camMoveLoc = eventPanLoc[0];
            PrincessFace.flipX = true;
            PrinmoveBool = true;
        }
        private void Update()
        {
            if (PrinmoveBool)
            {
                Princess.transform.position = Vector2.MoveTowards(Princess.transform.position, PrincessMover.position, 1.0f * Time.deltaTime);
            }
            if (FadeOut)
            {
                foreach(SpriteRenderer x in Faders)
                {
                    x.color = new Color(1, 1, 1, Mathf.MoveTowards(x.color.a, 0.0f, 0.1f));
                    if (x.color.a >= 0.0f)
                    {
                        FadeBlack = true;
                        FadeOut = false;
                    }
                }

            }
            if (FadeBlack)
            {
                Blackout.color = new Color(0, 0, 0, Mathf.MoveTowards(Blackout.color.a, 1.0f, 0.1f));
                if (Blackout.color.a >= 1.0f)
                {
                    FadeBlack = false;
                    GameManager.Instance.StartDialogue(endDia);
                }
            }
        }
        public void GameEnd()
        {
            FadeOut = true;
        }
        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
