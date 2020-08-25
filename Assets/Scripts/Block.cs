using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class Block : MonoBehaviour
    {
        private bool isClicked = false;
        public bool isLocked = false;
        enum fadeState { Solid, Faded, FadingOut, FadingIn };
        fadeState fader = fadeState.Solid;
        public ParticleSystem clickSys;
        public SpriteRenderer face;
        public Collider2D body;
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
            
            switch (fader)
            {
                case fadeState.Solid:
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
                    break;
                case fadeState.Faded:

                    break;
                case fadeState.FadingIn:
                    face.color = new Color(1, 1, 1, Mathf.MoveTowards(face.color.a, 1, 0.2f * Time.deltaTime));
                    if (face.color.a >= 1)
                    {
                        fader = fadeState.Solid;
                        StateChange();
                        body.enabled = true;
                    }
                    break;
                case fadeState.FadingOut:
                    face.color = new Color(1, 1, 1, Mathf.MoveTowards(face.color.a, 0.1f, 0.4f * Time.deltaTime));
                    if (face.color.a <= 0.1f)
                    {
                        fader = fadeState.Faded;
                        StateChange();
                        body.enabled = false;
                    }
                    break;
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
                isClicked = true;
                GameManager.Instance.isGrabbing = true;
                if (isLocked || fader == fadeState.Faded)
                {
                    isClicked = false;
                }
                else
                {
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
                }
                return;
            }
            if (GameManager.Instance.isEditing && GameManager.Instance.isGrabbing && isClicked)
            {
                isClicked = false;
                GameManager.Instance.isGrabbing = false;
                if (isLocked || fader == fadeState.Faded)
                {
                    isClicked = false;
                }
                else
                {
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
                }
                return;
            }

        }
        private void OnMouseDown()
        {
            StateChange();
        }
        public void Fade()
        {
            fader = fadeState.FadingOut;
            
        }
        public void FadeIn()
        {
            fader = fadeState.FadingIn;
            
        }
    }
}
