using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EVENTCOMPENDIUM : MonoBehaviour
    {

        //THIS CONTAINS ALL EVENTS, IT WILL BE A MESS, JUST GIVE IT OPEN ACCESS TO WHATEVER
        public void StartPanDown()
        {
            GameManager.Instance.isEvent = false;
        }
    }
}
