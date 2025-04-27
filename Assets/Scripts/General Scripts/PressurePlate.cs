using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public float goDetectRange = 0.3f;
    public float playerDetectRange = 0.5f;
    public Transform playerFeet;

    public GameObject toggle;
    public bool playerActivated;
    public bool pushableActivated;
    public bool turnOn;
    public bool oneTime;




    public void OnTriggerStay2D(Collider2D coll)
    {
        //Debug.Log("enter");

        if (coll.gameObject.tag == "Pushable" && pushableActivated == true)
        {
            float distance = Vector2.Distance(transform.position, coll.transform.position);
            Debug.Log("D: " + distance);
            
            if (distance <= goDetectRange)
            {
                Debug.Log("Down");
                if(toggle.activeSelf == true)
                {
                    toggle.SetActive(false);

                }
                
            }
            else if (distance > goDetectRange)
            {
                if (toggle.activeSelf == false)
                {
                    toggle.SetActive(true);
                    Debug.Log("Up");
                }
            }

        }
        else if (coll.gameObject.tag == "Player" && playerActivated == true)
        {
            

            if ((playerFeet.position.x <= transform.position.x + playerDetectRange && playerFeet.position.x >= transform.position.x - playerDetectRange))
            {
                if(playerFeet.position.y <= transform.position.y + playerDetectRange && playerFeet.position.y >= transform.position.y - playerDetectRange)
                {
                    if (toggle.activeSelf == true && turnOn == false)
                    {
                        toggle.SetActive(false);
                        Debug.Log("Down");
                        Debug.Log("Player: " + playerFeet.position);
                        Debug.Log("this: " + transform.position);
                        Debug.Log("posx: " + playerFeet.position.x + " < " + (transform.position.x + playerDetectRange));
                        Debug.Log("negx: " + playerFeet.position.x + " > " + (transform.position.x - playerDetectRange));
                        Debug.Log("posy: " + playerFeet.position.y + " < " + (transform.position.y + playerDetectRange));
                        Debug.Log("negy: " + playerFeet.position.y + " > " + (transform.position.y - playerDetectRange));


                    }
                    else if (toggle.activeSelf == false && turnOn == true)
                    {
                        toggle.SetActive(true);
                    }
                }
              
            }
            if(oneTime == false)
            {
                if (playerFeet.position.x >= transform.position.x + playerDetectRange || playerFeet.position.x <= transform.position.x - playerDetectRange || playerFeet.position.y >= transform.position.y + playerDetectRange || playerFeet.position.y <= transform.position.y - playerDetectRange)
                {
                    if (toggle.activeSelf == false && turnOn == true)
                    {
                        toggle.SetActive(true);
                        Debug.Log("Up");
                    }
                    else if (toggle.activeSelf == true && turnOn == false)
                    {
                        toggle.SetActive(true);
                        Debug.Log("Up");
                    }
                }
            }
           

        }

    }
    /*public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && playerActivated == true)
        {
            if((playerFeet.position.x  <= transform.position.x + playerDetectRange || playerFeet.position.x  >= transform.position.x - playerDetectRange) && (playerFeet.position.y <= transform.position.y + playerDetectRange || playerFeet.position.y  >= transform.position.y - playerDetectRange))
            {
                if (toggle.activeSelf == true)
                {
                    toggle.SetActive(false);
                    Debug.Log("Down");
                }
            }
            
        }
    }
    public void OnTriggerExit2D(Collider2D coll)
    {
         if (coll.gameObject.tag == "Player" && playerActivated == true)
         {
            if ((playerFeet.position.x >= transform.position.x + playerDetectRange || playerFeet.position.x <= transform.position.x - playerDetectRange) && (playerFeet.position.y >= transform.position.y + playerDetectRange || playerFeet.position.y <= transform.position.y - playerDetectRange))
            {
                if (toggle.activeSelf == false)
                {
                    toggle.SetActive(true);
                    Debug.Log("Up");
                }
            }
             
         }
    } */
}
