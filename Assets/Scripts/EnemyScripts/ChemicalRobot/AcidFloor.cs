using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidFloor : MonoBehaviour
{
    public int damage = 1;
    public float lifeSpan;
    private float timer;
    private float timerMax = .1f;

    // Start is called before the first frame update
    void Start()
    {


        //float x = Mathf.Floor(transform.position.x);
        // float y = Mathf.Floor(transform.position.y) + 0.5f;

        // transform.position = new Vector2(x, y);

        timer = timerMax;

        Destroy(gameObject, lifeSpan);

    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
           coll.gameObject.GetComponent<PlayerHealth>().ChangeHealth(-damage);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Acid")
        {
            if(timer <= 0)
            {
                Destroy(gameObject);
            }
        }
        if(coll.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

}
