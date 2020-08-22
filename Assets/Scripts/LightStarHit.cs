using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class LightStarHit : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Block")
            {
                collision.gameObject.GetComponent<Block>().Fade();
            }
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().Die();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Block")
            {
                collision.gameObject.GetComponent<Block>().FadeIn();
            }
        }
    }
}
