using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject liveCanvas;
    public GameObject pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        liveCanvas.SetActive(true);
        pauseCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(liveCanvas.activeSelf == true)
            {
                liveCanvas.SetActive(false);
                pauseCanvas.SetActive(true);
            }
            else if(pauseCanvas.activeSelf == true)
            {
                liveCanvas.SetActive(true);
                pauseCanvas.SetActive(false);
            }
        }
    }
}
