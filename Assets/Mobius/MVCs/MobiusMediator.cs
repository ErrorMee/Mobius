using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WindowBaseView))]
[DisallowMultipleComponent]
public class MobiusMediator : Mediator
{
    [Inject]
    public WindowService windowService { get; set; }

    [Inject]
    public WindowModel windowModel { get; set; }

    [ReadOnly]
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
