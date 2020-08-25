using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{

    public class DialogueTrigger : MonoBehaviour
    {

        public Dialogue dialogue;
        public bool oneTime = false;
        public bool oneTimeOBJ = false;
        public bool Examine = true;
        public bool Active = true;
        public void TriggerDialogue()
        {
            if (Active)
            {
                GameManager.Instance.StartDialogue(dialogue);
                if (oneTime)
                    Destroy(gameObject);
            }

        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!Examine)
            {
                if (other.tag == "Player")
                {
                    TriggerDialogue();
                    if (oneTime)
                        Destroy(gameObject);
                    if (oneTimeOBJ)
                        Destroy(this);
                }
            }

        }
    }
}
