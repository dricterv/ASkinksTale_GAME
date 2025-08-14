using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    private Animator anim;
    public bool opened;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if(opened == true)
        {
            anim.SetBool("open", true);
            anim.SetBool("closed", false);
        }
        else if(opened == false)
        {
            anim.SetBool("open", false);
            anim.SetBool("closed", true);
        }
    }

    public void OpenChest()
    {
        if(opened == false)
        {
            anim.SetBool("closed", false);

            anim.SetBool("open", true);
            opened = true;
        }
       
    }
}
