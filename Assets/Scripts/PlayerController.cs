using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float rollDist;
    public float rollTime;
    public GameObject spriteGO;

    private float hori;
    private float vert;
    private bool isRolling;

    private Vector2 facing = new Vector2();

    void Start()
    {
        isRolling = false;
        facing = new Vector2(0, -1);
    }

    void FixedUpdate()
    {
            Move();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && (Mathf.Abs(hori) > 0 || Mathf.Abs(vert) > 0) && isRolling == false)
        {
            Debug.Log("Roll?");
            Roll();
        }
    }

    public void Move()
    {
         hori = Input.GetAxisRaw("Horizontal");
         vert = Input.GetAxisRaw("Vertical");
       
        if(isRolling == false)
        {
            rb.velocity = new Vector2(hori, vert) * speed;
            //Debug.Log("h: " + hori);
           // Debug.Log("v: " + vert);
           // Debug.Log("goin: " + hori + " " + vert);
           if (Mathf.Abs(hori) > Mathf.Abs(vert))
            {
                //Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
                facing = new Vector2(hori, 0);
            }
            else if (Mathf.Abs(vert) > Mathf.Abs(hori))
            {
                //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
                facing = new Vector2(0, vert);
                 //Debug.Log("v: " + vert);
                
            }
            //Debug.Log("h: " + hori);
            //Debug.Log("v: " + vert);
            Debug.Log("facing: " + facing);
        }

    }
    public void Roll()
    {
        //this is only for visualisation of rolling
        spriteGO.transform.Rotate(new Vector3(0, 0, 90));

        //roll start
        Debug.Log("Roll Start");
        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        

        isRolling = true;
        rb.velocity = facing * rollDist;
        Debug.Log("Roll Mid");
        StartCoroutine(RollTimer());
        Debug.Log("Roll End");

    }



    IEnumerator RollTimer( )
    {
        Debug.Log("Roll Timer S");

        yield return new WaitForSeconds(rollTime);
        rb.velocity = Vector2.zero;
        isRolling = false;
        Debug.Log("Roll Timer F");
        //this is only for visualisation of rolling
        spriteGO.transform.eulerAngles = new Vector3(0, 0, 0);

    }
}
