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
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (liveCanvas.activeSelf == true)
        {
            Time.timeScale = 0;
            liveCanvas.SetActive(false);
            pauseCanvas.SetActive(true);
        }
        else if (pauseCanvas.activeSelf == true)
        {
            Time.timeScale = 1;
            liveCanvas.SetActive(true);
            pauseCanvas.SetActive(false);
        }
    }
}
