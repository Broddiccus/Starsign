using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class LightStar : MonoBehaviour
    {
        private bool isClicked = false;
        public bool isLocked = false;
        public Transform star;
        public Transform point1;
        public Transform point2;
        private Transform followpoint;
        private int dir = 2;
        public ParticleSystem clickSys;
        public int level;
        public float speed = 1.0f;
        private void Awake()
        {
            GameManager.Instance.onLevelChange += Lock;
            followpoint = point2;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            //IF NOT CLICKED MOVE BACK AND FORTH BETWEEN TWO POINTS
            //IF CLICKED STOP DOING THAT UNTIL PUT IN POSITION
            if (isClicked)
            {
                Vector3 mouseTemp = Input.mousePosition;
                mouseTemp.z = 1;
                transform.position = Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(mouseTemp), speed);
                if (!GameManager.Instance.isEditing)
                {
                    isClicked = false;
                    StateChange();
                }
            }
            else
            {
                star.position = Vector2.MoveTowards(star.position, followpoint.position, speed/3 * Time.deltaTime);
                if (Mathf.Approximately(Vector2.Distance(star.position, followpoint.position), 0.0f)){
                    switch (dir)
                    {
                        case 1:
                            dir = 2;
                            followpoint = point2;
                            break;
                        case 2:
                            dir = 1;
                            followpoint = point1;
                            break;
                    }
                }
            }

        }
        private void Lock(int y) //this will only work forward, needs to be edited.
        {
            Debug.Log(y);
            if (y == level)
                isLocked = false;
            if (y > level)
                isLocked = true;
        }
        public void StateChange()
        {
            if (GameManager.Instance.isEditing)
            {
                isClicked = !isClicked;
                if (isLocked)
                {
                    isClicked = false;
                }

            }
            switch (isClicked)
            {
                case true:
                    clickSys.Play();
                    break;
                case false:
                    clickSys.Stop();
                    break;
            }
        }
    }
}
