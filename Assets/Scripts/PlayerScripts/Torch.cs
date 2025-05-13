using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public float torchHeadRange;
    public LayerMask flamableLayer;
    private Transform flamableObject;
    private Transform flamePoint;
    public Transform pointLeft;
    public Transform pointRight;
    public Transform pointUp;
    public Transform pointDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightOnFire()
    {
        if (StatsManager.Instance.facing == new Vector2(0, -1))
        {
            flamePoint = pointDown;
        }
        if (StatsManager.Instance.facing == new Vector2(0, 1))
        {
            flamePoint = pointUp;
        }
        if (StatsManager.Instance.facing == new Vector2(-1, 0))
        {
            flamePoint = pointLeft;
        }
        if (StatsManager.Instance.facing == new Vector2(1, 0))
        {
            flamePoint = pointRight;
        }

        //Debug.Log("Torch");

        Collider2D[] hits = Physics2D.OverlapCircleAll(flamePoint.position, torchHeadRange, flamableLayer);
        if (hits.Length > 0)
        {
            flamableObject = hits[0].transform;
            if(flamableObject.gameObject.GetComponent<Flamable>().isFlamable == true && flamableObject.gameObject.GetComponent<Flamable>().isOnFire == false)
            {
                flamableObject.gameObject.GetComponent<Flamable>().SetOnFire();
            }
        }
    }
   

}
