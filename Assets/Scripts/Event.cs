using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{
    public class Event : MonoBehaviour
    {
        public string TrigName;
        public bool OneShot = false;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.Instance.Invoke(TrigName, 0f);
                if (OneShot)
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
}
