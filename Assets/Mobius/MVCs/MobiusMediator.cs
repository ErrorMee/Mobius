using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobiusMediator : Mediator
{
    [Inject]
    public WindowService windowService { get; set; }
    [HideInInspector]
    public WindowEnum windowEnum = WindowEnum.Undefined;

    public override void OnRegister()
    {
        base.OnRegister();
    }

    protected void CloseWindow()
    {
        windowService.CloseWindow(windowEnum);
    }

    public override void OnRemove()
    {
    }
}
