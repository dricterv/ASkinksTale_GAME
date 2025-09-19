using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemText : MonoBehaviour
{
    public GameObject text;
    public float time;
    void Start()
    {
        //healthText.text = "Press I to use the Match.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText()
    {
        if(text != null)
        {
            StartCoroutine(ActivateText());

        }
    }
    IEnumerator ActivateText()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(time);
        text.SetActive(false);

    }
}
