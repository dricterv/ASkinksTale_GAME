using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    public bool isFlamable;
    public bool isOnFire;
    private Animator anim;
    public SpriteRenderer flamableSpriteRenderer;
    private bool colourToggle;
    private float timer;
    //private float timerMax = 1f;
    public Color originalColor;
    public Color goalColor;
    
    public NumberCounter counter;
    public NumberCounter counter2;
    public Flamable flame;
    public LayerMask flameLayer;
    public bool torchOn;

    // Start is called before the first frame update
    void Start()
    {
        
        originalColor = flamableSpriteRenderer.color;
        anim = GetComponent<Animator>();
        if (isOnFire == true)
        {
            //colourToggle = true;
            anim.SetBool("isOnFire", true);
        }
        else
        {
            //colourToggle = false;

            anim.SetBool("isOnFire", false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
      
    }
    public void SetOnFire()
    {
        if (counter != null && isOnFire == false)
        {
            counter.AddToCount(1);

        }
        if (counter2 != null && isOnFire == false)
        {
            counter2.AddToCount(1);
        }
        isOnFire = true;
        anim.SetBool("isOnFire", true);
        if (flame != null && torchOn == true)
        {
            flame.SetOnFire();
        }
        //anim.SetBool("isOnFire", true);
        Debug.Log("OnFire");
    }
    public void FireOff()
    {
        isOnFire = false;
        anim.SetBool("isOnFire", false);
        //anim.SetBool("isOnFire", true);
        //Debug.Log("OffFire");
    }
    /*void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.tag == "Flamable" && coll.gameObject.GetComponent<Flamable>().isFlamable == true && coll.gameObject.GetComponent<Flamable>().isOnFire == false && isOnFire == true)
        {
            Debug.Log("lighting");
            coll.gameObject.GetComponent<Flamable>().SetOnFire();
        }
    }*/
    public void CheckForTorch()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, StatsManager.Instance.facing, 1.5f, flameLayer);
        Debug.DrawRay(transform.position, StatsManager.Instance.facing * 1.5f, Color.white);

        if (hit1 == true && hit1.collider.gameObject.tag == "Flamable" && hit1.collider.gameObject.GetComponent<Flamable>().isFlamable == true && hit1.collider.gameObject.GetComponent<Flamable>().isOnFire == false && isOnFire == true)
        {
            Debug.Log("lighting");
            hit1.collider.gameObject.gameObject.GetComponent<Flamable>().SetOnFire();
        }
    }

}
