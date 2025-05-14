using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public float goDetectRange = 0.3f;
    public float playerDetectRange = 0.5f;
    public Transform playerFeet;
    public SpriteRenderer pressurePlateSpriteRenderer;
    public Sprite upSprite;
    public Sprite downSprite;
    public GameObject toggle;
    public bool playerActivated;
    public bool pushableActivated;
    public bool turnOn;
    public bool oneTime;
    private bool down;
    public NumberCounter counter;
    public Flamable flame;
    public bool torchOn;

    void Start()
    {
        pressurePlateSpriteRenderer.sprite = upSprite;
    }

    public void OnTriggerStay2D(Collider2D coll)
    {
        //Debug.Log("enter");

        if (coll.gameObject.tag == "Pushable" && pushableActivated == true)
        {
            float distance = Vector2.Distance(transform.position, coll.transform.position);
            Debug.Log("D: " + distance);
            
            if (distance <= goDetectRange )
            {
                Debug.Log("Down");
                if(down == false)
                {
                    pressurePlateSpriteRenderer.sprite = downSprite;
                    if (counter != null)
                    {
                        counter.AddToCount(1);
                        Debug.Log("counter up");
                    }
                    if (toggle != null && toggle.activeSelf == true)
                    {
                        toggle.SetActive(false);
                    }
                    if (flame != null && torchOn == true)
                    {
                        flame.SetOnFire();
                    }
                    down = true;
                }
                
            }
            else if (distance > goDetectRange && (playerFeet.position.x >= transform.position.x + playerDetectRange || playerFeet.position.x <= transform.position.x - playerDetectRange || playerFeet.position.y >= transform.position.y + playerDetectRange || playerFeet.position.y <= transform.position.y - playerDetectRange))
            {
                if (down == true)
                {
                    pressurePlateSpriteRenderer.sprite = upSprite;
                    if (toggle != null && toggle.activeSelf == false)
                    {
                        toggle.SetActive(true);
                    }
                    if (counter != null)
                    {
                        counter.AddToCount(-1);

                    }
                    if (flame != null && torchOn == true)
                    {
                        flame.FireOff();
                    }
                    Debug.Log("Up");
                    down = false;

                }
            }

        }
        else if (coll.gameObject.tag == "Player" && playerActivated == true)
        {
            

            if ((playerFeet.position.x <= transform.position.x + playerDetectRange && playerFeet.position.x >= transform.position.x - playerDetectRange))
            {
                if(playerFeet.position.y <= transform.position.y + playerDetectRange && playerFeet.position.y >= transform.position.y - playerDetectRange && down == false)
                {
                    if (turnOn == false)
                    {
                        if (counter != null)
                        {
                            counter.AddToCount(1);
                            Debug.Log("counter up");
                        }
                        if (toggle != null && toggle.activeSelf == true)
                        {
                            toggle.SetActive(false);
                        }
                        if (flame != null && torchOn == true)
                        {
                            flame.SetOnFire();
                        }
                        Debug.Log("Down");
                        pressurePlateSpriteRenderer.sprite = downSprite;
                        down = true;
                      //  Debug.Log("Player: " + playerFeet.position);
                       // Debug.Log("this: " + transform.position);
                       // Debug.Log("posx: " + playerFeet.position.x + " < " + (transform.position.x + playerDetectRange));
                       // Debug.Log("negx: " + playerFeet.position.x + " > " + (transform.position.x - playerDetectRange));
                       // Debug.Log("posy: " + playerFeet.position.y + " < " + (transform.position.y + playerDetectRange));
                       // Debug.Log("negy: " + playerFeet.position.y + " > " + (transform.position.y - playerDetectRange));


                    }
                    else if (turnOn == true)
                    {
                        if(counter != null)
                        {
                            counter.AddToCount(1);

                        }
                        if(toggle != null && toggle.activeSelf == false)
                        {
                            toggle.SetActive(true);
                        }
                        if (flame != null && torchOn == true)
                        {
                            flame.SetOnFire();
                        }
                        down = true;


                        pressurePlateSpriteRenderer.sprite = downSprite;
                    }
                }
                else if (oneTime == false)
                {
                    Debug.Log("1");

                    if (playerFeet.position.x >= transform.position.x + playerDetectRange || playerFeet.position.x <= transform.position.x - playerDetectRange || playerFeet.position.y >= transform.position.y + playerDetectRange || playerFeet.position.y <= transform.position.y - playerDetectRange)
                    {
                        Debug.Log("2");

                        if (turnOn == false && down == true)
                        {
                            pressurePlateSpriteRenderer.sprite = upSprite;
                            if (toggle != null && toggle.activeSelf == false )
                            {
                                toggle.SetActive(true);
                            }
                            if (counter != null)
                            {
                                counter.AddToCount(-1);

                            }
                            if (flame != null && torchOn == true)
                            {
                                flame.FireOff();
                            }
                            down = false;
                            Debug.Log("Up");
                        }
                        else if (turnOn == true && down == true)
                        {
                            pressurePlateSpriteRenderer.sprite = upSprite;
                            if (toggle != null && toggle.activeSelf == false)
                            {
                                toggle.SetActive(true);
                            }
                            if (counter != null)
                            {
                                counter.AddToCount(-1);

                            }
                            if (flame != null && torchOn == true)
                            {
                                flame.FireOff();
                            }
                            Debug.Log("Up");
                            down = false;
                        }
                    }
                }

            }
            else if(oneTime == false)
            {
                Debug.Log("1");

                if (playerFeet.position.x >= transform.position.x + playerDetectRange || playerFeet.position.x <= transform.position.x - playerDetectRange || playerFeet.position.y >= transform.position.y + playerDetectRange || playerFeet.position.y <= transform.position.y - playerDetectRange)
                {
                    Debug.Log("2");

                    if (turnOn == false && down == true)
                    {
                        if (toggle != null && toggle.activeSelf == false)
                        {
                            toggle.SetActive(true);
                        }
                        if (counter != null)
                        {
                            counter.AddToCount(-1);

                        }
                        if (flame != null && torchOn == true)
                        {
                            flame.FireOff();
                        }
                        down = false;

                    }
                    else if (turnOn == true && down == true)
                    {
                        pressurePlateSpriteRenderer.sprite = upSprite;
                        if (toggle != null && toggle.activeSelf == true)
                        {
                            toggle.SetActive(true);
                        }
                        if (counter != null)
                        {
                            counter.AddToCount(-1);

                        }
                        if (flame != null && torchOn == true)
                        {
                            flame.FireOff();
                        }
                        down = false;

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
