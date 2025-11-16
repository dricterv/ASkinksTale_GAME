using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoxStart : MonoBehaviour
{
    public GameObject cat;
    public GameObject fox;
    public GameObject wall;
    private bool started = false;
    void Update()
    {
        if(StatsManager.Instance.flags["catFoxStart"] == true && StatsManager.Instance.flags["catFoxFight"] == false && started == false)
        {
            cat.SetActive(true);
            fox.SetActive(true);
            wall.SetActive(true); 
            started = true;
        }
    }
}
