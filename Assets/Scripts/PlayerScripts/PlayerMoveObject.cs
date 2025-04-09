using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveObject : MonoBehaviour
{
    public float grabDist = 1;
    public LayerMask pushableLayer;
    private RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if ((Input.GetKeyDown(KeyCode.L)))
        {
            hit = Physics2D.Raycast(this.transform.position, StatsManager.Instance.facing, grabDist, pushableLayer);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Pushable>().StartPush();
            }

        }
        if ((Input.GetKeyUp(KeyCode.L)))
        {

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<Pushable>().EndPush();
            }

        }
        */

    }
}
