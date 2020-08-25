using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class Event2 : MonoBehaviour
    {
        public string TrigName;
        public bool OneShot = false;
        private EVENTCOMPENDIUM eventer;
        private void Start()
        {
            eventer = GameObject.Find("Managers").GetComponent<EVENTCOMPENDIUM>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                eventer.Invoke(TrigName, 0f);
                if (OneShot)
                {
                    Destroy(this);
                }
            }

        }
    }
}