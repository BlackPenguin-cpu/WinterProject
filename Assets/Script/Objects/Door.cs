using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Objects
{
    public override void Interaction()
    {
        base.Interaction();
        TalkUIManager.Instance.IsTalk = true;
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
