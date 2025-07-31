using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    public bool isPushable;
    private Rigidbody2D rb;
    public bool pushed;
    public bool lockX;
    public bool lockY;
    public GameObject playerGO;
    public Rigidbody2D playerRB;
    public LayerMask wallLayer;
    public LayerMask pushableLayer;
    public Flamable flame;
    public PlayerController playerController;


    //StatsManager.Instance.facing
    // Start is called before the first frame update
    void Start()
    {
        pushed = false;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        
        

    }

    void Update()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(pushed == true)
        {
            float hori = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");

            if (flame != null)
            {
                flame.CheckForTorch();
            }
            //Debug.Log(StatsManager.Instance.facing);
            // Debug.Log(-StatsManager.Instance.facing);
            //Debug.Log(hori + " : " + vert);

            RaycastHit2D hit1 = Physics2D.BoxCast(transform.position, new Vector2(0.15f, 0.15f), 0f, StatsManager.Instance.facing, 2f, wallLayer);
            RaycastHit2D hit2 = Physics2D.BoxCast(transform.position, new Vector2(0.15f, 0.15f), 0f, -StatsManager.Instance.facing, 4f, wallLayer);
            RaycastHit2D hit3 = Physics2D.BoxCast(transform.position, new Vector2(0.15f, 0.15f), 0f, StatsManager.Instance.facing, 2f, pushableLayer);
            RaycastHit2D hit4 = Physics2D.BoxCast(transform.position, new Vector2(0.15f, 0.15f), 0f, -StatsManager.Instance.facing, 4f, pushableLayer);
           
           if (hit1 == true)
           {
                RaycastHit1(hit1, hit2, hit3, hit4, hori, vert);
           }
           else if(hit2 == true)
           {
                RaycastHit2( hit2, hit3, hit4, hori, vert);
           }
           else if(hit3 == true)
           {
                RaycastHit3(hit3, hit4, hori, vert);
           }
           else if(hit4 == true)
           {
                RaycastHit4(hit4, hori, vert);
           }
           else
           {
                StatsManager.Instance.canGrabMove = true;
           }
            

            if (StatsManager.Instance.lockHori == false && StatsManager.Instance.lockVert == false)
            {
                EndPush();
            }
            if (lockX == true && lockY == true)
            {
                if (StatsManager.Instance.facing == new Vector2(0, -1) || StatsManager.Instance.facing == new Vector2(0, 1))
                {
                   // rb.velocity = new Vector2(0, vert) * StatsManager.Instance.dragSpeed;
                    rb.velocity = playerRB.velocity;
                }
                else if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                {
                    //rb.velocity = new Vector2(hori, 0) * StatsManager.Instance.dragSpeed;
                    rb.velocity = playerRB.velocity;

                }

            }
            else if (lockX == true && lockY == false)
            {
               // rb.velocity = new Vector2(0, vert) * StatsManager.Instance.dragSpeed;
                rb.velocity = playerRB.velocity;

            }
            else if (lockY == true && lockX == false)
            {
                // rb.velocity = new Vector2(hori, 0) * StatsManager.Instance.dragSpeed;
                rb.velocity = playerRB.velocity;
            }
           
            //playerGO.GetComponent<Rigidbody2D>().velocity
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    public void StartPush()
    {
        if(isPushable == true)
        {
            playerController.ChangeState(PlayerState.Grabbing);
            rb.bodyType = RigidbodyType2D.Dynamic;
            pushed = true;
            
        }
    }
    public void EndPush()
    {
        if (isPushable == true)
        {
            pushed = false;
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D coll)
    {
       // Debug.Log("push: " + coll.gameObject.name);
       // Debug.Log("push: " + coll.gameObject.tag);
       // Debug.Log("push: " + rb.bodyType);

        if (coll.gameObject.tag == "Grab" && isPushable == true)
        {
            
            

            //StatsManager.Instance.lockFace = true;
            // hit.collider.gameObject.GetComponent<Pushable>().StartPush();
            if (lockX == true)
            {
                if (StatsManager.Instance.facing == new Vector2(0, -1) || StatsManager.Instance.facing == new Vector2(0, 1))
                {
                    StatsManager.Instance.lockFace = true;
                    StatsManager.Instance.lockVert = true;
                    StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                    StartPush();
                }
                else if (lockY == true)
                {
                    if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                    {
                        StatsManager.Instance.lockFace = true;
                        StatsManager.Instance.lockHori = true;
                        StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                        StartPush();
                    }
                }
            }

            else if (lockY == true)
            {
                if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                {
                    StatsManager.Instance.lockFace = true;
                    StatsManager.Instance.lockHori = true;
                    StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                    StartPush();
                }

            }
        }
    } 

    public void RaycastHit1(RaycastHit2D hit1, RaycastHit2D hit2, RaycastHit2D hit3,RaycastHit2D hit4, float hori, float vert)
    {
        
        
        Debug.Log(hit1.collider.gameObject.name + " : a");
        Debug.Log(StatsManager.Instance.facing);
        Debug.Log(hori + " h:v " + vert);
            
        if (StatsManager.Instance.facing == new Vector2(hori, 0))
        {
            Debug.Log(" : 1h1");
            StatsManager.Instance.canGrabMove = false;
            Debug.Log(" : 1h2");
        }

        else if (StatsManager.Instance.facing == new Vector2(0, vert))
        {
            Debug.Log(" : 1v1");
            StatsManager.Instance.canGrabMove = false;
            Debug.Log(" : 1v2");
        }
        else if (hit2 == true)
        {
            RaycastHit2(hit2, hit3, hit4, hori, vert);
        }
        else if (hit3 == true)
        {
            RaycastHit3(hit3, hit4, hori, vert);
        }
        else if (hit4 == true)
        {
            RaycastHit4( hit4, hori, vert);
        }
        else
        {
            Debug.Log(" : 2a1");
            StatsManager.Instance.canGrabMove = true;
            Debug.Log(" : 2a2");
        }
        
    }
    public void RaycastHit2(RaycastHit2D hit2, RaycastHit2D hit3,RaycastHit2D hit4, float hori, float vert)
    {
        if (-StatsManager.Instance.facing == new Vector2(hori, 0))
        {
            Debug.Log(" : 1h1");
            StatsManager.Instance.canGrabMove = false;
            Debug.Log(" : 1h2");
        }

        else if (-StatsManager.Instance.facing == new Vector2(0, vert))
        {
            Debug.Log(" : 1v1");
            StatsManager.Instance.canGrabMove = false;
            Debug.Log(" : 1v2");
        }
        else if (hit3 == true)
        {
            RaycastHit3(hit3, hit4, hori, vert);
        }
        else if (hit4 == true)
        {
            RaycastHit4( hit4, hori, vert);
        }
        else
        {
            Debug.Log(" : 2a1");
            StatsManager.Instance.canGrabMove = true;
            Debug.Log(" : 2a2");
        }
    }
    public void RaycastHit3(RaycastHit2D hit3,RaycastHit2D hit4, float hori, float vert)
    {
        if (StatsManager.Instance.facing == new Vector2(hori, 0))
        {
            Debug.Log(" : 1h1");
            StatsManager.Instance.canGrabMove = false;
            Debug.Log(" : 1h2");
        }

        else if (StatsManager.Instance.facing == new Vector2(0, vert))
        {
            Debug.Log(" : 1v1");
            StatsManager.Instance.canGrabMove = false;
            Debug.Log(" : 1v2");
        }

        else if (hit4 == true)
        {
            RaycastHit4( hit4, hori, vert);
        }
        else
        {
            Debug.Log(" : 2a1");
            StatsManager.Instance.canGrabMove = true;
            Debug.Log(" : 2a2");
        }
    }
    public void RaycastHit4(RaycastHit2D hit4, float hori, float vert)
    {
        if (-StatsManager.Instance.facing == new Vector2(hori, 0))
        {
            Debug.Log(" : 1h1");
            StatsManager.Instance.canGrabMove = false;
            Debug.Log(" : 1h2");
        }

        else if (-StatsManager.Instance.facing == new Vector2(0, vert))
        {
            Debug.Log(" : 1v1");
            StatsManager.Instance.canGrabMove = false;
            Debug.Log(" : 1v2");
        }
        else
        {
            Debug.Log(" : 2a1");
            StatsManager.Instance.canGrabMove = true;
            Debug.Log(" : 2a2");
        }
    }
    
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Grab")
        {
           // EndPush();
        }
    }
    
}
