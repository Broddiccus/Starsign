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
        public Transform EventPOS; //position for events;
        Camera self;

        public Vector2 offset; //Camera offset by player position
        public float cameraYPosMin, cameraYPosMax; //Camera position clamp
        public float smoothSpeed;
        public float playZoom;
        public float editZoom;

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
            player = GameObject.Find("CAMFOLLOWPOS").transform; //Find player in scene
            CamEditPOS = GameObject.Find("CamEditPOS").transform;
            cameraShake = GetComponent<CameraShakeEffect>();
            self = GetComponent<Camera>();
        }

        public void FixedUpdate()
        {
            if (!GameManager.Instance.isPause)
            {

                if (!GameManager.Instance.isEditing && !GameManager.Instance.isEvent)
                {

                    Vector3 newPos = new Vector3(player.position.x + offset.x, player.position.y + offset.y, -1); //Local vector get player position

                    self.orthographicSize = Mathf.Lerp(self.orthographicSize, playZoom, smoothSpeed * Time.deltaTime);

                    transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime); //Set camera position smooth

                    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, cameraYPosMin, cameraYPosMax), transform.position.z); //make clamp
                }
                else if (GameManager.Instance.isEditing && !GameManager.Instance.isEvent)
                {
                    Vector3 newPos = new Vector3(CamEditPOS.position.x + offset.x, CamEditPOS.position.y + offset.y, -1); //Local vector get player position

                    self.orthographicSize = Mathf.Lerp(self.orthographicSize, editZoom, smoothSpeed * Time.deltaTime);

                    transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime); //Set camera position smooth

                    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, cameraYPosMin, cameraYPosMax), transform.position.z); //make clamp
                }
                else if (GameManager.Instance.isEvent)
                {
                    EventPOS = GameManager.Instance.camMoveLoc;

                    Vector3 newPos = new Vector3(EventPOS.position.x + offset.x, EventPOS.position.y + offset.y, -1); //Local vector get player position

                    //self.orthographicSize = Mathf.Lerp(self.orthographicSize, 4.0f, smoothSpeed * Time.deltaTime);

                    transform.position = Vector3.Lerp(transform.position, newPos, smoothSpeed * Time.deltaTime); //Set camera position smooth

                    transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, cameraYPosMin, cameraYPosMax), transform.position.z); //make clamp
                }
            }

        }
    }
}