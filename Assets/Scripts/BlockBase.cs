using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class BlockBase : MonoBehaviour
    {
        private bool isClicked = false;
        public bool isLocked = false;
        public ParticleSystem clickSys;
        private Rigidbody2D weight;
        public int level;
        public float speed = 1.0f;
        private void Awake()
        {
            GameManager.Instance.onLevelChange += Lock;
            weight = gameObject.GetComponent<Rigidbody2D>();
        }
        // Update is called once per frame
        void FixedUpdate()
        {
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
            
        }
        private void Lock(int y) //this will only work forward, needs to be edited.
        {
            if (y == level)
                isLocked = false;
            if (y > level)
                isLocked = true;
        }
        void StateChange()
        {
            if (GameManager.Instance.isEditing && !GameManager.Instance.isGrabbing && !isClicked)
            {
                GameManager.Instance.isGrabbing = true;
                isClicked = true;
                if (isLocked)
                {
                    isClicked = false;
                }
                switch (isClicked)
                {
                    case true:
                        weight.mass = 0.0001f;
                        clickSys.Play();
                        break;
                    case false:
                        weight.mass = 10000f;
                        clickSys.Stop();
                        break;
                }
                return;
            }
            if (GameManager.Instance.isEditing && GameManager.Instance.isGrabbing && isClicked)
            {
                GameManager.Instance.isGrabbing = false;
                isClicked = false;
                if (isLocked)
                {
                    isClicked = false;
                }
                switch (isClicked)
                {
                    case true:
                        weight.mass = 0.0001f;
                        clickSys.Play();
                        break;
                    case false:
                        weight.mass = 10000f;
                        clickSys.Stop();
                        break;
                }
                return;
            }
            
        }
        private void OnMouseDown()
        {
            StateChange();
        }
    }
}
