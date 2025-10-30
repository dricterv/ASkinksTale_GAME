using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeevilPenPuzzle : MonoBehaviour
{
    public NumberCounter counter;

   private void OnTriggerEnter2D(Collider2D coll) 
   {
        Debug.Log(coll.tag);
        if(coll.tag == "Weevil")
        {
            //if carrying weevil add to counter
            Debug.Log("we");
            counter.AddToCount(1);
        }
   }
}
