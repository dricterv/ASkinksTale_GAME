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

    
    public PlayerController playerController;


    //StatsManager.Instance.facing
    // Start is called before the first frame update
    void Start()
    {
        pushed = false;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(pushed == true)
        {
            float hori = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");
            
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

    
    
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Grab")
        {
            EndPush();
        }
    }
    
}
