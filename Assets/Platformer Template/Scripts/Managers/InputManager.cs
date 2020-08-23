using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer
{
    [Serializable]
    public class InputManager
    {
        public InputConfig inputConfig;

        //Here you setup controll settings

#if UNITY_ANDROID || UNITY_IOS
        [HideInInspector] public float Horizontal { get { return VirtualJoystick.joystickMoveDir.x; } }
        [HideInInspector] public float Vertical { get { return VirtualJoystick.joystickMoveDir.z; } }

        [HideInInspector] public bool Jump { get { return MobileController.action["Jump"]; } }
        [HideInInspector] public bool Roll { get { return MobileController.action["Roll"]; } }
        [HideInInspector] public bool Duck { get { return MobileController.action["Duck"]); } }
        [HideInInspector] public bool Rewind { get { return MobileController.action["Rewind"]); } }
        [HideInInspector] public bool MeleeAttack { get { return MobileController.action["MeleeAttack"]; } }
        [HideInInspector] public bool RangeAttack { get { return MobileController.action["RangeAttack"]; } }
        [HideInInspector] public bool Pause { get { return MobileController.action["Pause"]; } }

#elif UNITY_STANDALONE
        [HideInInspector] public float Horizontal { get { return Input.GetAxis("Horizontal"); } }
        [HideInInspector] public float Vertical { get { return Input.GetAxis("Vertical"); } }

        [HideInInspector] public bool Jump { get { return Input.GetKeyDown(inputConfig.Keys["Jump"]); } }
        [HideInInspector] public bool Roll { get { return Input.GetKeyDown(inputConfig.Keys["Roll"]); } }
        [HideInInspector] public bool Duck { get { return Input.GetKey(inputConfig.Keys["Duck"]); } }
        [HideInInspector] public bool Rewind { get { return Input.GetKeyDown(inputConfig.Keys["Rewind"]); } }
        [HideInInspector] public bool MeleeAttack { get { return Input.GetKeyDown(inputConfig.Keys["MeleeAttack"]); } }
        [HideInInspector] public bool RangeAttack { get { return Input.GetKeyDown(inputConfig.Keys["RangeAttack"]); } }
        [HideInInspector] public bool Pause { get { return Input.GetKeyDown(inputConfig.Keys["Pause"]); } }

#endif

    }
}