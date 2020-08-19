using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance; //static camera for use in other scripts
        [HideInInspector] public CameraShakeEffect cameraShake; //camera shake effect

        Transform player; //Player position
        Transform CamEditPOS; //edit cam position
        Camera self;

        public Vector2 offset; //Camera offset by player position
        public float cameraYPosMin, cameraYPosMax; //Camera position clamp
        public float smoothSpeed;

        void SingletonInit()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Awake()
        {
            SingletonInit();
        }

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform; //Find player in scene
            CamEditPOS = GameObject.Find("CamEditPOS").transform;
            cameraShake = GetComponent<CameraShakeEffect>();
            self = GetComponent<Camera>();
        }

        public void FixedUpdate()
        {
            if (GameManager.Instance.isEditing == false)
            {
                Vector3 newPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -1); //Local vector get player position

                self.fieldOfView = Mathf.Lerp(self.fieldOfView, 85, smoothSpeed * Time.deltaTime);

                transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime); //Set camera position smooth

                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, cameraYPosMin, cameraYPosMax), transform.position.z); //make clamp
            }
            else
            {
                Vector3 newPos = new Vector3(CamEditPOS.position.x + offset.x, CamEditPOS.position.y + offset.y, -1); //Local vector get player position

                self.fieldOfView = Mathf.Lerp(self.fieldOfView, 165, smoothSpeed * Time.deltaTime);

                transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime); //Set camera position smooth

                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, cameraYPosMin, cameraYPosMax), transform.position.z); //make clamp
            }
            
        }
    }
}