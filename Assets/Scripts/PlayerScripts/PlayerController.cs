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
    public bool isRolling;

    private float hori;
    private float vert;
    private bool lockVert;
    private bool lockHori;

    [Header("Grabbing")]
    public float grabDist = 1;
    public LayerMask pushableLayer;
    private RaycastHit2D hit;
    [Header("Other")]


    //private Vector2 facing = new Vector2();
    public PlayerCombat playerCombat;

    void Start()
    {
       
        isRolling = false;
        StatsManager.Instance.facing = new Vector2(0, -1);
        StatsManager.Instance.lockFace = false;
        attackPoint.transform.localPosition = StatsManager.Instance.facing;
    }

    void FixedUpdate()
    {
            Move();
    }
    void Update()
    {
        Grab();
        if (Input.GetKeyDown(KeyCode.Space) && (Mathf.Abs(hori) > 0 || Mathf.Abs(vert) > 0) && isRolling == false)
        {
         //   Debug.Log("Roll?");
            Roll();
        }
        if (Input.GetKeyDown(KeyCode.J) && playerCombat.enabled == true)
        {
            playerCombat.Attack();
        }
    }

    public void Move()
    {
         hori = Input.GetAxisRaw("Horizontal");
         vert = Input.GetAxisRaw("Vertical");
       
        if(isRolling == false)
        {
            if(lockHori == true)
            {
                rb.velocity = new Vector2(hori, 0) * StatsManager.Instance.dragSpeed;
            }
            else if (lockVert == true)
            {
                rb.velocity = new Vector2(0, vert) * StatsManager.Instance.dragSpeed;
            }
            else
            {
                rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.speed;
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
                }
                else if (StatsManager.Instance.lockFace == true)
                {
                    StatsManager.Instance.facing = StatsManager.Instance.lockFacing;
                    attackPoint.transform.localPosition = StatsManager.Instance.facing;
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
                }
                else if (StatsManager.Instance.lockFace == true)
                {
                    StatsManager.Instance.facing = StatsManager.Instance.lockFacing;
                    attackPoint.transform.localPosition = StatsManager.Instance.facing;
                }
                //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
               // StatsManager.Instance.facing = new Vector2(0, vert);
                //attackPoint.transform.localPosition = StatsManager.Instance.facing;
                //Debug.Log("v: " + vert);

            }
            //Debug.Log("h: " + hori);
            //Debug.Log("v: " + vert);
           // Debug.Log("facing: " + StatsManager.Instance.facing);
        }

    }
    public void Roll()
    {
        //this is only for visualisation of rolling
        spriteGO.transform.Rotate(new Vector3(0, 0, 90));

        //roll start
       // Debug.Log("Roll Start");
       // float hori = Input.GetAxisRaw("Horizontal");
       // float vert = Input.GetAxisRaw("Vertical");

        

        isRolling = true;
        rb.velocity = StatsManager.Instance.facing * StatsManager.Instance.rollDist;
       // Debug.Log("Roll Mid");
        StartCoroutine(RollTimer());
       // Debug.Log("Roll End");

    }

    public void Grab()
    {
        if ((Input.GetKeyDown(KeyCode.L)))
        {
            hit = Physics2D.Raycast(this.transform.position, StatsManager.Instance.facing, grabDist, pushableLayer);
            if (hit.collider != null)
            {
                //StatsManager.Instance.lockFace = true;
               // hit.collider.gameObject.GetComponent<Pushable>().StartPush();
                if(hit.collider.gameObject.GetComponent<Pushable>().lockX == true)
                {
                    if(StatsManager.Instance.facing == new Vector2(0, -1) || StatsManager.Instance.facing == new Vector2(0, 1))
                    {
                        StatsManager.Instance.lockFace = true;
                        lockVert = true;
                        StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                        hit.collider.gameObject.GetComponent<Pushable>().StartPush();
                    }
                    
                }
              
                else if (hit.collider.gameObject.GetComponent<Pushable>().lockY == true)
                {
                    if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                    {
                        StatsManager.Instance.lockFace = true;
                        lockHori = true;
                        StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                        hit.collider.gameObject.GetComponent<Pushable>().StartPush();
                    }
                   // lockHori = true;
                  //  StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                }
               

            }

        }
        if ((Input.GetKeyUp(KeyCode.L)))
        {

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Pushable>().EndPush();
            }
            lockHori= false;
            lockVert = false;
            StatsManager.Instance.lockFacing = Vector2.zero;
            StatsManager.Instance.lockFace = false;
        }
    }



    IEnumerator RollTimer( )
    {
       // Debug.Log("Roll Timer S");

        yield return new WaitForSeconds(StatsManager.Instance.rollTime);
        rb.velocity = Vector2.zero;
        isRolling = false;
        //Debug.Log("Roll Timer F");
        //this is only for visualisation of rolling
        spriteGO.transform.eulerAngles = new Vector3(0, 0, 0);

    }
}
