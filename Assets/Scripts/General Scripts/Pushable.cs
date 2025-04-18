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
    public Collider2D kinCol;
    public Collider2D dynCol;



    //StatsManager.Instance.facing
    // Start is called before the first frame update
    void Start()
    {
        pushed = false;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        Physics2D.IgnoreCollision(kinCol, dynCol);
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
                    rb.velocity = new Vector2(0, vert) * StatsManager.Instance.dragSpeed;
                }
                else if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                {
                    rb.velocity = new Vector2(hori, 0) * StatsManager.Instance.dragSpeed;
                }
                    
            }
            else if (lockX == true && lockY == false)
            {
                rb.velocity = new Vector2(0, vert) * StatsManager.Instance.dragSpeed;
            }
            else if (lockY == true && lockX == false)
            {
                rb.velocity = new Vector2(hori, 0) * StatsManager.Instance.dragSpeed;
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
            pushed = true;       
        }
    }
    public void EndPush()
    {
        if (isPushable == true)
        {
            pushed = false;
            rb.velocity = Vector2.zero;
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            
        }
    } 

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && (Input.GetKey(KeyCode.L)))
        {
            rb.velocity = coll.gameObject.transform.position.normalized;
            Debug.Log("touch");
            
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            rb.velocity = Vector2.zero;
        }
    }
    */
}
