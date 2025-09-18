using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Start()
    {
        transform.position = Vector2.zero;
    }
}
