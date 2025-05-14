using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Vector2 cameraChangeMax;
    public Vector2 cameraChangeMin;
    
    public Vector3 playerChange;
    private CameraMovement cam;
    public List<GameObject> enemiesEntry = new List<GameObject>();
    public List<GameObject> enemiesExit = new List<GameObject>();

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
            cam.minPosition = cameraChangeMin;
            cam.maxPosition = cameraChangeMax;
            coll.transform.position += playerChange;
            foreach (GameObject gameObject in enemiesEntry)
            {
                gameObject.SetActive(true);
                gameObject.GetComponent<EnemyHealth>().ChangeHealth(100);
                gameObject.transform.localPosition = gameObject.GetComponent<EnemyHealth>().spawn;
            }
            foreach (GameObject gameObject in enemiesExit)
            {
                gameObject.SetActive(false);
                
            }
        }
    }


    public void RemoveEntry(GameObject gameObject)
    {
        enemiesEntry.Remove(gameObject);
       
    }
    public void RemoveExit(GameObject gameObject)
    {
       
        enemiesExit.Remove(gameObject);
    }




}
