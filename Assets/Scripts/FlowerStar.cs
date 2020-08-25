﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class FlowerStar : MonoBehaviour
    {
        private bool collected = false;
        public AudioSource sound;
        private bool collectable = true;
        public Transform followLoc;
        public Transform home;
        public int level;
        public float speed = 1.0f;
        public float followDist = 1.0f;
        private void Awake()
        {
            GameManager.Instance.onLevelChange += Lock;
        }
        // Update is called once per frame
        void FixedUpdate()
        {
            //MOVE TO LOCATION OR MOVE TO PLAYER
            switch (collected)
            {
                case true:
                    if (Vector2.Distance(transform.position, followLoc.position) > followDist)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, followLoc.position, speed*Time.deltaTime);
                    }
                    break;
                case false:
                    
                        transform.position = Vector2.MoveTowards(transform.position, home.position, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, home.position) <= 0.1f)
                    {
                        collectable = true;
                    }
                        break;
            }

        }
        private void Lock(int y) //this will only work forward, needs to be edited.
        {
            //SWAP BOOL TO MOVE TO HOME LOCATION

            if (y == level)
                collected = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !collected && collectable)
            {
                sound.Play();
                collectable = false;
                collected = true;
                GameManager.Instance.LevelChange();
            }
        }
    }
}
