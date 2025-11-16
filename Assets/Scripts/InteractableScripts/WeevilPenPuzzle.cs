using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeevilPenPuzzle : MonoBehaviour
{
    public NumberCounter counter;
    public int max;
    private int counting;
    private bool questStarted = false;
    public List<GameObject> disableList;
    public List<GameObject> enableList;
    public List<GameObject> otherList;


    private void Start()
    {
            counting = 0;
            bool done = StatsManager.Instance.flags["weevilsCaught"];
            if (done == true)
            {

                foreach (GameObject go in disableList)
                {
                    go.SetActive(false);
                }
                foreach (GameObject go in otherList)
                {
                    go.SetActive(false);
                }
                foreach (GameObject go in enableList)
                {
                    go.SetActive(true);
                }

                questStarted = true;
                Debug.Log("alreadyDone");
            }
    }

    private void Update()
    {
        if(StatsManager.Instance.flags["weevilsStart"] == true && questStarted == false)
        {
            foreach (GameObject go in disableList)
            {
                go.SetActive(true);
            }
            foreach (GameObject go in enableList)
            {
                go.SetActive(false);
            }
            foreach (GameObject go in otherList)
            {
                go.SetActive(false);
            }
            questStarted = true;
            Debug.Log("started");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D coll) 
   {
        Debug.Log(coll.tag);
        if(coll.tag == "Weevil")
        {
            //if carrying weevil add to counter
            Debug.Log("we");
            counter.AddToCount(1);
            coll.gameObject.SetActive(false);
            enableList[counting].SetActive(true);
            counting++;


        }
   }
}
