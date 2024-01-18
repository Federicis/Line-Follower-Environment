using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorColliderScript : MonoBehaviour
{

    public Boolean isOnLine;
    public Boolean justEntered;

    // Start is called before the first frame update
    void Start()
    {
        isOnLine = false;
        justEntered = false;
        if (name.EndsWith("2") || name.EndsWith("3") || name.EndsWith("4"))
            isOnLine=true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        isOnLine = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Contains("polygon"))
            isOnLine = false;
    }
}
