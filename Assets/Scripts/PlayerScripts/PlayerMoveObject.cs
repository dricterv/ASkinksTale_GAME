using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveObject : MonoBehaviour
{
    public float grabDist = 1;
    public LayerMask pushableLayer;
    private RaycastHit2D hit;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       

    }
    /*
    public void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("push: " + coll.gameObject.name);
        if(coll.gameObject.tag == "Pushable")
        {
            playerController.ChangeState(PlayerState.Grabbing);

            //StatsManager.Instance.lockFace = true;
            // hit.collider.gameObject.GetComponent<Pushable>().StartPush();
            if (coll.gameObject.GetComponent<Pushable>().lockX == true)
            {
                if (StatsManager.Instance.facing == new Vector2(0, -1) || StatsManager.Instance.facing == new Vector2(0, 1))
                {
                    StatsManager.Instance.lockFace = true;
                    StatsManager.Instance.lockVert = true;
                    StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                    coll.gameObject.GetComponent<Pushable>().StartPush();
                }
                else if (coll.gameObject.GetComponent<Pushable>().lockY == true)
                {
                    if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                    {
                        StatsManager.Instance.lockFace = true;
                        StatsManager.Instance.lockHori = true;
                        StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                        coll.gameObject.GetComponent<Pushable>().StartPush();
                    }
                }
            }

            else if (coll.gameObject.GetComponent<Pushable>().lockY == true)
            {
                if (StatsManager.Instance.facing == new Vector2(-1, 0) || StatsManager.Instance.facing == new Vector2(1, 0))
                {
                    StatsManager.Instance.lockFace = true;
                    StatsManager.Instance.lockHori = true;
                    StatsManager.Instance.lockFacing = StatsManager.Instance.facing;
                    coll.gameObject.GetComponent<Pushable>().StartPush();
                }
               
            }
        }
    }
    public void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Pushable")
        {
            coll.gameObject.GetComponent<Pushable>().EndPush();

        }
    }*/
}
