using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    //public float speed;
    //public float rollDist;
    //public float rollTime;
    public GameObject spriteGO;
    public GameObject attackPoint;
    public bool isRolling;

    private float hori;
    private float vert;
    

    //private Vector2 facing = new Vector2();
    public PlayerCombat playerCombat;

    void Start()
    {
       
        isRolling = false;
        StatsManager.Instance.facing = new Vector2(0, -1);
        attackPoint.transform.localPosition = StatsManager.Instance.facing;
    }

    void FixedUpdate()
    {
            Move();
    }
    void Update()
    {
        
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
            rb.velocity = new Vector2(hori, vert) * StatsManager.Instance.speed;
            //Debug.Log("h: " + hori);
           // Debug.Log("v: " + vert);
           // Debug.Log("goin: " + hori + " " + vert);
           if (Mathf.Abs(hori) > Mathf.Abs(vert))
            {
                //Debug.Log(Mathf.Abs(hori) + " : " + (vert - .1f));
                StatsManager.Instance.facing = new Vector2(hori, 0);
                attackPoint.transform.localPosition = StatsManager.Instance.facing;
            }
            else if (Mathf.Abs(vert) > Mathf.Abs(hori))
            {
                //Debug.Log(Mathf.Abs(vert) + " : " + (hori - .1f));
                StatsManager.Instance.facing = new Vector2(0, vert);
                attackPoint.transform.localPosition = StatsManager.Instance.facing;
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
