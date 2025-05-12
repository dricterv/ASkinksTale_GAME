using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamable : MonoBehaviour
{
    public bool isFlamable;
    public bool isOnFire;
    private Animator anim;
    public SpriteRenderer flamableSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
       // anim = GetComponent<Animator>();
        if(isOnFire == true)
        {
           // anim.SetBool("isOnFire", true);
        }
        else
        {
           // anim.SetBool("isOnFire", false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isOnFire == true)
        {
            flamableSpriteRenderer.color = Color.red;
        }
    }
    public void SetOnFire()
    {
        isOnFire = true;
        //anim.SetBool("isOnFire", true);
        Debug.Log("OnFire");
    }
}
