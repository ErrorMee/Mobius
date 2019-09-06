using strange.extensions.command.impl;
using strange.extensions.context.api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStartCommand : SignalCommand
{
    
    public override void Execute()
    {
        base.Execute();

        Debug.Log("GlobalStartCommand Execute");

        WindowConfigList.Instance.Init();

    }
}
