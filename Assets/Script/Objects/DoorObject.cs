using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : Objects
{
    public GameObject ExitUI;

    public override void Interaction()
    {
        base.Interaction();
        ExitUI.SetActive(true);
    }


}
