using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Objects
{
    protected override void Interaction()
    {
        onClick_MainMenu.OnApplicationQuit();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}