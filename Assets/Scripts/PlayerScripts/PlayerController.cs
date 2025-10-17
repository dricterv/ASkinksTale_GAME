using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Base")]
    public Rigidbody2D rb;
    //public float speed;
    //public float rollDist;
    //public float rollTime;
    public GameObject spriteGO;
    public GameObject attackPoint;
    private Torch torch;
    public GameObject projectilePrefab;
    public InventoryItem quill;

    //private bool isTorching;


    public bool isRolling;
    
   


    private float hori;
    private float vert;
    private int moveDir;
    //private bool lockVert;
    //private bool lockHori;

    [Header("Grabbing")]
    public float grabDist = 1;
    public LayerMask pushableLayer;
    private RaycastHit2D grabHit;
    [Header("Interacting")]
    public float interactDist = 1;
    public LayerMask interactableLayer;
    private RaycastHit2D interactHit;
    [Header("Other")]
    public bool blocking;
    private Animator anim;
    private PlayerState playerState;
    public Collider2D grabLeft;
    public Collider2D grabRight;
    public Collider2D grabUp;
    public Collider2D grabDown;
    public Transform grabPoints;


    //private Vector2 facing = new Vector2();


    void Start()
    {
        StatsManager.Instance.canGrabMove = true;
        anim = GetComponent<Animator>();
        isRolling = false;
        //isTorching = false;
        StatsManager.Instance.lockHori = false;
        StatsManager.Instance.lockVert = false;
        torch = GetComponent<Torch>();
        StatsManager.Instance.facing = new Vector2(0, -1);
        StatsManager.Instance.lockFace = false;
        attackPoint.transform.localPosition = StatsManager.Instance.facing;
        anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
        anim.SetFloat("yFacing", StatsManager.Instance.facing.y);

        StatsManager.Instance.isMoving = false;

    }

    void FixedUpdate()
    {
        
        Move();
        grabPoints.position = transform.position;
    }
    void Update()
    {
        
        
        if(Input.GetButtonDown("Item1") && GameManager.Instance.uiManager.inventoryOpen == false)
        {
            switch (StatsManager.Instance.equippedItemOne)
            {
                case EquippedItem.BaseFork:

                    Attack();
                    //Debug.Log("fork");
                    break;

                case EquippedItem.BaseShield:

                    Block();
                   
                    break;

                case EquippedItem.MatchStick:

                    UseTorch();
                    //Debug.Log("torch");

                    break;

                case EquippedItem.SpearThrower:

                    if(InventoryManager.Instance.inventory[6].thisItem == quill)
                    {
                        StartShoot();
                    }
                    //Debug.Log("Shoot");

                    break;


            }

            
        }
        
        if(Input.GetButtonDown("Item2") && GameManager.Instance.uiManager.inventoryOpen == false)
        {
            switch (StatsManager.Instance.equippedItemTwo)
            {
                case EquippedItem.BaseFork:

                    Attack();

                    Debug.Log("fork");
                    break;

                case EquippedItem.BaseShield:

                    Block();
                   
                    break;

                case EquippedItem.MatchStick:

                    UseTorch();
                    Debug.Log("torch");

                    break;

                case EquippedItem.SpearThrower:

                    if(InventoryManager.Instance.inventory[6].thisItem == quill)
                    {
                        StartShoot();
                    }

                    break;


            }
        }
        if (Input.GetButton("Item1") != true && playerState == PlayerState.Blocking && StatsManager.Instance.equippedItemOne == EquippedItem.BaseShield)
                    {
                        StatsManager.Instance.blocking = false;
                        StatsManager.Instance.lockFace = false;
                        Facing();

                    }
        if (Input.GetButton("Item2") != true && playerState == PlayerState.Blocking && StatsManager.Instance.equippedItemTwo == EquippedItem.BaseShield)
                    {
                        StatsManager.Instance.blocking = false;
                        StatsManager.Instance.lockFace = false;
                        Facing();

                    }
        
        
        if(Input.GetButtonDown("Global"))
        {   
            if (playerState != PlayerState.Attacking && playerState != PlayerState.Torching && playerState != PlayerState.Shooting && GameManager.Instance.uiManager.inventoryOpen == false)
            {
                Collider2D interactHit = Physics2D.OverlapBox(this.transform.position, new Vector2(2,2), 0, interactableLayer);//Physics2D.Raycast(this.transform.position, StatsManager.Instance.facing, interactDist, interactableLayer);
                grabHit = Physics2D.Raycast(this.transform.position, StatsManager.Instance.facing, grabDist, pushableLayer);
                if(interactHit != null)
                Debug.Log(interactHit.gameObject.name);

                //Debug.Log("w");
                if (interactHit != null && playerState != PlayerState.Blocking && playerState != PlayerState.Rolling)
                {
                    if (GameManager.Instance.DialogueManager.isDialogueActive == true && GameManager.Instance.DialogueManager.isButtonActive == false)
                    {
                        GameManager.Instance.DialogueManager.AdvanceDialogue();
                        //Debug.Log("input 1");
                    }
                    else if(GameManager.Instance.DialogueManager.isDialogueActive == false && GameManager.Instance.DialogueManager.isButtonActive == false && GameManager.Instance.DialogueManager.CanStartDialogue())
                    {
                        interactHit.GetComponent<Dialogue>().CheckForNewConversation();
                        GameManager.Instance.DialogueManager.StartDialogue(interactHit.GetComponent<Dialogue>().currentConversation, interactHit.GetComponent<Dialogue>());
                        //Debug.Log("input 2");
                    }
                }

                else if (grabHit.collider != null && playerState != PlayerState.Blocking && GameManager.Instance.DialogueManager.isDialogueActive != true)
                {
                    Grab();

                }

                else if((Mathf.Abs(hori) > 0 || Mathf.Abs(vert) > 0) && isRolling == false && playerState != PlayerState.Grabbing && GameManager.Instance.DialogueManager.isDialogueActive != true)
                {
                    Roll();
                }
            }
        }
        if((Input.GetButton("Global") == false) && StatsManager.Instance.isMoving == false && (StatsManager.Instance.lockHori == true || StatsManager.Instance.lockVert == true) && (playerState == PlayerState.Grabbing || playerState == PlayerState.Pushing || playerState == PlayerState.Pushing))
        {
            
        
            
            
            grabLeft.enabled = false;
            grabRight.enabled = false;
            grabUp.enabled = false;
            grabDown.enabled = false;
                /* if (hit.collider != null)
                 {
                     hit.collider.gameObject.GetComponent<Pushable>().EndPush();
                 }*/
            StatsManager.Instance.lockHori = false;
            StatsManager.Instance.lockVert = false;

            StatsManager.Instance.lockFace = false;
            Facing();
        }
        

    }


    public void Move()
    {
        //Debug.Log(StatsManager.Instance.blocking);
         hori = Input.GetAxisRaw("Horizontal");
         vert = Input.GetAxisRaw("Vertical");

        if (GameManager.Instance.DialogueManager != null && GameManager.Instance.DialogueManager.isDialogueActive == true)
        {
            hori = 0;
            vert = 0;
        }
        //rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.speed;
        //Debug.Log("h: " + hori);
        // Debug.Log("v: " + vert);
        // Debug.Log("goin: " + hori + " " + vert);
        if (Mathf.Abs(hori) > Mathf.Abs(vert))
         {
             //Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
             if(StatsManager.Instance.lockFace == false)
             {
                 StatsManager.Instance.facing = new Vector2(hori, 0);
                 attackPoint.transform.localPosition = StatsManager.Instance.facing;
                anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
             }
             else if (StatsManager.Instance.lockFace == true)
             {
                 StatsManager.Instance.facing = StatsManager.Instance.lockFacing;
                 attackPoint.transform.localPosition = StatsManager.Instance.facing;
                if (StatsManager.Instance.lockVert == true)
                {
                    anim.SetFloat("xFacing", 0);
                    anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
                }
                else if (StatsManager.Instance.lockHori == true)
                {
                    anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                    anim.SetFloat("yFacing", 0);
                }
                else
                {
                    anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                    anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
                }
                
                

            }
             // StatsManager.Instance.facing = new Vector2(hori, 0);
             // attackPoint.transform.localPosition = StatsManager.Instance.facing;

         }
         else if (Mathf.Abs(vert) > Mathf.Abs(hori))
         {
             if (StatsManager.Instance.lockFace == false)
             {
                 StatsManager.Instance.facing = new Vector2(0, vert);
                 attackPoint.transform.localPosition = StatsManager.Instance.facing;
                anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
             }
             else if (StatsManager.Instance.lockFace == true)
             {
                 StatsManager.Instance.facing = StatsManager.Instance.lockFacing;
                 attackPoint.transform.localPosition = StatsManager.Instance.facing;
                if (StatsManager.Instance.lockVert == true)
                {
                    anim.SetFloat("xFacing", 0);
                    anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
                }
                else if (StatsManager.Instance.lockHori == true)
                {
                    anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                    anim.SetFloat("yFacing", 0);
                }
                else
                {
                    anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                    anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
                }



            }
             //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
             // StatsManager.Instance.facing = new Vector2(0, vert);
             //attackPoint.transform.localPosition = StatsManager.Instance.facing;
             //Debug.Log("v: " + vert);

         }
        
        //Debug.Log("h: " + hori);
        //Debug.Log("v: " + vert);
        // Debug.Log("facing: " + StatsManager.Instance.facing);
        
        if (isRolling == true)
        {
            rb.velocity = StatsManager.Instance.facing * StatsManager.Instance.rollDist;

        }
        else if (playerState == PlayerState.Attacking || playerState == PlayerState.Torching || playerState == PlayerState.Shooting)
        {
            rb.velocity = Vector2.zero;
        }
        else if (StatsManager.Instance.lockHori == true)
        {
            
           
            if (hori == 0 && StatsManager.Instance.isMoving == false)
            {
                ChangeState(PlayerState.Grabbing);
                anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                anim.SetFloat("yFacing", 0);
                rb.velocity = Vector2.zero;
                moveDir = 0;
            }
            else if (hori > 0 && StatsManager.Instance.isMoving == false)
            {


                //rb.MovePosition(transform.position + new Vector3(hori, 0, 0) * StatsManager.Instance.dragSpeed * Time.fixedDeltaTime);
                //rb.velocity = new Vector2(hori, 0) * StatsManager.Instance.dragSpeed;
                if(StatsManager.Instance.canGrabMove == true)
                {
                    moveDir = 1;
                }
                else
                {
                    moveDir = 0;
                    rb.velocity = Vector2.zero;
                }
                StartCoroutine(GrabMoveTimer());
                //rb.velocity = Vector3.Lerp(transform.position, new Vector3(hori, 0, 0), 100);

                if (StatsManager.Instance.facing.x == 1)
                {
                    ChangeState(PlayerState.Pushing);
                }
                else if (StatsManager.Instance.facing.x == -1)
                {
                    ChangeState(PlayerState.Pulling);
                }
            }
            else if (hori < 0 && StatsManager.Instance.isMoving == false)
            {
                //rb.velocity = new Vector2(moveDirl, 0) * StatsManager.Instance.dragSpeed;
                //rb.MovePosition(transform.position + new Vector3(hori, 0, 0) * StatsManager.Instance.dragSpeed * Time.fixedDeltaTime);
                if (StatsManager.Instance.canGrabMove == true)
                {
                    moveDir = -1;
                }
                else
                {
                    moveDir = 0;
                    rb.velocity = Vector2.zero;
                }
                StartCoroutine(GrabMoveTimer());

                if (StatsManager.Instance.facing.x == -1)
                {
                    ChangeState(PlayerState.Pushing);
                }
                else if (StatsManager.Instance.facing.x == 1)
                {
                    ChangeState(PlayerState.Pulling);
                }
            }

            rb.velocity = new Vector2(moveDir, 0) * StatsManager.Instance.dragSpeed * Time.fixedDeltaTime;

        }
        else if (StatsManager.Instance.lockVert == true)
        {
            if (vert == 0 && StatsManager.Instance.isMoving == false)
            {
                ChangeState(PlayerState.Grabbing);
                anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
                anim.SetFloat("xFacing", 0);
                moveDir = 0;
                rb.velocity = Vector2.zero;
            }
            else if (vert > 0 && StatsManager.Instance.isMoving == false)
            {

                if (StatsManager.Instance.canGrabMove == true)
                {
                    moveDir = 1;
                }
                else
                {
                    moveDir = 0;
                    rb.velocity = Vector2.zero;
                }
                StartCoroutine(GrabMoveTimer());

                if (StatsManager.Instance.facing.y == 1)
                {
                    ChangeState(PlayerState.Pushing);
                }
                else if (StatsManager.Instance.facing.y == -1)
                {
                    ChangeState(PlayerState.Pulling);
                }
            }
            else if (vert < 0 && StatsManager.Instance.isMoving == false)
            {
                if (StatsManager.Instance.canGrabMove == true)
                {
                    moveDir = -1;
                }
                else
                {
                    moveDir = 0;
                    rb.velocity = Vector2.zero;
                }
                StartCoroutine(GrabMoveTimer());

                if (StatsManager.Instance.facing.y == -1)
                {
                    ChangeState(PlayerState.Pushing);
                }
                else if (StatsManager.Instance.facing.y == 1)
                {
                    ChangeState(PlayerState.Pulling);
                }
            }
            rb.velocity = new Vector2(0, moveDir) * StatsManager.Instance.dragSpeed * Time.fixedDeltaTime;

        }
        else if (hori > 0 || vert > 0 || hori < 0 || vert < 0)
        {
            if (StatsManager.Instance.blocking == true)
            {
                ChangeState(PlayerState.Blocking);
                rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.dragSpeed * Time.fixedDeltaTime;
            }
            else
            {
                ChangeState(PlayerState.Walking);
                rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.speed;
            }

        }
        else
        {
            if (StatsManager.Instance.blocking == true)
            {
                ChangeState(PlayerState.Blocking);
                rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.dragSpeed * Time.fixedDeltaTime;
            }
            else
            {
                ChangeState(PlayerState.Idle);
                rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.speed;
            }



        }

    }
    public void Roll()
    {
        //this is only for visualisation of rolling
        //spriteGO.transform.Rotate(new Vector3(0, 0, 90));

        //roll start
        // Debug.Log("Roll Start");
        // float hori = Input.GetAxisRaw("Horizontal");
        // float vert = Input.GetAxisRaw("Vertical");


        ChangeState(PlayerState.Rolling);
        isRolling = true;
        StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
        StatsManager.Instance.lockFace = true;
        //rb.velocity = StatsManager.Instance.facing * StatsManager.Instance.rollDist;
        // Debug.Log("Roll Mid");
        StartCoroutine(RollTimer());
       // Debug.Log("Roll End");

    }
    public void Interact(GameObject interactGO)
    {
        interactGO.GetComponent<Interactable>().Interact();
    }
    public void Grab()
    {
        if (Input.GetButtonDown("Global"))
        {
            if(StatsManager.Instance.facing == new Vector2(0, -1))
            {
                grabDown.enabled = true;
            }
            if (StatsManager.Instance.facing == new Vector2(0, 1))
            {
                grabUp.enabled = true;
            }
            if (StatsManager.Instance.facing == new Vector2(-1, 0))
            {
                grabLeft.enabled = true;
            }
            if (StatsManager.Instance.facing == new Vector2(1, 0))
            {
                grabRight.enabled = true;
            }
            // instead do if facing(x) xtriggerGO.grab(), return coll of 
           /* hit = Physics2D.Raycast(this.transform.position, StatsManager.Instance.facing, grabDist, pushableLayer);
            if (hit.collider != null)
            {
                ChangeState(PlayerState.Grabbing);

                //StatsManager.Instance.lockFace = true;
                // hit.collider.gameObject.GetComponent<Pushable>().StartPush();
                if (hit.collider.gameObject.GetComponent<Pushable>().lockX == true)
                {
                    if(StatsManager.Instance.facing == new Vector2(0, -1) || StatsManager.Instance.facing == new Vector2(0, 1))
                    {
                        StatsManager.Instance.lockFace = true;
                        StatsManager.Instance.lockVert = true;
                        StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                        hit.collider.gameObject.GetComponent<Pushable>().StartPush();
                    }
                    else if(hit.collider.gameObject.GetComponent<Pushable>().lockY == true)
                    {
                        if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                        {
                            StatsManager.Instance.lockFace = true;
                            StatsManager.Instance.lockHori = true;
                            StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                            hit.collider.gameObject.GetComponent<Pushable>().StartPush();
                        }
                    }
                }
              
                else if (hit.collider.gameObject.GetComponent<Pushable>().lockY == true)
                {
                    if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                    {
                        StatsManager.Instance.lockFace = true;
                        StatsManager.Instance.lockHori = true;
                        StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                        hit.collider.gameObject.GetComponent<Pushable>().StartPush();
                    }
                   // lockHori = true;
                  //  StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                }
               

            }*/

        }
        if ((Input.GetButton("Global") == false) && StatsManager.Instance.isMoving == false && (StatsManager.Instance.lockHori == true || StatsManager.Instance.lockVert == true))
        {
            
            
                grabLeft.enabled = false;
                grabRight.enabled = false;
                grabUp.enabled = false;
                grabDown.enabled = false;
                /* if (hit.collider != null)
                 {
                     hit.collider.gameObject.GetComponent<Pushable>().EndPush();
                 }*/
                StatsManager.Instance.lockHori = false;
                StatsManager.Instance.lockVert = false;

                StatsManager.Instance.lockFace = false;
                Facing();
            
            
        }
    }
    public void Facing()
    {
        hori = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");
        //Debug.Log(hori + " : " + vert);
        if (StatsManager.Instance.facing ==  new Vector2(-1, 0) && hori == 1)
        {
            //Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
            StatsManager.Instance.facing = new Vector2(hori, 0);
            attackPoint.transform.localPosition = StatsManager.Instance.facing;
            anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
            anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
            
            
            // StatsManager.Instance.facing = new Vector2(hori, 0);
            // attackPoint.transform.localPosition = StatsManager.Instance.facing;

        }
        else if (StatsManager.Instance.facing == new Vector2(1, 0) && hori == -1)
        {
            if (StatsManager.Instance.lockFace == false)
            {
                StatsManager.Instance.facing = new Vector2(-1, 0);
                attackPoint.transform.localPosition = StatsManager.Instance.facing;
                anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
            }
            
            //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
            // StatsManager.Instance.facing = new Vector2(0, vert);
            //attackPoint.transform.localPosition = StatsManager.Instance.facing;
            //Debug.Log("v: " + vert);

        }
        else if (StatsManager.Instance.facing == new Vector2(0, -1) && vert == 1)
        {
            StatsManager.Instance.facing = new Vector2(0, 1);
            attackPoint.transform.localPosition = StatsManager.Instance.facing;
            anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
            anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
        }
        else if (StatsManager.Instance.facing == new Vector2(0, 1) && vert == -1)
        {
            StatsManager.Instance.facing = new Vector2(0, -1);
            attackPoint.transform.localPosition = StatsManager.Instance.facing;
            anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
            anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
        }
       
    }

    public void Block()
    {
        /*
        if (Input.GetKeyDown(KeyCode.K) && playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing)
        {
            StatsManager.Instance.blocking = true;
            StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
            //if(StatsManager.Instance.lockFacing =)
            StatsManager.Instance.lockFace = true;
            


        } */
        if (playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing )
        {

           

            StatsManager.Instance.blocking = true;
            StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
            StatsManager.Instance.lockFace = true;


        }
       

    }
    public void Attack()
    {
        if (playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing && playerState != PlayerState.Torching && playerState != PlayerState.Attacking && playerState != PlayerState.Shooting)
        {
            StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
            StatsManager.Instance.lockFace = true;
            StatsManager.Instance.blocking = false;
            ChangeState(PlayerState.Attacking);

        }
    }
    public void EndAttack()
    {
        StatsManager.Instance.lockFace = false;
        StatsManager.Instance.blocking = false;
        Facing();
        ChangeState(PlayerState.Idle);
    }


    IEnumerator RollTimer( )
    {
        yield return new WaitForSeconds(StatsManager.Instance.rollTime);
        isRolling = false;
        StatsManager.Instance.lockFace = false;
        
        Facing();
        //Debug.Log("Roll Timer F");
        //this is only for visualisation of rolling

    }
    
    IEnumerator GrabMoveTimer()
    {
        StatsManager.Instance.isMoving = true;
        yield return new WaitForSeconds(1f);
        StatsManager.Instance.isMoving = false;
       

    }
    

    private void UseTorch()
    {
       if(playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing && playerState != PlayerState.Attacking && playerState != PlayerState.Torching && playerState != PlayerState.Shooting)
       {
        
            StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
            StatsManager.Instance.lockFace = true;

            StatsManager.Instance.blocking = false;
            ChangeState(PlayerState.Torching);
        }
 
    }
    public void LightTorch()
    {
        torch.LightOnFire();
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Pushable")
        {

        }
    }
    public void StartShoot()
    {
        if (playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing && playerState != PlayerState.Torching && playerState != PlayerState.Attacking && playerState != PlayerState.Shooting)
        {
            StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
            StatsManager.Instance.lockFace = true;
            StatsManager.Instance.blocking = false;
            ChangeState(PlayerState.Shooting);

        }
    }
    public void EndShoot()
    {
        StatsManager.Instance.lockFace = false;
        StatsManager.Instance.blocking = false;
        Facing();
        ChangeState(PlayerState.Idle);
    }
    public void Shoot()
    {
        Debug.Log("w");
        Projectile projectile = Instantiate(projectilePrefab, attackPoint.transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.direction = StatsManager.Instance.facing.normalized;
        // shootTimer = shootCooldown;
    }

    public void ChangeState(PlayerState newState)
    {
        //exit current animation
        if (playerState == PlayerState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (playerState == PlayerState.Walking)
        {
            anim.SetBool("isWalking", false);
        }
        else if (playerState == PlayerState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        else if (playerState == PlayerState.Rolling)
        {
            anim.SetBool("isRolling", false);
        }
        else if (playerState == PlayerState.Blocking)
        {
            anim.SetBool("isBlocking", false);
        }
        else if (playerState == PlayerState.Grabbing)
        {
            anim.SetBool("isGrabbing", false);
        }
        else if (playerState == PlayerState.Torching)
        {
            anim.SetBool("isTorching", false);
        }
        else if (playerState == PlayerState.Pushing)
        {
            anim.SetBool("isPushing", false);
        }
        else if (playerState == PlayerState.Pulling)
        {
            anim.SetBool("isPulling", false);
        }
        else if (playerState == PlayerState.Shooting)
        {
            anim.SetBool("isShooting", false);
        }

        //update current state

        playerState = newState;

        //enter current animation

        if (playerState == PlayerState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (playerState == PlayerState.Walking)
        {
            anim.SetBool("isWalking", true);
        }
        else if (playerState == PlayerState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else if (playerState == PlayerState.Rolling)
        {
            anim.SetBool("isRolling", true);
        }
        else if (playerState == PlayerState.Blocking)
        {
            anim.SetBool("isBlocking", true);
        }
        else if (playerState == PlayerState.Grabbing)
        {
            anim.SetBool("isGrabbing", true);
        }
        else if (playerState == PlayerState.Torching)
        {
            anim.SetBool("isTorching", true);
        }
        else if (playerState == PlayerState.Pushing)
        {
            anim.SetBool("isPushing", true);
        }
        else if (playerState == PlayerState.Pulling)
        {
            anim.SetBool("isPulling", true);
        }
        else if (playerState == PlayerState.Shooting)
        {
            anim.SetBool("isShooting", true);
        }

    }
    
}
public enum PlayerState
{
    Idle,
    Walking,
    Attacking,
    KnockedBack,
    Blocking,
    Rolling,
    Grabbing,
    Pushing,
    Pulling,
    Torching,
    Shooting,
}
