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
    public Collider2D kinCol;
    public Collider2D dynCol;
    public NumberCounter counter;
    public Flamable flame;
    public bool torchOn;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(kinCol, dynCol);
        originalColor = flamableSpriteRenderer.color;
       // anim = GetComponent<Animator>();
        if (isOnFire == true)
        {
            //colourToggle = true;
           // anim.SetBool("isOnFire", true);
        }
        else
        {
            colourToggle = false;

            // anim.SetBool("isOnFire", false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isOnFire == true && colourToggle == false)
        {
            flamableSpriteRenderer.color = goalColor;
            colourToggle = true;
        }
        else if (isOnFire == false && colourToggle == true)
        {
            colourToggle = false;

            flamableSpriteRenderer.color = originalColor;
        }
        /*
        if (isOnFire == true && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            timer = timerMax;
            FireOff();
        }*/
    }
    public void SetOnFire()
    {
        if (counter != null && isOnFire == false)
        {
            counter.AddToCount(1);

        }
        isOnFire = true;
        
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
        //anim.SetBool("isOnFire", true);
        //Debug.Log("OffFire");
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.transform.parent.tag == "Flamable" && coll.gameObject.GetComponentInParent<Flamable>().isFlamable == true && coll.gameObject.GetComponentInParent<Flamable>().isOnFire == false && isOnFire == true)
        {
            Debug.Log("lighting");
            coll.gameObject.GetComponentInParent<Flamable>().SetOnFire();
        }
    }

}
