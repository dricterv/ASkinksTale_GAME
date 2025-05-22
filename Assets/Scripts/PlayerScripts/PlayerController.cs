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
    //private bool isTorching;


    public bool isRolling;

    private float hori;
    private float vert;

    //private bool lockVert;
    //private bool lockHori;

    [Header("Grabbing")]
    public float grabDist = 1;
    public LayerMask pushableLayer;
    private RaycastHit2D hit;
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

    }

    void FixedUpdate()
    {
            Move();
        grabPoints.position = transform.position;
    }
    void Update()
    {
        //Debug.Log(playerState);
        //Debug.Log(StatsManager.Instance.facing);

        if (playerState != PlayerState.Attacking && playerState != PlayerState.Torching)
        {
            
            Block();

            if(playerState != PlayerState.Rolling && playerState != PlayerState.Blocking )
            { 
                Grab();
                if(torch.enabled == true && playerState != PlayerState.Grabbing)
                {
                    UseTorch();

                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && (Mathf.Abs(hori) > 0 || Mathf.Abs(vert) > 0) && isRolling == false && playerState != PlayerState.Grabbing)
            {
                //   Debug.Log("Roll?");
                Roll();
            }
            if (Input.GetKeyDown(KeyCode.J) && playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing)
            {
                StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                StatsManager.Instance.lockFace = true;
                StatsManager.Instance.blocking = false;
                ChangeState(PlayerState.Attacking);
                
            }
        }
        
       
    }
   

    public void Move()
    {
        //Debug.Log(StatsManager.Instance.blocking);
         hori = Input.GetAxisRaw("Horizontal");
         vert = Input.GetAxisRaw("Vertical");
        

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
        else if (playerState == PlayerState.Attacking || playerState == PlayerState.Torching)
        {
            rb.velocity = Vector2.zero;
        }
        else if (StatsManager.Instance.lockHori == true)
        {
            if (hori == 0)
            {
                ChangeState(PlayerState.Grabbing);
                anim.SetFloat("xFacing", StatsManager.Instance.facing.x);
                anim.SetFloat("yFacing", 0);

                rb.velocity = Vector2.zero;
            }
            else if (hori > 0)
            {

                rb.velocity = new Vector2(hori, 0) * StatsManager.Instance.dragSpeed;


                if (StatsManager.Instance.facing.x == 1)
                {
                    ChangeState(PlayerState.Pushing);
                }
                else if (StatsManager.Instance.facing.x == -1)
                {
                    ChangeState(PlayerState.Pulling);
                }
            }
            else if (hori < 0)
            {
                rb.velocity = new Vector2(hori, 0) * StatsManager.Instance.dragSpeed;


                if (StatsManager.Instance.facing.x == -1)
                {
                    ChangeState(PlayerState.Pushing);
                }
                else if (StatsManager.Instance.facing.x == 1)
                {
                    ChangeState(PlayerState.Pulling);
                }
            }


        }
        else if (StatsManager.Instance.lockVert == true)
        {
            if (vert == 0)
            {
                ChangeState(PlayerState.Grabbing);
                anim.SetFloat("yFacing", StatsManager.Instance.facing.y);
                anim.SetFloat("xFacing", 0);

                rb.velocity = Vector2.zero;
            }
            else if (vert > 0)
            {

                rb.velocity = new Vector2(0, vert) * StatsManager.Instance.dragSpeed;


                if (StatsManager.Instance.facing.y == 1)
                {
                    ChangeState(PlayerState.Pushing);
                }
                else if (StatsManager.Instance.facing.y == -1)
                {
                    ChangeState(PlayerState.Pulling);
                }
            }
            else if (vert < 0)
            {
                rb.velocity = new Vector2(0, vert) * StatsManager.Instance.dragSpeed;


                if (StatsManager.Instance.facing.y == -1)
                {
                    ChangeState(PlayerState.Pushing);
                }
                else if (StatsManager.Instance.facing.y == 1)
                {
                    ChangeState(PlayerState.Pulling);
                }
            }

        }
        else if (hori > 0 || vert > 0 || hori < 0 || vert < 0)
        {
            if (StatsManager.Instance.blocking == true)
            {
                ChangeState(PlayerState.Blocking);
                rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.dragSpeed;
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
                rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.dragSpeed;
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

    public void Grab()
    {
        if ((Input.GetKeyDown(KeyCode.L)))
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
        if ((Input.GetKeyUp(KeyCode.L)))
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
        if (Input.GetKeyDown(KeyCode.K) && playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing)
        {
            StatsManager.Instance.blocking = true;
            StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
            //if(StatsManager.Instance.lockFacing =)
            StatsManager.Instance.lockFace = true;
            


        }
        if (Input.GetKey(KeyCode.K) && playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing )
        {

           

            StatsManager.Instance.blocking = true;
            StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
            StatsManager.Instance.lockFace = true;


        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            StatsManager.Instance.blocking = false;
            StatsManager.Instance.lockFace = false;
            Facing();

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

    private void UseTorch()
    {
       if(Input.GetKeyDown(KeyCode.I) && playerState != PlayerState.Rolling && playerState != PlayerState.Grabbing)
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
}
