using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Platformer
{

    public class StarClick : MonoBehaviour
    {
        public LightStar daddy;
        private void OnMouseDown()
        {
            daddy.StateChange();
        }
    }
}
