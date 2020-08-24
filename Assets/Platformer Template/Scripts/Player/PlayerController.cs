using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour, IController
    {
        //Components
        Stats stats;
        AnimatorController animatorController;
        PlayerCombat playerCombat;

        public InputManager inputManager = new InputManager();

        [Header("Variables")]
        public float moveSpeed;
        public float jumpForce;
        public float rollForce;

        Transform playerSprite;
        [HideInInspector] public Rigidbody2D rigid2D { get; set; }

        [SerializeField] bool isGrounded;
        [HideInInspector] public bool isAttacking { get; set; }

        [HideInInspector] public bool onLadder;
        bool isClimping;

        private void Start()
        {
            stats = GetComponent<Stats>();
            rigid2D = GetComponent<Rigidbody2D>();
            playerSprite = transform.GetChild(0);

            animatorController = GetComponentInChildren<AnimatorController>();

            inputManager.inputConfig.UpdateDictionary(); //

            //add Death event
            stats.OnDeath += Death;
        }

        public void FixedUpdate()
        {
            if (GameManager.Instance.isGame && !GameManager.Instance.isPause) //check Game status
            {
                if (!GameManager.Instance.isEditing && !GameManager.Instance.isEvent)
                    Move();
                //LadderClimb();
                GroundCheck();
            }
        }

        private void Update()
        {
            if (GameManager.Instance.isGame && !GameManager.Instance.isPause && !GameManager.Instance.isEvent)
            {
                if (!GameManager.Instance.isEditing)
                {
                    Rotation();
                    Jump();
                    
                }
                Roll();
                Duck();
                Rewind();
                //Attack();
                Animation();
                Pause();
            }
            if (GameManager.Instance.isEvent && GameManager.Instance.isGame)
            {
                AdvanceDialogue();
            }

        }
        public void AdvanceDialogue()
        {
            if(inputManager.Jump)
                GameManager.Instance.DisplayNextSentence();
        }
        public void Rewind()
        {
            
            if (inputManager.Rewind && GameManager.Instance.currLev > 0)
            {
                GameManager.Instance.LevelReset();
            }
        }
        public void Duck()
        {
            if (inputManager.Duck && isGrounded)
            {
                animatorController.SetBool("Duck", true);
            }
            else
            {
                animatorController.SetBool("Duck", false);
            }
        }
        //Move method
        public void Move()
        {
            if (!isAttacking)
                transform.Translate(new Vector2(inputManager.Horizontal * moveSpeed * Time.deltaTime, 0));
        }

        //Rotation method
        public void Rotation()
        {
            if (inputManager.Horizontal != 0) //if player any horizontal side move
            {
                if (inputManager.Horizontal < 0)
                    playerSprite.localScale = new Vector3(-1, 1, 1);
                else
                    playerSprite.localScale = new Vector3(1, 1, 1);
            }

        }

        //Roll method
        void Roll()
        {
            if (inputManager.Roll && isGrounded)
            {
                GameManager.Instance.isEditing = !GameManager.Instance.isEditing;
                isClimping = !isClimping;
                animatorController.SetBool("Climping",isClimping);
            }
                
            //if (isGrounded && inputManager.Roll && !isAttacking && !isClimping) //Check for availability rollback
            //{
            //    if (Mathf.Round(inputManager.Horizontal) != 0)
            //    {
            //        animatorController.SetTrigger("Roll");
            //        rigid2D.velocity = Vector2.right * inputManager.Horizontal * rollForce;
            //    }
            //}
        }

        //Jump method
        void Jump()
        {
            if (inputManager.Jump && isGrounded && !isAttacking && !isClimping)
            {
                animatorController.SetTrigger("Jump");
                rigid2D.velocity = Vector2.up * jumpForce;
            }

        }
        void Pause()
        {
            if (inputManager.Pause)
            {
                UIMan.Instance.Pauser();
            }
        }
        //Attack method
        public void Attack()
        {
            if (isGrounded && (inputManager.MeleeAttack || inputManager.RangeAttack) && !isAttacking && !isClimping) 
            {
                isAttacking = true; //attack status
                if (inputManager.MeleeAttack)
                {
                    animatorController.SetBool("MeleeAttack", isAttacking); //Set animator bool
                }
                else if (inputManager.RangeAttack)
                {
                    animatorController.SetBool("RangeAttack", isAttacking);
                }
            }
        }

        //Animator method
        public void Animation()
        {
            if (!isClimping) //Lader check
                if (inputManager.Horizontal != 0)
                    animatorController.SetBool("Move", true);
                else
                    animatorController.SetBool("Move", false);
            else
            {
                animatorController.SetBool("Move", false);
            }

            animatorController.SetBool("isGrounded", isGrounded);
        }

        void LadderClimb()
        {
            //if (onLadder) //check lader status
            //{
            //    if (inputManager.Vertical != 0)
            //    {
            //        //isClimping = true; 
            //        rigid2D.velocity = Vector2.up * inputManager.Vertical * moveSpeed; //move up or down
            //    }
            //    else
            //    {
            //        rigid2D.velocity = Vector2.zero; 
            //    }

            //}
            //else //if leave ladder
            //{
            //    isClimping = false;
            //}

            //if (onLadder)
            //    rigid2D.gravityScale = 0;
            //else
            //    rigid2D.gravityScale = 1;
        }

        //Death event
        public void Death()
        {
            animatorController.SetTrigger("Death");
            GameManager.Instance.GameOver(); //set game state to game over
        }

        //Check ground 
        void GroundCheck()
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, 1f);

            if (raycastHit2D.collider != null)
            {
                if (Vector2.Distance(transform.position, raycastHit2D.point) <= raycastHit2D.distance) //if raycast 
                {
                    if (!isGrounded)
                        animatorController.SetTrigger("Landing");
                    isGrounded = true; //is ground 
                    GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                    Debug.DrawLine(transform.position, raycastHit2D.point); //draw line only in editor
                }
            }
            else
            {
                isGrounded = false; 
            }

        }
        public void Die()
        {
            Debug.Log("died");
        }


    }
}
