using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;

    public Vector3 playerChange;
    private CameraMovement cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            cam.minPosition += cameraChangeMin;
            cam.maxPosition += cameraChangeMax;
            coll.transform.position += playerChange;
        }
    }
}
